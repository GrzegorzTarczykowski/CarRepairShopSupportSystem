using System;
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
    [Activity(Label = "Edytor zleceń")]
    public class OrderEditorActivity : AppCompatActivity
    {
        private const int serviceListRequestCode = 1;
        private const int timetableRequestCode = 2;
        private const int vehiclePartListRequestCode = 3;
        private IList<BLL.Models.Service> selectedServiceList;
        private IList<VehiclePart> selectedVehiclePartList;
        private decimal sumServicePrice;
        private decimal sumVehiclePartPrice;
        private DateTime selectedStartDateTime;
        private int vehicleId;

        private readonly IOrderService orderService;

        public OrderEditorActivity()
        {
            orderService = new OrderService(new HttpClientService(new AccessTokenService(new ApplicationSessionService(), new TokenService())));
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_orderEditor);

            vehicleId = int.Parse(Intent.GetStringExtra("VehicleId"));

            FindViewById<Button>(Resource.Id.btnService).Click += BtnService_Click;
            FindViewById<Button>(Resource.Id.btnVehiclePart).Click += BtnVehiclePart_Click;
            FindViewById<Button>(Resource.Id.btnTimetable).Click += BtnTimetable_Click;
            FindViewById<Button>(Resource.Id.btnAddOrder).Click += BtnAddOrder_Click;
        }

        private void BtnAddOrder_Click(object sender, EventArgs e)
        {
            if (!(selectedServiceList?.Count > 0))
            {
                Toast.MakeText(Application.Context, "Wybierz przynajmniej jedna usługe", ToastLength.Long).Show();
            }
            else if (selectedStartDateTime < DateTime.Now)
            {
                Toast.MakeText(Application.Context, "Wybierz termin realizacji usługi", ToastLength.Long).Show();
            }
            else
            {
                Order order = new Order
                {
                    TotalCost = selectedServiceList.Sum(ss => ss.Price),
                    CreateDate = DateTime.Now,
                    PlannedStartDateOfRepair = selectedStartDateTime,
                    OrderStatusId = (int)OrderStatusId.Planned,
                    VehicleId = vehicleId,
                    ContainsServices = selectedServiceList//.AsEnumerable()
                };

                OperationResult operationResult = orderService.AddVehicleOrder(order);

                if (operationResult.ResultCode == ResultCode.Successful)
                {
                    Intent intent = new Intent(this, typeof(VehicleDetailsActivity));
                    //intent.PutExtra("VehicleDetails", JsonConvert.SerializeObject(vehicle));
                    SetResult(Result.Ok, intent);
                    Finish();
                }
                else
                {
                    Toast.MakeText(Application.Context, operationResult.Message, ToastLength.Long).Show();
                }
            }
        }

        private void BtnTimetable_Click(object sender, EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(TimetableActivity));
            StartActivityForResult(nextActivity, timetableRequestCode);
        }

        private void BtnVehiclePart_Click(object sender, EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(VehiclePartListActivity));
            nextActivity.PutExtra("SelectedVehiclePartList", JsonConvert.SerializeObject(selectedVehiclePartList));
            StartActivityForResult(nextActivity, vehiclePartListRequestCode);
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
                    sumServicePrice = selectedServiceList.Sum(ss => ss.Price);
                }
            }
            else if (requestCode == timetableRequestCode)
            {
                if (resultCode == Result.Ok)
                {
                    selectedStartDateTime = JsonConvert.DeserializeObject<DateTime>(data.GetStringExtra("SelectedDateTime"));
                    FindViewById<TextView>(Resource.Id.tvPlannedStartDateOfRepair).Text = selectedStartDateTime.ToString();
                }
            }
            else if (requestCode == vehiclePartListRequestCode)
            {
                if (resultCode == Result.Ok)
                {
                    selectedVehiclePartList = JsonConvert.DeserializeObject<IList<VehiclePart>>(data.GetStringExtra("SelectedVehiclePartList"));
                    sumVehiclePartPrice = selectedVehiclePartList.Sum(svp => svp.Price);
                }
            }
            FindViewById<TextView>(Resource.Id.tvTotalCostOrder).Text = $"{sumServicePrice + sumVehiclePartPrice}";
        }
    }
}