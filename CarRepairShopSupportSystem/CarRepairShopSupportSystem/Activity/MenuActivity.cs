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

namespace CarRepairShopSupportSystem.Activity
{
    [Activity(Label = "Menu")]
    public class MenuActivity : AppCompatActivity
    {
        private const int myVehicleListRequestCode = 1;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_menu);
            FindViewById<Button>(Resource.Id.btnMyVehicle).Click += BtnMyVehicle_Click; ;
            FindViewById<Button>(Resource.Id.btnLogout).Click += BtnLogout_Click;
        }

        private void BtnMyVehicle_Click(object sender, EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(MyVehicleListActivity));
            StartActivityForResult(nextActivity, myVehicleListRequestCode);
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            this.Finish();
        }
    }
}