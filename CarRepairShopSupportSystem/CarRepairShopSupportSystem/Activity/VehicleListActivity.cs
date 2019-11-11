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
using CarRepairShopSupportSystem.BLL.IService;
using CarRepairShopSupportSystem.BLL.Models;
using CarRepairShopSupportSystem.BLL.Service;

namespace CarRepairShopSupportSystem.Activity
{
    [Activity(Label = "Moje pojazdy")]
    public class VehicleListActivity : AppCompatActivity
    {
        private const int vehicleRequestCode = 1;
        private readonly IVehicleService vehicleService;
        private readonly IApplicationSessionService applicationSessionService;

        public VehicleListActivity()
        {
            vehicleService = new VehicleService(new HttpClientService(new AccessTokenService(new ApplicationSessionService(), new TokenService())));
            applicationSessionService = new ApplicationSessionService();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_vehicleList);
            GridView gvVehicleList = FindViewById<GridView>(Resource.Id.gvVehicleList);
            IEnumerable<Vehicle> vehicles = vehicleService.GetVehicleListByUserId(applicationSessionService.GetUserFromApplicationSession().UserId);
            gvVehicleList.Adapter = new VehicleAdapter(this, vehicles.ToArray());
            FindViewById<Button>(Resource.Id.btnAddVehicle).Click += BtnAddVehicle_Click;
        }

        private void BtnAddVehicle_Click(object sender, EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(VehicleActivity));
            StartActivityForResult(nextActivity, vehicleRequestCode);
        }
    }
}