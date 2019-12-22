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
    [Activity(Label = "Edytor usług")]
    public class ServiceActivity : AppCompatActivity
    {
        BLL.Models.Service selectedService;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_service);
            selectedService = JsonConvert.DeserializeObject<BLL.Models.Service>(Intent.GetStringExtra("SelectedService"));
        }
    }
}