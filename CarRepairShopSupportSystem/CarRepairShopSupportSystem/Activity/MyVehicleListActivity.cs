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
using CarRepairShopSupportSystem.Adapter;

namespace CarRepairShopSupportSystem.Activity
{
    [Activity(Label = "Moje pojazdy")]
    public class MyVehicleListActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_myVehicleList);
            GridView gvVehicleList = FindViewById<GridView>(Resource.Id.gvVehicleList);
            gvVehicleList.Adapter = new VehicleAdapter(this, null);
        }
    }
}