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

namespace CarRepairShopSupportSystem.Adapter
{
    internal class SpinKeyValuePairAdapter : ArrayAdapter<string>
    {
        private readonly Context context;
        private readonly KeyValuePair<long, string>[] keyValuePair;

        public SpinKeyValuePairAdapter(Context context, KeyValuePair<long, string>[] keyValuePair) 
            : base(context, Android.Resource.Layout.SimpleSpinnerItem, keyValuePair.Select(kvp => kvp.Value).ToArray())
        {
            this.context = context;
            this.keyValuePair = keyValuePair;
        }

        public override long GetItemId(int position)
        {
            return keyValuePair[position].Key;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            TextView dummyTextView = new TextView(context);
            dummyTextView.Text = keyValuePair[position].Value;
            return dummyTextView;
        }
    }
}