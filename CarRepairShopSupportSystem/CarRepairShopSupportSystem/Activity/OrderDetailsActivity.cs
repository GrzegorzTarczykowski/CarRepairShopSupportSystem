﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using CarRepairShopSupportSystem.BLL.Enums;
using CarRepairShopSupportSystem.BLL.IService;
using CarRepairShopSupportSystem.BLL.Models;
using CarRepairShopSupportSystem.BLL.Service;
using Newtonsoft.Json;

namespace CarRepairShopSupportSystem.Activity
{
    [Activity(Label = "Szczegóły pojazdu")]
    public class OrderDetailsActivity : AppCompatActivity
    {
        private const int serviceListRequestCode = 1;
        private const int vehiclePartListRequestCode = 2;
        private const int workerListManagerRequestCode = 3;
        private IList<User> selectedWorkerList;
        private IList<BLL.Models.Service> selectedServiceList;
        private IList<VehiclePart> selectedVehiclePartList;
        private IList<OrderStatus> orderStatusList;
        private decimal sumServicePrice;
        private decimal sumVehiclePartPrice;
        private Order order;
        private readonly IOrderService orderService;
        private readonly IOrderStatusService orderStatusService;
        private readonly IVehicleService vehicleService;
        private readonly IApplicationSessionService applicationSessionService;
        private bool isInitOrderStatus = true;
        private int userReceiverId;

        public OrderDetailsActivity()
        {
            this.orderService = new OrderService(new HttpClientService(new AccessTokenService(new ApplicationSessionService(), new TokenService())));
            this.orderStatusService = new OrderStatusService(new HttpClientService(new AccessTokenService(new ApplicationSessionService(), new TokenService())));
            this.vehicleService = new VehicleService(new HttpClientService(new AccessTokenService(new ApplicationSessionService(), new TokenService())), new ApplicationSessionService());
            this.applicationSessionService = new ApplicationSessionService();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_orderDetails);

            FindViewById<Button>(Resource.Id.btnService).Click += BtnService_Click;
            FindViewById<Button>(Resource.Id.btnMessages).Click += BtnMessages_Click;
            FindViewById<Button>(Resource.Id.btnDeleteOrder).Click += BtnDeleteOrder_Click;
            order = JsonConvert.DeserializeObject<Order>(Intent.GetStringExtra("OrderDetails"));
            selectedWorkerList = order.WorkByUsers.ToList();

            FindViewById<TextView>(Resource.Id.tvOrderStatusName).Text = order.OrderStatusName;
            FindViewById<TextView>(Resource.Id.tvStartDateOfRepair).Text = order.StartDateOfRepair?.ToString() ?? string.Empty;
            FindViewById<TextView>(Resource.Id.tvEndDateOfRepair).Text = order.EndDateOfRepair?.ToString() ?? string.Empty;
            FindViewById<TextView>(Resource.Id.tvTotalCost).Text = order.TotalCost.ToString();
            FindViewById<TextView>(Resource.Id.tvWorkByUsers).Text = $"{order.WorkByUsers.FirstOrDefault()?.FirstName} {order.WorkByUsers.FirstOrDefault()?.LastName}";
            FindViewById<TextView>(Resource.Id.tvDescription).Text = order.Description;

            int permissionId = applicationSessionService.GetUserFromApplicationSession().PermissionId;

            if ((PermissionId)permissionId == PermissionId.Admin || (PermissionId)permissionId == PermissionId.SuperAdmin)
            {
                userReceiverId = vehicleService.GetUserIdOwnerByVehicleId(order.VehicleId);
                FindViewById<TextView>(Resource.Id.tvOrderStatusNameLabel).Visibility = ViewStates.Gone;
                FindViewById<TextView>(Resource.Id.tvOrderStatusName).Visibility = ViewStates.Gone;
                FindViewById<TextView>(Resource.Id.tvOrderStatusNameLabelForWorker).Visibility = ViewStates.Visible;

                Spinner spinnerOrderStatusNameForWorker = FindViewById<Spinner>(Resource.Id.spinnerOrderStatusNameForWorker);
                spinnerOrderStatusNameForWorker.Visibility = ViewStates.Visible;
                orderStatusList = orderStatusService.GetAllOrderStatusList().ToList();
                var orderStatusAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, orderStatusList.Select(os => os.Name).ToList());
                orderStatusAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                spinnerOrderStatusNameForWorker.Adapter = orderStatusAdapter;
                spinnerOrderStatusNameForWorker.ItemSelected += SpinnerOrderStatusNameForWorker_ItemSelected;

                if ((PermissionId)permissionId == PermissionId.SuperAdmin)
                {
                    FindViewById<TextView>(Resource.Id.tvWorkByUsersLabel).Visibility = ViewStates.Gone;
                    FindViewById<TextView>(Resource.Id.tvWorkByUsers).Visibility = ViewStates.Gone;
                    Button btnAddWorker = FindViewById<Button>(Resource.Id.btnAddWorker);
                    btnAddWorker.Visibility = ViewStates.Visible;
                    btnAddWorker.Click += BtnAddWorker_Click;
                }
            }
            else
            {
                userReceiverId = order.WorkByUsers.FirstOrDefault()?.UserId ?? 1;
            }
        }

        private void BtnAddWorker_Click(object sender, EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(WorkerListManagerActivity));
            nextActivity.PutExtra("SelectedWorkerList", JsonConvert.SerializeObject(selectedWorkerList));
            StartActivityForResult(nextActivity, workerListManagerRequestCode);
        }

        private void SpinnerOrderStatusNameForWorker_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (isInitOrderStatus)
            {
                var orderStatus = orderStatusList
                    .FirstOrDefault(os => os.OrderStatusId == order.OrderStatusId);
                FindViewById<Spinner>(Resource.Id.spinnerOrderStatusNameForWorker)
                    .SetSelection(orderStatusList.IndexOf(orderStatus));
                isInitOrderStatus = false;
                return;
            }

            order.OrderStatusId = orderStatusList[e.Position].OrderStatusId;
            OperationResult operationResult = orderService.EditVehicleOrder(order);

            if (operationResult.ResultCode == ResultCode.Successful)
            {
                Toast.MakeText(Application.Context, "Zmieniono status zamówienia", ToastLength.Long).Show();
            }
            else
            {
                Toast.MakeText(Application.Context, operationResult.Message, ToastLength.Long).Show();
            }
        }

        private void BtnDeleteOrder_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void BtnMessages_Click(object sender, EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(MessageActivity));
            nextActivity.PutExtra("OrderId", order.OrderId.ToString());
            nextActivity.PutExtra("UserReceiverId", userReceiverId.ToString());
            StartActivityForResult(nextActivity, serviceListRequestCode);
        }

        private void BtnService_Click(object sender, EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(ServiceListActivity));
            nextActivity.PutExtra("SelectedServiceList", JsonConvert.SerializeObject(selectedServiceList));
            StartActivityForResult(nextActivity, serviceListRequestCode);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            order = orderService.GetOrderByOrderId(order.OrderId);
            if (requestCode == serviceListRequestCode)
            {
                if (resultCode == Result.Ok)
                {
                    selectedServiceList = JsonConvert.DeserializeObject<IList<BLL.Models.Service>>(data.GetStringExtra("SelectedServiceList"));
                }
            }
            else if (requestCode == vehiclePartListRequestCode)
            {
                if (resultCode == Result.Ok)
                {
                    selectedVehiclePartList = JsonConvert.DeserializeObject<IList<VehiclePart>>(data.GetStringExtra("SelectedVehiclePartList"));
                }
            }
            if (requestCode == workerListManagerRequestCode)
            {
                if (resultCode == Result.Ok)
                {
                    selectedWorkerList = JsonConvert.DeserializeObject<IList<User>>(data.GetStringExtra("SelectedWorkerList"));
                    order.WorkByUsers = selectedWorkerList;
                    OperationResult operationResult = orderService.EditVehicleOrder(order);

                    if (operationResult.ResultCode == ResultCode.Successful)
                    {
                        Toast.MakeText(Application.Context, "Przypisano pracowników", ToastLength.Long).Show();
                    }
                    else
                    {
                        Toast.MakeText(Application.Context, operationResult.Message, ToastLength.Long).Show();
                    }
                }
            }
            FindViewById<TextView>(Resource.Id.tvTotalCost).Text = $"{sumServicePrice + sumVehiclePartPrice}";
        }
    }
}