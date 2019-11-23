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
    [Activity(Label = "Edytor zamówienia")]
    public class OrderEditorActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_orderEditor);

            FindViewById<Button>(Resource.Id.btnService).Click += BtnService_Click;
            FindViewById<Button>(Resource.Id.btnVehiclePart).Click += BtnVehiclePart_Click;
            FindViewById<Button>(Resource.Id.btnTimetable).Click += BtnTimetable_Click;
            FindViewById<Button>(Resource.Id.btnAddOrder).Click += BtnAddOrder_Click;
        }

        private void BtnAddOrder_Click(object sender, EventArgs e)
        {
            
        }

        private void BtnTimetable_Click(object sender, EventArgs e)
        {
            
        }

        private void BtnVehiclePart_Click(object sender, EventArgs e)
        {
            
        }

        private void BtnService_Click(object sender, EventArgs e)
        {
            
        }
    }
}