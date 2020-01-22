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
using Newtonsoft.Json;

namespace CarRepairShopSupportSystem.Adapter
{
    class VehiclePartAdapter : BaseAdapter
    {
        private readonly Context context;
        private readonly VehiclePart[] vehicleParts;
        private readonly VehiclePart[] selectedVehicleParts;

        public VehiclePartAdapter(Context context, VehiclePart[] vehicleParts, VehiclePart[] selectedVehicleParts)
        {
            this.context = context;
            this.vehicleParts = vehicleParts;
            this.selectedVehicleParts = selectedVehicleParts;
        }

        public override int Count => vehicleParts.Length;

        public override Java.Lang.Object GetItem(int position)
        {
            return JsonConvert.SerializeObject(vehicleParts[position]);
        }

        public override long GetItemId(int position)
        {
            return vehicleParts[position].VehiclePartId;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            LinearLayout linearLayout = new LinearLayout(context);
            linearLayout.Orientation = Orientation.Vertical;
            TextView tvBrandNameAndModelName = new TextView(context)
            {
                TextSize = 40,
                Text = $"  {vehicleParts[position].Name}"
            };
            linearLayout.AddView(tvBrandNameAndModelName);
            TextView tvRegistrationNumbers = new TextView(context)
            {
                TextSize = 20,
                Text = $"Cena: {vehicleParts[position].Price} [PLN]"
            };
            linearLayout.AddView(tvRegistrationNumbers);

            if (selectedVehicleParts.Any(svp => svp.VehiclePartId == vehicleParts[position].VehiclePartId))
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