﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CarRepairShopSupportSystem.BLL.Enums;
using CarRepairShopSupportSystem.BLL.Extensions;
using CarRepairShopSupportSystem.BLL.Models;
using Java.Lang;
using Newtonsoft.Json;

namespace CarRepairShopSupportSystem.Adapter
{
    class OrderAdapter : BaseAdapter
    {
        private readonly Context context;
        private readonly Order[] orders;

        public OrderAdapter(Context context, Order[] orders)
        {
            this.context = context;
            this.orders = orders;
        }

        public override int Count => orders.Length;

        public override Java.Lang.Object GetItem(int position)
        {
            return JsonConvert.SerializeObject(orders[position]);
        }

        public override long GetItemId(int position)
        {
            return orders[position].VehicleId;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            LinearLayout linearLayout;

            if (convertView == null)
            {
                linearLayout = new LinearLayout(context);
                linearLayout.Orientation = Orientation.Vertical;
                TextView tvOrderInfo = new TextView(context)
                {
                    TextSize = 20,
                    Text = $" {position + 1}. Data zlecenia: {orders[position].CreateDate} Status zlecenia: {((OrderStatusId)orders[position].OrderStatusId).GetDescription()}"
                };
                linearLayout.AddView(tvOrderInfo);
            }
            else
            {
                linearLayout = (LinearLayout)convertView;
            }

            return linearLayout;
        }
    }
}