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
    class MessageAdapter : BaseAdapter
    {
        private readonly Context context;
        private readonly BLL.Models.Message[] messages;

        public MessageAdapter(Context context, BLL.Models.Message[] messages)
        {
            this.context = context;
            this.messages = messages;
        }

        public override int Count => messages.Length;

        public override Java.Lang.Object GetItem(int position)
        {
            return JsonConvert.SerializeObject(messages[position]);
        }

        public override long GetItemId(int position)
        {
            return messages[position].MessageId;
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
                    Text = $"  {position}. Status zlecenia: {messages[position].Content}"
                };
                linearLayout.AddView(tvBrandNameAndModelName);
            }
            else
            {
                linearLayout = (LinearLayout)convertView;
            }

            return linearLayout;
        }
    }
}