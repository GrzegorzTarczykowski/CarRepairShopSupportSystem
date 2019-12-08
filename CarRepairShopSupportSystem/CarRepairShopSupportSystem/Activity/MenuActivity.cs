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

namespace CarRepairShopSupportSystem.Activity
{
    [Activity(Label = "Menu")]
    public class MenuActivity : AppCompatActivity
    {
        private const int editUserRequestCode = 1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_menu);
            FindViewById<Button>(Resource.Id.btnMyVehicle).Click += BtnMyVehicle_Click;
            FindViewById<Button>(Resource.Id.btnMyAccount).Click += BtnMyAccount_Click;
            FindViewById<Button>(Resource.Id.btnContact).Click += BtnContact_Click;
            FindViewById<Button>(Resource.Id.btnLogout).Click += BtnLogout_Click;
        }

        private void BtnContact_Click(object sender, EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(ContactActivity));
            StartActivity(nextActivity);
        }

        private void BtnMyAccount_Click(object sender, EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(RegisterActivity));
            nextActivity.PutExtra(nameof(OperationType), OperationType.Edit.GetDescription());
            StartActivity(nextActivity);
        }

        private void BtnMyVehicle_Click(object sender, EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(VehicleListActivity));
            StartActivity(nextActivity);
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            this.Finish();
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == editUserRequestCode)
            {
                if (resultCode == Result.Ok)
                {
                    Toast.MakeText(Application.Context, "Pomyślnie zminiono dane użytkownika", ToastLength.Long).Show();
                }
            }
        }
    }
}