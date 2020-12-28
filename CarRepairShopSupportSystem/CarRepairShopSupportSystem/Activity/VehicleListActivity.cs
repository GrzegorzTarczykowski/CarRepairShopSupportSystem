using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Widget;
using Autofac;
using CarRepairShopSupportSystem.Adapter;
using CarRepairShopSupportSystem.BLL.Enums;
using CarRepairShopSupportSystem.BLL.Extensions;
using CarRepairShopSupportSystem.BLL.IService;
using CarRepairShopSupportSystem.BLL.Models;
using Newtonsoft.Json;

namespace CarRepairShopSupportSystem.Activity
{
    [Activity(Label = "Moje pojazdy")]
    public class VehicleListActivity : AppCompatActivity
    {
        private const int vehicleRequestCode = 1;
        private const int vehicleDetailsRequestCode = 2;
        private readonly IVehicleService vehicleService;

        private List<Vehicle> vehicleList;

        public VehicleListActivity()
        {
            vehicleService = MainApplication.Container.Resolve<IVehicleService>();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_vehicleList);
            ListView lvVehicleList = FindViewById<ListView>(Resource.Id.lvVehicleList);
            RefreshLvVehicleList(lvVehicleList);
            lvVehicleList.ItemClick += GvVehicleList_ItemClick;
            FindViewById<Button>(Resource.Id.btnAddVehicle).Click += BtnAddVehicle_Click;
        }

        private void GvVehicleList_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(VehicleDetailsActivity));
            nextActivity.PutExtra("VehicleDetails", JsonConvert.SerializeObject(vehicleList[e.Position]));
            StartActivityForResult(nextActivity, vehicleDetailsRequestCode);
        }

        private void BtnAddVehicle_Click(object sender, EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(VehicleActivity));
            nextActivity.PutExtra(nameof(OperationType), OperationType.Add.GetDescription());
            StartActivityForResult(nextActivity, vehicleRequestCode);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == vehicleRequestCode
                || requestCode == vehicleDetailsRequestCode)
            {
                RefreshLvVehicleList(FindViewById<ListView>(Resource.Id.lvVehicleList));
            }
        }

        private void RefreshLvVehicleList(ListView lvVehicleList)
        {
            if (Intent.GetStringExtra(nameof(PermissionId)) == PermissionId.SuperAdmin.ToString())
            {
                vehicleList = vehicleService.GetVehicleList().ToList();
            }
            else
            {
                vehicleList = vehicleService.GetVehicleListByUserId().ToList();
            }
            lvVehicleList.Adapter = new VehicleAdapter(this, vehicleList);
        }
    }
}