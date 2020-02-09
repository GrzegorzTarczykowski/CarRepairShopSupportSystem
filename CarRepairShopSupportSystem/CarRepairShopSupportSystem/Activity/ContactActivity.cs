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
    [Activity(Label = "Kontakt")]
    public class ContactActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_contact);
            FindViewById<TextView>(Resource.Id.tvContact).Text = 
            @"Kontakt z warsztatem: Telefon: 123456789
Email: warsztat @warsztat.pl
Twórcami oprogramowania są:
Artur Krakowiak
Grzegorz Tarczykowski";
        }
    }
}