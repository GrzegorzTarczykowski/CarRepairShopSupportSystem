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
using CarRepairShopSupportSystem.Adapter;
using CarRepairShopSupportSystem.BLL.Enums;
using CarRepairShopSupportSystem.BLL.Extensions;
using CarRepairShopSupportSystem.BLL.IService;
using CarRepairShopSupportSystem.BLL.Models;
using CarRepairShopSupportSystem.BLL.Service;
using Newtonsoft.Json;

namespace CarRepairShopSupportSystem.Activity
{
    [Activity(Label = "Dodaj/Edytuj pojazd")]
    public class VehicleActivity : AppCompatActivity
    {
        private readonly IVehicleService vehicleService;
        private readonly IVehicleBrandService vehicleBrandService;
        private readonly IVehicleModelService vehicleModelService;
        private readonly IVehicleEngineService vehicleEngineService;
        private readonly IVehicleFuelService vehicleFuelService;
        private readonly IVehicleTypeService vehicleTypeService;

        private IList<VehicleBrand> vehicleBrandList;
        private IList<VehicleEngine> vehicleEngineList;
        private IList<VehicleFuel> vehicleFuelList;
        private IList<VehicleType> vehicleTypeList;
        private IList<VehicleModel> vehicleModelList;
        private Vehicle vehicle;

        public VehicleActivity()
        {
            vehicleService = new VehicleService(new HttpClientService(new AccessTokenService(new ApplicationSessionService(), new TokenService())), new ApplicationSessionService());
            vehicleBrandService = new VehicleBrandService(new HttpClientService(new AccessTokenService(new ApplicationSessionService(), new TokenService())));
            vehicleModelService = new VehicleModelService(new HttpClientService(new AccessTokenService(new ApplicationSessionService(), new TokenService())));
            vehicleEngineService = new VehicleEngineService(new HttpClientService(new AccessTokenService(new ApplicationSessionService(), new TokenService())));
            vehicleFuelService = new VehicleFuelService(new HttpClientService(new AccessTokenService(new ApplicationSessionService(), new TokenService())));
            vehicleTypeService = new VehicleTypeService(new HttpClientService(new AccessTokenService(new ApplicationSessionService(), new TokenService())));
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_vehicle);

            Button btnSubmitOperation = FindViewById<Button>(Resource.Id.btnSubmitOperation);
            btnSubmitOperation.Text = Intent.GetStringExtra(nameof(OperationType));
            btnSubmitOperation.Click += BtnSubmitOperation_Click;

            Spinner spinnerVehicleBrand = FindViewById<Spinner>(Resource.Id.spinnerVehicleBrand);
            spinnerVehicleBrand.ItemSelected += SpinnerVehicleBrand_ItemSelected;
            vehicleBrandList = vehicleBrandService.GetAllVehicleBrandList().ToList();
            var vehicleBrandAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, vehicleBrandList.Select(vb => vb.Name).ToList());
            vehicleBrandAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinnerVehicleBrand.Adapter = vehicleBrandAdapter;

            Spinner spinnerVehicleEngine = FindViewById<Spinner>(Resource.Id.spinnerVehicleEngine);
            vehicleEngineList = vehicleEngineService.GetAllVehicleEngineList().ToList();
            var vehicleEngineAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, vehicleEngineList.Select(vb => vb.EngineCode).ToList());
            vehicleEngineAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinnerVehicleEngine.Adapter = vehicleEngineAdapter;

            Spinner spinnerVehicleFuel = FindViewById<Spinner>(Resource.Id.spinnerVehicleFuel);
            vehicleFuelList = vehicleFuelService.GetAllVehicleFuelList().ToList();
            var vehicleFuelAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, vehicleFuelList.Select(vb => vb.Name).ToList());
            vehicleFuelAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinnerVehicleFuel.Adapter = vehicleFuelAdapter;

            Spinner spinnerVehicleType = FindViewById<Spinner>(Resource.Id.spinnerVehicleType);
            vehicleTypeList = vehicleTypeService.GetAllVehicleTypeList().ToList();
            var vehicleTypeAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, vehicleTypeList.Select(vb => vb.Name).ToList());
            vehicleTypeAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinnerVehicleType.Adapter = vehicleTypeAdapter;

            if (Intent.GetStringExtra(nameof(OperationType)) == OperationType.Edit.GetDescription())
            {
                vehicle = JsonConvert.DeserializeObject<Vehicle>(Intent.GetStringExtra("VehicleDetails"));
                FindViewById<EditText>(Resource.Id.editEngineMileage).Text = vehicle.EngineMileage.ToString();
                FindViewById<EditText>(Resource.Id.editRegistrationNumbers).Text = vehicle.RegistrationNumbers;

                FindViewById<Spinner>(Resource.Id.spinnerVehicleModel).Enabled = false;
                FindViewById<Spinner>(Resource.Id.spinnerVehicleBrand).Enabled = false;
                FindViewById<Spinner>(Resource.Id.spinnerVehicleBrand)
                    .SetSelection(vehicleBrandList.IndexOf(vehicleBrandList.FirstOrDefault(vb => vb.VehicleBrandId == vehicle.VehicleBrandId)));
                FindViewById<Spinner>(Resource.Id.spinnerVehicleEngine).Enabled = false;
                FindViewById<Spinner>(Resource.Id.spinnerVehicleEngine)
                    .SetSelection(vehicleEngineList.IndexOf(vehicleEngineList.FirstOrDefault(vb => vb.VehicleEngineId == vehicle.VehicleEngineId)));
                FindViewById<Spinner>(Resource.Id.spinnerVehicleFuel).Enabled = false;
                FindViewById<Spinner>(Resource.Id.spinnerVehicleFuel)
                    .SetSelection(vehicleFuelList.IndexOf(vehicleFuelList.FirstOrDefault(vb => vb.VehicleFuelId == vehicle.VehicleFuelId)));
                FindViewById<Spinner>(Resource.Id.spinnerVehicleType).Enabled = false;
                FindViewById<Spinner>(Resource.Id.spinnerVehicleType)
                    .SetSelection(vehicleTypeList.IndexOf(vehicleTypeList.FirstOrDefault(vb => vb.VehicleTypeId == vehicle.VehicleTypeId)));
            }
        }

        private void SpinnerVehicleBrand_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = FindViewById<Spinner>(Resource.Id.spinnerVehicleModel);
            vehicleModelList = vehicleModelService.GetVehicleModelListByVehicleBrandId(e.Position + 1).ToList();
            var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, vehicleModelList.Select(vb => vb.Name).ToList());
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;
        }

        private void BtnSubmitOperation_Click(object sender, EventArgs e)
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
                if (vehicle == null)
                {
                    vehicle = new Vehicle();
                }

                vehicle.EngineMileage = engineMileage;
                vehicle.RegistrationNumbers = FindViewById<EditText>(Resource.Id.editRegistrationNumbers).Text.ToUpper();
                vehicle.VehicleBrandId = vehicleBrandList[FindViewById<Spinner>(Resource.Id.spinnerVehicleBrand).SelectedItemPosition].VehicleBrandId;
                vehicle.VehicleBrandName = vehicleBrandList[FindViewById<Spinner>(Resource.Id.spinnerVehicleBrand).SelectedItemPosition].Name;
                vehicle.VehicleEngineId = vehicleEngineList[FindViewById<Spinner>(Resource.Id.spinnerVehicleEngine).SelectedItemPosition].VehicleEngineId;
                vehicle.VehicleEngineCode = vehicleEngineList[FindViewById<Spinner>(Resource.Id.spinnerVehicleEngine).SelectedItemPosition].EngineCode;
                vehicle.VehicleFuelId = vehicleFuelList[FindViewById<Spinner>(Resource.Id.spinnerVehicleFuel).SelectedItemPosition].VehicleFuelId;
                vehicle.VehicleFuelName = vehicleFuelList[FindViewById<Spinner>(Resource.Id.spinnerVehicleFuel).SelectedItemPosition].Name;
                vehicle.VehicleTypeId = vehicleTypeList[FindViewById<Spinner>(Resource.Id.spinnerVehicleType).SelectedItemPosition].VehicleTypeId;
                vehicle.VehicleTypeName = vehicleTypeList[FindViewById<Spinner>(Resource.Id.spinnerVehicleType).SelectedItemPosition].Name;
                vehicle.VehicleModelId = vehicleModelList[FindViewById<Spinner>(Resource.Id.spinnerVehicleModel).SelectedItemPosition].VehicleModelId;
                vehicle.VehicleModelName = vehicleModelList[FindViewById<Spinner>(Resource.Id.spinnerVehicleModel).SelectedItemPosition].Name;

                OperationResult operationResult;
                if (Intent.GetStringExtra(nameof(OperationType)) == OperationType.Add.GetDescription())
                {
                    operationResult = vehicleService.AddUserVehicle(vehicle);
                }
                else
                {
                    operationResult = vehicleService.EditUserVehicle(vehicle);
                }

                if (operationResult.ResultCode == ResultCode.Successful)
                {
                    Intent intent = new Intent(this, typeof(VehicleDetailsActivity));
                    intent.PutExtra("VehicleDetails", JsonConvert.SerializeObject(vehicle));
                    SetResult(Result.Ok, intent);
                    Finish();
                }
                else
                {
                    Toast.MakeText(Application.Context, operationResult.Message, ToastLength.Long).Show();
                }
            }
        }
    }
}