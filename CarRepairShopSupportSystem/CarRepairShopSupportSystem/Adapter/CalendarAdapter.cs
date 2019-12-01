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
using CarRepairShopSupportSystem.BLL.Models;
using Java.Lang;
using Newtonsoft.Json;

namespace CarRepairShopSupportSystem.Adapter
{
    public class CalendarAdapter : BaseAdapter
    {
        private readonly Context context;
        private readonly IEnumerable<TimetablePerDay> timetablePerDayEnumer;
        private readonly int daysInMonth;
        private readonly DayOfWeek dayOfWeekFirstDayOfMouth;

        public CalendarAdapter(Context context, DateTime dateTime, IEnumerable<TimetablePerDay> timetablePerDayEnumer)
        {
            Calendar calendar = new GregorianCalendar();
            daysInMonth = calendar.GetDaysInMonth(dateTime.Year, dateTime.Month);
            DateTime dateTimeFirstDayOfMouth = new DateTime(dateTime.Year, dateTime.Month, 1);
            dayOfWeekFirstDayOfMouth = calendar.GetDayOfWeek(dateTimeFirstDayOfMouth);
            this.context = context;
            this.timetablePerDayEnumer = timetablePerDayEnumer;
        }

        public override int Count => (daysInMonth + (int)dayOfWeekFirstDayOfMouth);

        public override Java.Lang.Object GetItem(int position)
        {
            return JsonConvert.SerializeObject(null);
        }

        public override long GetItemId(int position)
        {
            return (1 + position - (int)dayOfWeekFirstDayOfMouth);
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
                    textView.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                    if (timetablePerDayEnumer.FirstOrDefault(tpd => tpd.Day == (1 + position - (int)dayOfWeekFirstDayOfMouth))?.IsAvailableEmployeesForCustomer ?? false)
                    {
                        textView.SetBackgroundColor(Android.Graphics.Color.Green);
                    }
                    else
                    {
                        textView.Clickable = false;
                        textView.SetBackgroundColor(Android.Graphics.Color.Red);
                    }
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