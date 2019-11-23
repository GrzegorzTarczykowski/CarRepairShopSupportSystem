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

        public ServiceAdapter(Context context, Service[] services)
        {
            this.context = context;
            this.services = services;
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
            ScrollView scrollView;

            if (convertView == null)
            {
                scrollView = new ScrollView(context);
                RelativeLayout relativeLayout = new RelativeLayout(context);
                LinearLayout linearLayout = new LinearLayout(context);
                linearLayout.Orientation = Orientation.Vertical;
                LinearLayout linearLayoutHorizontal = new LinearLayout(context);
                linearLayoutHorizontal.Orientation = Orientation.Horizontal;
                CheckBox checkBox = new CheckBox(context);
                checkBox.Click += CheckBox_Click;
                linearLayoutHorizontal.AddView(checkBox);
                TextView tvBrandNameAndModelName = new TextView(context)
                {
                    TextSize = 60,
                    Text = $"{services[position].Name}"
                };
                linearLayoutHorizontal.AddView(tvBrandNameAndModelName);
                linearLayout.AddView(linearLayoutHorizontal);
                TextView tvRegistrationNumbers = new TextView(context)
                {
                    TextSize = 20,
                    Text = $"Cena: {services[position].Price} [PLN]"
                };
                linearLayout.AddView(tvRegistrationNumbers);
                relativeLayout.AddView(linearLayout);
                scrollView.AddView(relativeLayout);
            }
            else
            {
                scrollView = (ScrollView)convertView;
            }

            return scrollView;
        }

        private void CheckBox_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }
    }
}