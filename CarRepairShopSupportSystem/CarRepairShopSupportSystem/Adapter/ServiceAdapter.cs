using System.Collections.Generic;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using CarRepairShopSupportSystem.BLL.Models;

namespace CarRepairShopSupportSystem.Adapter
{
    internal class ServiceAdapter : BaseAdapter<Service>
    {
        private readonly AppCompatActivity context;
        private readonly List<Service> serviceList;

        public ServiceAdapter(AppCompatActivity context, List<Service> serviceList) : base()
        {
            this.context = context;
            this.serviceList = serviceList;
        }

        public override Service this[int position] => serviceList[position];

        public override int Count => serviceList.Count;

        public override long GetItemId(int position) => serviceList[position].ServiceId;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var service = serviceList[position];
            if (convertView == null)
            {
                convertView = context.LayoutInflater.Inflate(Resource.Layout.activity_serviceListItem, null);
            }
            convertView.FindViewById<TextView>(Resource.Id.tvServiceNameItem).Text = $"{service.Name}";
            convertView.FindViewById<TextView>(Resource.Id.tvServicePriceItem).Text = $"Cena: {service.Price} [PLN]";
            convertView.FindViewById<TextView>(Resource.Id.tvServiceExecutionTimeItem).Text = $"Czas trwania: {service.ExecutionTimeInMinutes} minut";
            return convertView;
        }
    }
}