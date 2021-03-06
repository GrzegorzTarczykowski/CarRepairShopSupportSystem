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
using CarRepairShopSupportSystem.BLL.Models;
using Java.Lang;
using Newtonsoft.Json;

namespace CarRepairShopSupportSystem.Adapter
{
    public class TimetableAdapter : BaseAdapter
    {
        private readonly Context context;
        private readonly IList<TimetablePerHour> timetablePerHourList;
        private readonly int? selectedPosition;

        public TimetableAdapter(Context context, IList<TimetablePerHour> timetablePerHourList, int? selectedPosition)
        {
            this.context = context;
            this.timetablePerHourList = timetablePerHourList;
            this.selectedPosition = selectedPosition;
        }

        public override int Count => timetablePerHourList.Count;

        public override Java.Lang.Object GetItem(int position)
        {
            return JsonConvert.SerializeObject(null);
        }

        public override long GetItemId(int position)
        {
            return timetablePerHourList[position].Hour;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            TextView textView = new TextView(context);
            TimetablePerHour timetablePerHour = timetablePerHourList[position];
            textView.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
            textView.Text = $"      Godzina: {timetablePerHour.Hour}:00";
            if (timetablePerHour.IsAvailableEmployeesForCustomer)
            {
                if (position == selectedPosition)
                {
                    textView.SetBackgroundColor(Android.Graphics.Color.Blue);
                }
                else
                {
                    textView.SetBackgroundColor(Android.Graphics.Color.Green);
                }
            }
            else
            {
                textView.Clickable = false;
                textView.SetBackgroundColor(Android.Graphics.Color.Red);
            }

            return textView;
        }
    }
}