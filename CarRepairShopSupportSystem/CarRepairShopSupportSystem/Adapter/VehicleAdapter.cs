using System.Collections.Generic;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using CarRepairShopSupportSystem.BLL.Models;

namespace CarRepairShopSupportSystem.Adapter
{
    internal class VehicleAdapter : BaseAdapter<Vehicle>
    {
        private readonly AppCompatActivity context;
        private readonly List<Vehicle> vehicleList;

        public VehicleAdapter(AppCompatActivity context, List<Vehicle> vehicleList)
        {
            this.context = context;
            this.vehicleList = vehicleList;
        }

        public override Vehicle this[int position] => vehicleList[position];

        public override int Count => vehicleList.Count;

        public override long GetItemId(int position) => vehicleList[position].VehicleId;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var vehicle = vehicleList[position];
            if (convertView == null)
            {
                convertView = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItemActivated2, null);
            }
            convertView.FindViewById<TextView>(Android.Resource.Id.Text1).Text
                = $"{vehicle.VehicleBrandName} {vehicle.VehicleModelName}";
            convertView.FindViewById<TextView>(Android.Resource.Id.Text2).Text
                    = $"Numer rejestracyjny: {vehicle.RegistrationNumbers}";
            return convertView;
        }
    }
}