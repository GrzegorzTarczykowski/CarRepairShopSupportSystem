using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using CarRepairShopSupportSystem.BLL.Enums;
using CarRepairShopSupportSystem.BLL.IService;
using CarRepairShopSupportSystem.BLL.Models;
using CarRepairShopSupportSystem.BLL.Service;
using Newtonsoft.Json;

namespace CarRepairShopSupportSystem.Activity
{
    [Activity(Label = "Zarządzanie pracownikiem")]
    public class WorkerManagerActivity : AppCompatActivity
    {
        private readonly ITimetableService timetableService;
        readonly List<DateTime> dateTimeMondays = new List<DateTime>();
        DateTime dateTimeMonday;
        readonly string[] workingHours = { "", "6:00 - 14:00", "7:00 - 15:00", "8:00 - 16:00", "9:00 - 17:00", "10:00 - 18:00" };
        User userWorker;
        IOrderedEnumerable<Timetable> timetablesPerYearByUser;
        Timetable selectedTimetable;

        public WorkerManagerActivity()
        {
            timetableService = new TimetableService(new HttpClientService(new AccessTokenService(new ApplicationSessionService(), new TokenService())));
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_workerManager);

            userWorker = JsonConvert.DeserializeObject<User>(Intent.GetStringExtra("SelectedWorker"));
            Button btnSaveWorkerTimetable = FindViewById<Button>(Resource.Id.btnSaveWorkerTimetable);
            btnSaveWorkerTimetable.Click += BtnSaveWorkerTimetable_Click;

            //////////
            Calendar calendar = new GregorianCalendar();

            DayOfWeek dayOfWeekFirstDayOfMouth = calendar.GetDayOfWeek(DateTime.Now);
            DateTime dateTimeMonday;
            if (dayOfWeekFirstDayOfMouth == DayOfWeek.Sunday)
            {
                dateTimeMonday = DateTime.Now.AddDays(1);
            }
            else if (dayOfWeekFirstDayOfMouth == DayOfWeek.Monday)
            {
                dateTimeMonday = DateTime.Now;
            }
            else
            {
                dateTimeMonday = DateTime.Now.AddDays((double)dayOfWeekFirstDayOfMouth - ((double)dayOfWeekFirstDayOfMouth * 2) + 1);
                
            }

            while (dateTimeMonday.Year == DateTime.Now.Year)
            {
                dateTimeMondays.Add(dateTimeMonday);
                dateTimeMonday = new DateTime(dateTimeMonday.Ticks).AddDays(7);
            }

            var dateTimeWeekAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem
                , dateTimeMondays.Select(dtm => $"{dtm.Day}.{dtm.Month}.{dtm.Year} - {dtm.AddDays(4).Day}.{dtm.AddDays(4).Month}.{dtm.AddDays(4).Year}").ToList());
            dateTimeWeekAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            Spinner spinnerWeekNumber = FindViewById<Spinner>(Resource.Id.spinnerWeekNumber);
            spinnerWeekNumber.ItemSelected += SpinnerWeekNumber_ItemSelected;
            spinnerWeekNumber.Adapter = dateTimeWeekAdapter;

            Spinner spinnerWorkStartHour = FindViewById<Spinner>(Resource.Id.spinnerWorkStartHour);
            var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, workingHours);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinnerWorkStartHour.Adapter = adapter;

            RefreshTimetablesPerYearByUser();
        }

        private void RefreshTimetablesPerYearByUser()
        {
            timetablesPerYearByUser = timetableService.GetTimetableListPerYearByUserId(DateTime.Now.Year, userWorker.UserId)
                                                      .OrderBy(x => x.DateTime.Ticks);
        }

        private void BtnSaveWorkerTimetable_Click(object sender, EventArgs e)
        {
            if (FindViewById<Spinner>(Resource.Id.spinnerWorkStartHour).SelectedItemPosition == 0)
            {
                Toast.MakeText(Application.Context, "Ustaw godziny pracy", ToastLength.Long).Show();
            }

            selectedTimetable.DateTime = new DateTime(dateTimeMonday.Year
                                                    , dateTimeMonday.Month
                                                    , dateTimeMonday.Day
                                                    , (FindViewById<Spinner>(Resource.Id.spinnerWorkStartHour).SelectedItemPosition + 5)
                                                    , 0
                                                    , 0);

            OperationResult operationResult = timetableService.SaveTimetableForUser(selectedTimetable);

            if (operationResult.ResultCode == ResultCode.Successful)
            {
                Toast.MakeText(Application.Context, "Pomyślnie zapisano czas pracy na dany tydzień", ToastLength.Long).Show();
            }
            else
            {
                Toast.MakeText(Application.Context, operationResult.Message, ToastLength.Long).Show();
            }
            RefreshTimetablesPerYearByUser();
        }

        private void SpinnerWeekNumber_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            dateTimeMonday = dateTimeMondays[e.Position];
            selectedTimetable = timetablesPerYearByUser.Where(g => g.DateTime.Day == dateTimeMonday.Day && g.DateTime.Month == dateTimeMonday.Month)
                                                   .FirstOrDefault();

            if (selectedTimetable == null)
            {
                FindViewById<Spinner>(Resource.Id.spinnerWorkStartHour).SetSelection(0);
            }
            else
            {
                FindViewById<Spinner>(Resource.Id.spinnerWorkStartHour).SetSelection(selectedTimetable.DateTime.Hour - 5);
            }
        }
    }
}