using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Autofac;
using CarRepairShopSupportSystem.BLL.Enums;
using CarRepairShopSupportSystem.BLL.IService;
using CarRepairShopSupportSystem.BLL.Models;
using Newtonsoft.Json;

namespace CarRepairShopSupportSystem.Activity
{
    [Activity(Label = "Edytor części")]
    public class VehiclePartActivity : AppCompatActivity
    {
        VehiclePart selectedVehiclePart;
        readonly IVehiclePartService vehiclePartService;

        public VehiclePartActivity()
        {
            vehiclePartService = MainApplication.Container.Resolve<IVehiclePartService>();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_vehiclePart);
            selectedVehiclePart = JsonConvert.DeserializeObject<VehiclePart>(Intent.GetStringExtra("SelectedVehiclePart") ?? string.Empty);
            Button btnSubmitVehiclePart = FindViewById<Button>(Resource.Id.btnSubmitVehiclePart);
            btnSubmitVehiclePart.Click += BtnSubmitVehiclePart_Click;
            if (selectedVehiclePart != null)
            {
                btnSubmitVehiclePart.Text = "Modyfikuj";
                FindViewById<EditText>(Resource.Id.editVehiclePartName).Text = selectedVehiclePart.Name;
                FindViewById<EditText>(Resource.Id.editVehiclePartPrice).Text = selectedVehiclePart.Price.ToString();
                FindViewById<Button>(Resource.Id.btnDeleteVehiclePart).Click += BtnDeleteVehiclePart_Click;
            }
            else
            {
                btnSubmitVehiclePart.Text = "Dodaj";
                FindViewById<Button>(Resource.Id.btnDeleteVehiclePart).Visibility = ViewStates.Gone;
            }
        }

        private void BtnDeleteVehiclePart_Click(object sender, EventArgs e)
        {
            OperationResult operationResult = vehiclePartService.DeleteVehiclePart(selectedVehiclePart.VehiclePartId);

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

        private void BtnSubmitVehiclePart_Click(object sender, EventArgs e)
        {
            string textVehiclePartName = FindViewById<EditText>(Resource.Id.editVehiclePartName).Text;
            string textVehiclePartPrice = FindViewById<EditText>(Resource.Id.editVehiclePartPrice).Text;

            if (string.IsNullOrWhiteSpace(textVehiclePartName))
            {
                Toast.MakeText(Application.Context, "Uzupełnij nazwę częsci", ToastLength.Long).Show();
            }
            else if (string.IsNullOrWhiteSpace(textVehiclePartPrice))
            {
                Toast.MakeText(Application.Context, "Uzupełnij cenę", ToastLength.Long).Show();
            }
            else if (!decimal.TryParse(textVehiclePartPrice, out decimal vehiclePartPrice))
            {
                Toast.MakeText(Application.Context, "Nie poprawna cena", ToastLength.Long).Show();
            }
            else
            {
                OperationResult operationResult = null;
                if (selectedVehiclePart != null)
                {
                    selectedVehiclePart.Name = textVehiclePartName;
                    selectedVehiclePart.Price = vehiclePartPrice;
                    operationResult = vehiclePartService.EditVehiclePart(selectedVehiclePart);
                }
                else
                {
                    selectedVehiclePart = new VehiclePart
                    {
                        Name = textVehiclePartName,
                        Price = vehiclePartPrice
                    };
                    operationResult = vehiclePartService.AddVehiclePart(selectedVehiclePart);
                }

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