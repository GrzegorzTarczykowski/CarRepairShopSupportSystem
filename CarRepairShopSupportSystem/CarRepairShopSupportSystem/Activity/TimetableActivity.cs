using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Autofac;
using CarRepairShopSupportSystem.Adapter;
using CarRepairShopSupportSystem.BLL.Enums;
using CarRepairShopSupportSystem.BLL.Extensions;
using CarRepairShopSupportSystem.BLL.IService;
using CarRepairShopSupportSystem.BLL.Models;
using Newtonsoft.Json;

namespace CarRepairShopSupportSystem.Activity
{
    [Activity(Label = "Harmonogram")]
    public class TimetableActivity : AppCompatActivity
    {
        private readonly ITimetableService timetableService;
        IEnumerable<TimetablePerDay> timetablesPerDayEnumer;
        DateTime selectedDateTime;
        GridView gvCalendar;
        GridView gvTimetable;

        public TimetableActivity()
        {
            timetableService = MainApplication.Container.Resolve<ITimetableService>();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_timetable);
            gvCalendar = (GridView)FindViewById(Resource.Id.gvCalendar);
            gvCalendar.ItemClick += GvCalendar_ItemClick;
            gvTimetable = (GridView)FindViewById(Resource.Id.gvTimetable);
            gvTimetable.ItemClick += GvTimetable_ItemClick;

            DateTime dateTimeNow = DateTime.Now;
            Spinner spinnerMonth = FindViewById<Spinner>(Resource.Id.spinnerMonth);
            List<DateTime> dateTimeList = new List<DateTime> { dateTimeNow, dateTimeNow.AddMonths(1), dateTimeNow.AddMonths(2) };
            spinnerMonth.Adapter = new SpinKeyValuePairAdapter(this, dateTimeList.ToDictionary(dtl => dtl.Ticks, dtl => $"{((Month)dtl.Month).GetDescription()} {dtl.Year}").ToArray());
            spinnerMonth.ItemSelected += SpinnerMonth_ItemSelected;
            Button btnSubmitSelectedTimetable = FindViewById<Button>(Resource.Id.btnSubmitSelectedTimetable);
            btnSubmitSelectedTimetable.Click += BtnSubmitSelectedTimetable_Click;
        }

        private void BtnSubmitSelectedTimetable_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(OrderEditorActivity));
            intent.PutExtra("SelectedDateTime", JsonConvert.SerializeObject(selectedDateTime));
            SetResult(Result.Ok, intent);
            Finish();
        }

        private void SpinnerMonth_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            selectedDateTime = new DateTime(e.Id);
            SetGvCalendarAdapter(null);
            gvTimetable.Adapter = null;
            TextView tvSelectedTimetable = FindViewById<TextView>(Resource.Id.tvSelectedTimetable);
            tvSelectedTimetable.Visibility = ViewStates.Invisible;
            FindViewById<Button>(Resource.Id.btnSubmitSelectedTimetable).Enabled = false;
        }

        private void SetGvCalendarAdapter(int? selectedPosition)
        {
            timetablesPerDayEnumer = timetableService.GetTimetableListPerMonth(selectedDateTime.Year, selectedDateTime.Month)
                                                     .Where(x => x.DateTime >= DateTime.Now)
                                                     .GroupBy(x => x.DateTime.Day)
                                                     .Select(x => new TimetablePerDay
                                                     {
                                                         Day = x.Key,
                                                         SumNumberOfEmployeesForCustomer = x.Sum(item => item.NumberOfEmployeesForCustomer),
                                                         SumNumberOfEmployeesReservedForCustomer = x.Sum(item => item.NumberOfEmployeesReservedForCustomer),
                                                         SumNumberOfEmployeesForManager = x.Sum(item => item.NumberOfEmployeesForManager),
                                                         SumNumberOfEmployeesReservedForManager = x.Sum(item => item.NumberOfEmployeesReservedForManager),
                                                     });

            gvCalendar.Adapter = new CalendarAdapter(this, selectedDateTime, timetablesPerDayEnumer, selectedPosition);
        }

        private void GvCalendar_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            selectedDateTime = new DateTime(selectedDateTime.Year, selectedDateTime.Month, (int)e.Id);
            SetGvCalendarAdapter(e.Position);
            SetGvTimetableAdapter(null);
            e.View.SetBackgroundColor(Android.Graphics.Color.Blue);
            selectedDateTime = new DateTime(selectedDateTime.Year, selectedDateTime.Month, (int)e.Id);
            TextView tvSelectedTimetable = FindViewById<TextView>(Resource.Id.tvSelectedTimetable);
            tvSelectedTimetable.Visibility = ViewStates.Invisible;
            FindViewById<Button>(Resource.Id.btnSubmitSelectedTimetable).Enabled = false;
        }

        private void SetGvTimetableAdapter(int? selectedPosition)
        {
            List<TimetablePerHour> timetablePerHourList = timetableService.GetTimetableListPerDay(selectedDateTime.Year, selectedDateTime.Month, selectedDateTime.Day)
                                                                          .Where(x => x.DateTime >= DateTime.Now)
                                                                          .GroupBy(x => x.DateTime.Hour)
                                                                          .Select(x => new TimetablePerHour
                                                                          {
                                                                              Hour = x.Key,
                                                                              SumNumberOfEmployeesForCustomer = x.Sum(item => item.NumberOfEmployeesForCustomer),
                                                                              SumNumberOfEmployeesReservedForCustomer = x.Sum(item => item.NumberOfEmployeesReservedForCustomer),
                                                                              SumNumberOfEmployeesForManager = x.Sum(item => item.NumberOfEmployeesForManager),
                                                                              SumNumberOfEmployeesReservedForManager = x.Sum(item => item.NumberOfEmployeesReservedForManager),
                                                                          })
                                                                          .ToList();

            gvTimetable.Adapter = new TimetableAdapter(this, timetablePerHourList, selectedPosition);
        }

        private void GvTimetable_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            SetGvTimetableAdapter(e.Position);
            e.View.SetBackgroundColor(Android.Graphics.Color.Blue);
            selectedDateTime = new DateTime(selectedDateTime.Year, selectedDateTime.Month, selectedDateTime.Day, (int)e.Id, 0, 0);
            TextView tvSelectedTimetable = FindViewById<TextView>(Resource.Id.tvSelectedTimetable);
            tvSelectedTimetable.Text = selectedDateTime.ToString();
            tvSelectedTimetable.Visibility = ViewStates.Visible;
            FindViewById<Button>(Resource.Id.btnSubmitSelectedTimetable).Enabled = true;
        }
    }
}