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

namespace CarRepairShopSupportSystem.Activity
{
    [Activity(Label = "Dodaj/Edytuj pojazd")]
    public class VehicleActivity : AppCompatActivity
    {
        private readonly IVehicleService vehicleService;
        private readonly IVehicleBrandService vehicleBrandService;
        private readonly IVehicleModelService vehicleModelService;

        public VehicleActivity()
        {
            vehicleService = new VehicleService(new HttpClientService(new AccessTokenService(new ApplicationSessionService(), new TokenService())));
            vehicleBrandService = new VehicleBrandService(new HttpClientService(new AccessTokenService(new ApplicationSessionService(), new TokenService())));
            vehicleModelService = new VehicleModelService(new HttpClientService(new AccessTokenService(new ApplicationSessionService(), new TokenService())));
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_vehicle);
            FindViewById<Button>(Resource.Id.btnAddEdit).Click += BtnAddEdit_Click;

            Spinner spinner = FindViewById<Spinner>(Resource.Id.spinnerVehicleBrand);
            spinner.ItemSelected += SpinnerVehicleBrand_ItemSelected;
            var vehicleBrandNameList = vehicleBrandService.GetAllVehicleBrandList().Select(vb => vb.Name);
            var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, vehicleBrandNameList.ToList());
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;
        }

        private void SpinnerVehicleBrand_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = FindViewById<Spinner>(Resource.Id.spinnerVehicleModel);
            var list = vehicleModelService.GetVehicleModelListByVehicleBrandId(e.Position + 1).Select(vb => vb.Name);
            var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, list.ToList());
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;
        }

        private void BtnAddEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FindViewById<EditText>(Resource.Id.editEngineMileage).Text))
            {
                Toast.MakeText(Application.Context, "Uzupełnij przebieg samochodu", ToastLength.Long).Show();
            }
            else if (!int.TryParse(FindViewById<EditText>(Resource.Id.editEngineMileage).Text, out int engineMileage))
            {
                Toast.MakeText(Application.Context, "Przebieg samochodu musi być liczbą", ToastLength.Long).Show();
            }
            else if (string.IsNullOrWhiteSpace(FindViewById<EditText>(Resource.Id.editRegistrationNumbers).Text))
            {
                Toast.MakeText(Application.Context, "Uzupełnij numer rejestracyjny", ToastLength.Long).Show();
            }
            else
            {
                Vehicle vehicle = new Vehicle()
                {
                    EngineMileage = engineMileage,
                    RegistrationNumbers = FindViewById<EditText>(Resource.Id.editRegistrationNumbers).Text
                };

                OperationResult operationResult = vehicleService.AddUserVehicle(vehicle);
                if (operationResult.ResultCode == ResultCode.Successful)
                {
                    SetResult(Result.Ok);
                    this.Finish();
                }
                else
                {
                    Toast.MakeText(Application.Context, operationResult.Message, ToastLength.Long).Show();
                }
            }
        }
    }
}