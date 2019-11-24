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
using Newtonsoft.Json;

namespace CarRepairShopSupportSystem.Activity
{
    [Activity(Label = "OrderDetailsActivity")]
    public class OrderDetailsActivity : AppCompatActivity
    {
        private const int serviceListRequestCode = 1;
        private IList<BLL.Models.Service> selectedServiceList;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_orderDetails);

            FindViewById<Button>(Resource.Id.btnService).Click += BtnService_Click;
            FindViewById<Button>(Resource.Id.btnSentMessages).Click += BtnSentMessages_Click;
            FindViewById<Button>(Resource.Id.btnReceivedMessages).Click += BtnReceivedMessages_Click;
            FindViewById<Button>(Resource.Id.btnDeleteOrder).Click += BtnDeleteOrder_Click;
            FindViewById<Button>(Resource.Id.btnNextOrder).Click += BtnNextOrder_Click;
        }

        private void BtnNextOrder_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void BtnDeleteOrder_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void BtnReceivedMessages_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void BtnSentMessages_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void BtnService_Click(object sender, EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(ServiceListActivity));
            nextActivity.PutExtra("SelectedServiceList", JsonConvert.SerializeObject(selectedServiceList));
            StartActivityForResult(nextActivity, serviceListRequestCode);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == serviceListRequestCode)
            {
                if (resultCode == Result.Ok)
                {
                    selectedServiceList = JsonConvert.DeserializeObject<IList<BLL.Models.Service>>(data.GetStringExtra("SelectedServiceList"));
                }
            }
        }
    }
}