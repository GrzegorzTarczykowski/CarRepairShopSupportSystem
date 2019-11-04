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
            throw new NotImplementedException();
        }

        public override long GetItemId(int position)
        {
            throw new NotImplementedException();
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            TextView dummyTextView = new TextView(context);
            dummyTextView.Text = vehicles[position].VehicleModelName;
            return dummyTextView;
        }
    }
}