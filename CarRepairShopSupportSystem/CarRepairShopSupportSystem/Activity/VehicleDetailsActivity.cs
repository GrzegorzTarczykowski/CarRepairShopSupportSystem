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
using CarRepairShopSupportSystem.BLL.Extensions;
using CarRepairShopSupportSystem.BLL.Models;
using Newtonsoft.Json;

namespace CarRepairShopSupportSystem.Activity
{
    [Activity(Label = "Szczegóły pojazdu")]
    public class VehicleDetailsActivity : AppCompatActivity
    {
        private const int vehicleRequestCode = 1;
        private Vehicle vehicle;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_vehicleDetails);
            RefreshVehicleDetailsList(Intent);

            FindViewById<Button>(Resource.Id.btnEditVehicle).Click += BtnEditVehicle_Click;
        }

        private void BtnEditVehicle_Click(object sender, EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(VehicleActivity));
            nextActivity.PutExtra(nameof(OperationType), OperationType.Edit.GetDescription());
            nextActivity.PutExtra("VehicleDetails", JsonConvert.SerializeObject(vehicle));
            StartActivityForResult(nextActivity, vehicleRequestCode);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == vehicleRequestCode)
            {
                RefreshVehicleDetailsList(data);
            }
        }

        private void RefreshVehicleDetailsList(Intent intent)
        {
            vehicle = JsonConvert.DeserializeObject<Vehicle>(intent.GetStringExtra("VehicleDetails"));
            FindViewById<TextView>(Resource.Id.tvEngineMileage).Text = vehicle.EngineMileage.ToString();
            FindViewById<TextView>(Resource.Id.tvRegistrationNumbers).Text = vehicle.RegistrationNumbers;
            FindViewById<TextView>(Resource.Id.tvVehicleBrand).Text = vehicle.VehicleBrandName;
            FindViewById<TextView>(Resource.Id.tvVehicleModel).Text = vehicle.VehicleModelName;
            FindViewById<TextView>(Resource.Id.tvEngineMileage).Text = vehicle.VehicleEngineCode;
            FindViewById<TextView>(Resource.Id.tvVehicleFuel).Text = vehicle.VehicleFuelName;
            FindViewById<TextView>(Resource.Id.tvVehicleType).Text = vehicle.VehicleTypeName;
        }
    }
}