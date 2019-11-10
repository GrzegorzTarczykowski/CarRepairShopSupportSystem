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
using CarRepairShopSupportSystem.BLL.Models;

namespace CarRepairShopSupportSystem.Activity
{
    [Activity(Label = "Moje pojazdy")]
    public class VehicleListActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_vehicleList);
            GridView gvVehicleList = FindViewById<GridView>(Resource.Id.gvVehicleList);
            gvVehicleList.Adapter = new VehicleAdapter(this, Array.Empty<Vehicle>());
            FindViewById<Button>(Resource.Id.btnAddVehicle).Click += BtnAddVehicle_Click;
        }

        private void BtnAddVehicle_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}