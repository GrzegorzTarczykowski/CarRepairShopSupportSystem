using System;
using System.Collections.Generic;
using System.Globalization;
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
    public class CalendarAdapter : BaseAdapter
    {
        private readonly Context context;
        private readonly int daysInMonth;
        private readonly DayOfWeek dayOfWeekFirstDayOfMouth;

        public CalendarAdapter(Context context)
        {
            Calendar calendar = new GregorianCalendar();
            daysInMonth = calendar.GetDaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            DateTime dateTimeFirstDayOfMouth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dayOfWeekFirstDayOfMouth = calendar.GetDayOfWeek(dateTimeFirstDayOfMouth);
            this.context = context;
        }

        public override int Count => (daysInMonth + (int)dayOfWeekFirstDayOfMouth);

        public override Java.Lang.Object GetItem(int position)
        {
            return JsonConvert.SerializeObject(8);
        }

        public override long GetItemId(int position)
        {
            return 6;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            TextView textView;

            if (convertView == null)
            {
                textView = new TextView(context);
                if (position >= (int)dayOfWeekFirstDayOfMouth)
                {
                    textView.Text = (1 + position - (int)dayOfWeekFirstDayOfMouth).ToString();
                }
            }
            else
            {
                textView = (TextView)convertView;
            }

            return textView;
        }
    }
}