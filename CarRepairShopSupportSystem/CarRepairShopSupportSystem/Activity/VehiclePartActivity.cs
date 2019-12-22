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
using CarRepairShopSupportSystem.BLL.Models;
using Newtonsoft.Json;

namespace CarRepairShopSupportSystem.Activity
{
    [Activity(Label = "Edytor części")]
    public class VehiclePartActivity : AppCompatActivity
    {
        VehiclePart selectedVehiclePart;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_vehiclePart);
            selectedVehiclePart = JsonConvert.DeserializeObject<VehiclePart>(Intent.GetStringExtra("SelectedVehiclePart"));
        }
    }
}