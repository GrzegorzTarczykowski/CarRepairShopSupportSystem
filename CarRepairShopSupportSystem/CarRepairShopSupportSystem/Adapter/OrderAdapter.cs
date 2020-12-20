using System.Collections.Generic;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using CarRepairShopSupportSystem.BLL.Enums;
using CarRepairShopSupportSystem.BLL.Extensions;
using CarRepairShopSupportSystem.BLL.Models;

namespace CarRepairShopSupportSystem.Adapter
{
    class OrderAdapter : BaseAdapter<Order>
    {
        private readonly AppCompatActivity context;
        private readonly List<Order> orderList;
        private readonly bool isVisibleVehicleRegistrationNumbers;

        public OrderAdapter(AppCompatActivity context, List<Order> orderList, bool isVisibleVehicleRegistrationNumbers)
        {
            this.context = context;
            this.orderList = orderList;
            this.isVisibleVehicleRegistrationNumbers = isVisibleVehicleRegistrationNumbers;
        }

        public override Order this[int position] => orderList[position];

        public override int Count => orderList.Count;

        public override long GetItemId(int position) => orderList[position].OrderId;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var order = orderList[position];
            if (convertView == null)
            {
                convertView = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItemActivated2, null);
            }
            convertView.FindViewById<TextView>(Android.Resource.Id.Text1).Text 
                = $"{position + 1}. Data rozpoczęcia zlecenia: {order.StartDateOfRepair}. Data zakończenia zlecenia: {order.EndDateOfRepair}. Status zlecenia: {((OrderStatusId)order.OrderStatusId).GetDescription()}";
            if (isVisibleVehicleRegistrationNumbers)
            {
                convertView.FindViewById<TextView>(Android.Resource.Id.Text2).Text
                    = $"Numer rejestracyjny: {order.VehicleRegistrationNumbers}";
            }
            return convertView;
        }
    }
}