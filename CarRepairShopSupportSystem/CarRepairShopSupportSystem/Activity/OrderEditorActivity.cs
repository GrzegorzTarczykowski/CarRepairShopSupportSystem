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
using Newtonsoft.Json;

namespace CarRepairShopSupportSystem.Activity
{
    [Activity(Label = "Edytor zleceń")]
    public class OrderEditorActivity : AppCompatActivity
    {
        private const int serviceListRequestCode = 1;
        private IList<BLL.Models.Service> selectedServiceList;

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
            Intent nextActivity = new Intent(this, typeof(TimetableActivity));
            StartActivityForResult(nextActivity, serviceListRequestCode);
        }

        private void BtnVehiclePart_Click(object sender, EventArgs e)
        {
            
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
                    FindViewById<TextView>(Resource.Id.tvTotalCostOrder).Text = selectedServiceList.Sum(ss => ss.Price).ToString();
                }
            }
        }
    }
}