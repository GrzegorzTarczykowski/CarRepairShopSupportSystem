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
        private readonly int userId;

        public MessageAdapter(Context context, BLL.Models.Message[] messages, int userId)
        {
            this.context = context;
            this.messages = messages;
            this.userId = userId;
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
            LinearLayout linearLayout = new LinearLayout(context);
            linearLayout.Orientation = Orientation.Vertical;
            TextView tvMessageUserSender = new TextView(context)
            {
                TextSize = 15,
                Text = $" {messages[position].UserSenderFirstName} {messages[position].UserSenderLastName} {messages[position].SentDate}"
            };
            linearLayout.AddView(tvMessageUserSender);
            TextView tvMessageContent = new TextView(context)
            {
                TextSize = 20,
                Text = $"  {messages[position].Content}"
            };
            linearLayout.AddView(tvMessageContent);

            if (messages[position].UserSenderId == userId)
            {
                linearLayout.SetBackgroundColor(Android.Graphics.Color.Orange);
            }
            else
            {
                linearLayout.SetBackgroundColor(Android.Graphics.Color.Yellow);
            }

            return linearLayout;
        }
    }
}