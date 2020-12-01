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
using CarRepairShopSupportSystem.BLL.IService;
using CarRepairShopSupportSystem.BLL.Models;
using CarRepairShopSupportSystem.BLL.Service;

namespace CarRepairShopSupportSystem.Activity
{
    [Activity(Label = "Menu")]
    public class MenuActivity : AppCompatActivity
    {
        private const int editUserRequestCode = 1;
        private const int serviceListRequestCode = 2;
        private const int vehiclePartListRequestCode = 3;
        private const int orderManagerListRequestCode = 4;
        private const int orderByWorkerListRequestCode = 5;
        private const int workerListManagerRequestCode = 6;
        private readonly IApplicationSessionService applicationSessionService;

        public MenuActivity()
        {
            applicationSessionService = new ApplicationSessionService();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_menu);
            FindViewById<Button>(Resource.Id.btnMyVehicle).Click += BtnMyVehicle_Click;
            FindViewById<Button>(Resource.Id.btnMyAccount).Click += BtnMyAccount_Click;
            FindViewById<Button>(Resource.Id.btnContact).Click += BtnContact_Click;
            FindViewById<Button>(Resource.Id.btnLogout).Click += BtnLogout_Click;
            //User user = applicationSessionService.GetUserFromApplicationSession();
            //if (user.PermissionId == (int)PermissionId.User)
            //{
            //    FindViewById<Button>(Resource.Id.btnDayTimetable).Visibility = ViewStates.Gone;
            //    FindViewById<Button>(Resource.Id.btnTimetableManagement).Visibility = ViewStates.Gone;
            //    FindViewById<Button>(Resource.Id.btnServiceManagement).Visibility = ViewStates.Gone;
            //    FindViewById<Button>(Resource.Id.btnVehiclePartManagement).Visibility = ViewStates.Gone;
            //    FindViewById<Button>(Resource.Id.btnWorkersManagement).Visibility = ViewStates.Gone;
            //}
            //else if (user.PermissionId == (int)PermissionId.Admin)
            //{
            //    FindViewById<Button>(Resource.Id.btnDayTimetable).Click += BtnDayTimetable_Click;
            //    FindViewById<Button>(Resource.Id.btnMyVehicle).Visibility = ViewStates.Gone;
            //    FindViewById<Button>(Resource.Id.btnContact).Visibility = ViewStates.Gone;
            //    FindViewById<Button>(Resource.Id.btnTimetableManagement).Visibility = ViewStates.Gone;
            //    FindViewById<Button>(Resource.Id.btnServiceManagement).Visibility = ViewStates.Gone;
            //    FindViewById<Button>(Resource.Id.btnVehiclePartManagement).Visibility = ViewStates.Gone;
            //    FindViewById<Button>(Resource.Id.btnWorkersManagement).Visibility = ViewStates.Gone;
            //}
            //else if (user.PermissionId == (int)PermissionId.SuperAdmin)
            //{
            //    FindViewById<Button>(Resource.Id.btnTimetableManagement).Click += BtnTimetableManagement_Click;
            //    FindViewById<Button>(Resource.Id.btnServiceManagement).Click += BtnServiceManagement_Click;
            //    FindViewById<Button>(Resource.Id.btnVehiclePartManagement).Click += BtnVehiclePartManagement_Click;
            //    FindViewById<Button>(Resource.Id.btnWorkersManagement).Click += BtnWorkersManagement_Click;
            //    FindViewById<Button>(Resource.Id.btnMyVehicle).Text = "Samochody klientów";
            //    FindViewById<Button>(Resource.Id.btnContact).Visibility = ViewStates.Gone;
            //    FindViewById<Button>(Resource.Id.btnDayTimetable).Visibility = ViewStates.Gone;
            //}
        }

        private void BtnWorkersManagement_Click(object sender, EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(WorkerListManagerActivity));
            nextActivity.PutExtra(nameof(OperationType), OperationType.Edit.GetDescription());
            StartActivityForResult(nextActivity, workerListManagerRequestCode);
        }

        private void BtnVehiclePartManagement_Click(object sender, EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(VehiclePartListActivity));
            nextActivity.PutExtra(nameof(OperationType), OperationType.Edit.GetDescription());
            StartActivityForResult(nextActivity, vehiclePartListRequestCode);
        }

        private void BtnServiceManagement_Click(object sender, EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(ServiceListActivity));
            nextActivity.PutExtra(nameof(OperationType), OperationType.Edit.GetDescription());
            StartActivityForResult(nextActivity, serviceListRequestCode);
        }

        private void BtnTimetableManagement_Click(object sender, EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(OrderManagerListActivity));
            StartActivityForResult(nextActivity, orderManagerListRequestCode);
        }

        private void BtnDayTimetable_Click(object sender, EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(OrderByWorkerListActivity));
            StartActivityForResult(nextActivity, orderByWorkerListRequestCode);
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
            nextActivity.PutExtra(nameof(PermissionId), ((PermissionId)applicationSessionService.GetUserFromApplicationSession().PermissionId).ToString());
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