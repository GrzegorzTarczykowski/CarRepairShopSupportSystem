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
    [Activity(Label = "Moje pojazdy")]
    public class VehicleListActivity : AppCompatActivity
    {
        private const int vehicleRequestCode = 1;
        private const int vehicleDetailsRequestCode = 2;
        private readonly IVehicleService vehicleService;

        private IList<Vehicle> vehicles;

        public VehicleListActivity()
        {
            vehicleService = new VehicleService(new HttpClientService(new AccessTokenService(new ApplicationSessionService(), new TokenService())), new ApplicationSessionService());
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_vehicleList);
            GridView gvVehicleList = FindViewById<GridView>(Resource.Id.gvVehicleList);
            RefreshGvVehicleList(gvVehicleList);
            gvVehicleList.ItemClick += GvVehicleList_ItemClick;
            FindViewById<Button>(Resource.Id.btnAddVehicle).Click += BtnAddVehicle_Click;
        }

        private void GvVehicleList_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(VehicleDetailsActivity));
            nextActivity.PutExtra("VehicleDetails", JsonConvert.SerializeObject(vehicles[e.Position]));
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
                RefreshGvVehicleList(FindViewById<GridView>(Resource.Id.gvVehicleList));
            }
        }

        private void RefreshGvVehicleList(GridView gvVehicleList)
        {
            vehicles = vehicleService.GetVehicleListByUserId().ToList();
            gvVehicleList.Adapter = new VehicleAdapter(this, vehicles.ToArray());
        }
    }
}