using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using CarRepairShopSupportSystem.Adapter;
using CarRepairShopSupportSystem.BLL.Enums;
using CarRepairShopSupportSystem.BLL.Extensions;
using CarRepairShopSupportSystem.BLL.IService;
using CarRepairShopSupportSystem.BLL.Service;

namespace CarRepairShopSupportSystem.Activity
{
    [Activity(Label = "TimetableActivity")]
    public class TimetableActivity : AppCompatActivity
    {
        private readonly ITimetableService timetableService;
        GridView gvCalendar;
        GridView gvTimetable; 

        public TimetableActivity()
        {
            timetableService = new TimetableService(new HttpClientService(new AccessTokenService(new ApplicationSessionService(), new TokenService())));
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_timetable);

            Spinner spinnerMonth = FindViewById<Spinner>(Resource.Id.spinnerMonth);
            List<int> monthList = new List<int> { DateTime.Now.Month, DateTime.Now.Month + 1, DateTime.Now.Month + 2 };
            spinnerMonth.Adapter = new SpinKeyValuePairAdapter(this, monthList.ToDictionary(ml => ml, ml => ((Month)ml).GetDescription()).ToArray());

            gvCalendar = (GridView)FindViewById(Resource.Id.gvCalendar);
            gvCalendar.Adapter = new CalendarAdapter(this);
            gvCalendar.ItemClick += GvCalendar_ItemClick;
            gvTimetable = (GridView)FindViewById(Resource.Id.gvTimetable);
            gvTimetable.Adapter = new TimetableAdapter(this);
            gvTimetable.ItemClick += GvTimetable_ItemClick;
        }

        private void GvTimetable_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {

        }

        private void GvCalendar_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {

        }
    }
}