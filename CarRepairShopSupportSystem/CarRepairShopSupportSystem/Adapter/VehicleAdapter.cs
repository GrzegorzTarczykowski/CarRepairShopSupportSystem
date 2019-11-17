using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CarRepairShopSupportSystem.BLL.Models;

namespace CarRepairShopSupportSystem.Adapter
{
    internal class VehicleAdapter : BaseAdapter
    {
        private readonly Context context;
        private readonly Vehicle[] vehicles;

        public VehicleAdapter(Context context, Vehicle[] vehicles)
        {
            this.context = context;
            this.vehicles = vehicles;
        }

        public override int Count => vehicles.Length;

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return 0;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            LinearLayout linearLayout;

            if (convertView == null)
            {
                linearLayout = new LinearLayout(context);
                linearLayout.Orientation = Orientation.Vertical;
                TextView tvBrandNameAndModelName = new TextView(context)
                {
                    TextSize = 40,
                    Text = $"{vehicles[position].VehicleBrandName} {vehicles[position].VehicleModelName}"
                };
                linearLayout.AddView(tvBrandNameAndModelName);
                TextView tvRegistrationNumbers = new TextView(context)
                {
                    TextSize = 20,
                    Text = $"Numer rejestracyjny: {vehicles[position].RegistrationNumbers}"
                };
                linearLayout.AddView(tvRegistrationNumbers);
            }
            else
            {
                linearLayout = (LinearLayout)convertView;
            }
            
            return linearLayout;
        }
    }
}