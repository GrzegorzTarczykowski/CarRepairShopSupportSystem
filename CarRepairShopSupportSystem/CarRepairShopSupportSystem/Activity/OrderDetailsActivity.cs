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
    [Activity(Label = "OrderDetailsActivity")]
    public class OrderDetailsActivity : AppCompatActivity
    {
        private const int serviceListRequestCode = 1;
        private const int vehiclePartListRequestCode = 2;
        private IList<BLL.Models.Service> selectedServiceList;
        private IList<VehiclePart> selectedVehiclePartList;
        private IList<OrderStatus> orderStatusList;
        private decimal sumServicePrice;
        private decimal sumVehiclePartPrice;
        private Order order;
        private readonly IOrderService orderService;
        private readonly IOrderStatusService orderStatusService;

        public OrderDetailsActivity()
        {
            this.orderService = new OrderService(new HttpClientService(new AccessTokenService(new ApplicationSessionService(), new TokenService())));
            this.orderStatusService = new OrderStatusService(new HttpClientService(new AccessTokenService(new ApplicationSessionService(), new TokenService())));

        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_orderDetails);

            FindViewById<Button>(Resource.Id.btnService).Click += BtnService_Click;
            FindViewById<Button>(Resource.Id.btnMessages).Click += BtnMessages_Click;
            FindViewById<Button>(Resource.Id.btnDeleteOrder).Click += BtnDeleteOrder_Click;
            order = JsonConvert.DeserializeObject<Order>(Intent.GetStringExtra("OrderDetails"));

            if (Intent.GetStringExtra("IsWorker") == "Yes")
            {
                FindViewById<Button>(Resource.Id.btnNextOrder).Visibility = ViewStates.Gone;
                FindViewById<TextView>(Resource.Id.tvOrderStatusNameLabel).Visibility = ViewStates.Gone;
                FindViewById<TextView>(Resource.Id.tvOrderStatusName).Visibility = ViewStates.Gone;
                FindViewById<TextView>(Resource.Id.tvOrderStatusNameLabelForWorker).Visibility = ViewStates.Visible;

                Spinner spinnerOrderStatusNameForWorker = FindViewById<Spinner>(Resource.Id.spinnerOrderStatusNameForWorker);
                spinnerOrderStatusNameForWorker.Visibility = ViewStates.Visible;
                spinnerOrderStatusNameForWorker.ItemSelected += SpinnerOrderStatusNameForWorker_ItemSelected;
                orderStatusList = orderStatusService.GetAllOrderStatusList().ToList();
                var orderStatusAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, orderStatusList.Select(os => os.Name).ToList());
                orderStatusAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                spinnerOrderStatusNameForWorker.Adapter = orderStatusAdapter;
            }
            else
            {
                FindViewById<Button>(Resource.Id.btnNextOrder).Click += BtnNextOrder_Click;
            }
        }

        private void SpinnerOrderStatusNameForWorker_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            order.OrderStatusId = orderStatusList[e.Position + 1].OrderStatusId;
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

        private void BtnNextOrder_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void BtnDeleteOrder_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void BtnMessages_Click(object sender, EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(MessageActivity));
            nextActivity.PutExtra("OrderId", order.OrderId.ToString());
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
            order = orderService.GetOrderByOrderId(order.OrderId);
            FindViewById<TextView>(Resource.Id.tvTotalCost).Text = $"{sumServicePrice + sumVehiclePartPrice}";
        }
    }
}