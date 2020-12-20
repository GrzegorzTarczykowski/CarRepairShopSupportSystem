using System.Collections.Generic;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using CarRepairShopSupportSystem.BLL.Models;

namespace CarRepairShopSupportSystem.Adapter
{
    class VehiclePartAdapter : BaseAdapter<VehiclePart>
    {
        private readonly AppCompatActivity context;
        private readonly List<VehiclePart> vehiclePartList;

        public VehiclePartAdapter(AppCompatActivity context, List<VehiclePart> vehiclePartList)
        {
            this.context = context;
            this.vehiclePartList = vehiclePartList;
        }

        public override VehiclePart this[int position] => vehiclePartList[position];

        public override int Count => vehiclePartList.Count;

        public override long GetItemId(int position) => vehiclePartList[position].VehiclePartId;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var vehiclePart = vehiclePartList[position];
            if (convertView == null)
            {
                convertView = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItemActivated2, null);
            }
            convertView.FindViewById<TextView>(Android.Resource.Id.Text1).Text = $"{vehiclePart.Name}";
            convertView.FindViewById<TextView>(Android.Resource.Id.Text2).Text = $"Cena: {vehiclePart.Price} [PLN]";
            return convertView;
        }
    }
}