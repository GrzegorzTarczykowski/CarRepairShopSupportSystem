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
using Java.Lang;
using Newtonsoft.Json;

namespace CarRepairShopSupportSystem.Adapter
{
    public class TimetableAdapter : BaseAdapter
    {
        private readonly Context context;

        public TimetableAdapter(Context context)
        {
            this.context = context;
        }

        public override int Count => 8;

        public override Java.Lang.Object GetItem(int position)
        {
            return JsonConvert.SerializeObject(2);
        }

        public override long GetItemId(int position)
        {
            return 3;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            TextView textView;

            if (convertView == null)
            {
                textView = new TextView(context);
                textView.Text = position.ToString();
            }
            else
            {
                textView = (TextView)convertView;
            }

            return textView;
        }
    }
}