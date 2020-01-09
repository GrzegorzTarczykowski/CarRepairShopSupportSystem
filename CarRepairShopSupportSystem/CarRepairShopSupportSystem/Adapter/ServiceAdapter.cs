using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CarRepairShopSupportSystem.BLL.Models;
using Newtonsoft.Json;

namespace CarRepairShopSupportSystem.Adapter
{
    internal class ServiceAdapter : BaseAdapter
    {
        private readonly Context context;
        private readonly Service[] services;
        private readonly Service[] selectedServices;

        public ServiceAdapter(Context context, Service[] services, Service[] selectedServices)
        {
            this.context = context;
            this.services = services;
            this.selectedServices = selectedServices;
        }

        public override int Count => services.Length;

        public override Java.Lang.Object GetItem(int position)
        {
            return JsonConvert.SerializeObject(services[position]);
        }

        public override long GetItemId(int position)
        {
            return services[position].ServiceId;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            LinearLayout linearLayout = new LinearLayout(context);
            linearLayout.Orientation = Orientation.Vertical;
            TextView tvBrandNameAndModelName = new TextView(context)
            {
                TextSize = 40,
                Text = $"{services[position].Name}"
            };
            linearLayout.AddView(tvBrandNameAndModelName);
            TextView tvRegistrationNumbers = new TextView(context)
            {
                TextSize = 20,
                Text = $"Cena: {services[position].Price} [PLN]"
            };
            linearLayout.AddView(tvRegistrationNumbers);

            if (selectedServices.Any(ss => ss.ServiceId == services[position].ServiceId))
            {
                linearLayout.SetBackgroundColor(Android.Graphics.Color.GreenYellow);
            }
            else
            {
                linearLayout.SetBackgroundColor(Android.Graphics.Color.Transparent);
            }

            return linearLayout;
        }
    }
}