﻿using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Autofac;
using CarRepairShopSupportSystem.BLL.Enums;
using CarRepairShopSupportSystem.BLL.IService;
using CarRepairShopSupportSystem.BLL.Models;
using Newtonsoft.Json;

namespace CarRepairShopSupportSystem.Activity
{
    [Activity(Label = "Edytor usług")]
    public class ServiceActivity : AppCompatActivity
    {
        BLL.Models.Service selectedService;
        readonly IServiceService serviceService;

        public ServiceActivity()
        {
            serviceService = MainApplication.Container.Resolve<IServiceService>();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_service);
            selectedService = JsonConvert.DeserializeObject<BLL.Models.Service>(Intent.GetStringExtra("SelectedService") ?? string.Empty);
            Button btnSubmitService = FindViewById<Button>(Resource.Id.btnSubmitService);
            btnSubmitService.Click += BtnSubmitService_Click;
            if (selectedService != null)
            {
                btnSubmitService.Text = "Modyfikuj";
                FindViewById<EditText>(Resource.Id.editServiceName).Text = selectedService.Name;
                FindViewById<EditText>(Resource.Id.editServiceDescription).Text = selectedService.Description;
                FindViewById<EditText>(Resource.Id.editServicePrice).Text = selectedService.Price.ToString();
                FindViewById<EditText>(Resource.Id.etExecutionTimeInMinutes).Text = selectedService.ExecutionTimeInMinutes.ToString();
                FindViewById<Button>(Resource.Id.btnDeleteService).Click += BtnDeleteService_Click; 
            }
            else
            {
                btnSubmitService.Text = "Dodaj";
                FindViewById<Button>(Resource.Id.btnDeleteService).Visibility = ViewStates.Gone;
            }
        }

        private void BtnDeleteService_Click(object sender, EventArgs e)
        {
            OperationResult operationResult = serviceService.DeleteService(selectedService.ServiceId);

            if (operationResult.ResultCode == ResultCode.Successful)
            {
                SetResult(Result.Ok);
                this.Finish();
            }
            else
            {
                Toast.MakeText(Application.Context, operationResult.Message, ToastLength.Long).Show();
            }
        }

        private void BtnSubmitService_Click(object sender, EventArgs e)
        {
            string textServiceName = FindViewById<EditText>(Resource.Id.editServiceName).Text;
            string textServiceDescription = FindViewById<EditText>(Resource.Id.editServiceDescription).Text;
            string textServicePrice = FindViewById<EditText>(Resource.Id.editServicePrice).Text;
            string testExecutionTimeInMinutes = FindViewById<EditText>(Resource.Id.etExecutionTimeInMinutes).Text;

            if (string.IsNullOrWhiteSpace(textServiceName))
            {
                Toast.MakeText(Application.Context, "Uzupełnij nazwę usługi", ToastLength.Long).Show();
            }
            else if (string.IsNullOrWhiteSpace(textServiceDescription))
            {
                Toast.MakeText(Application.Context, "Uzupełnij opis usługi", ToastLength.Long).Show();
            }
            else if (string.IsNullOrWhiteSpace(textServicePrice))
            {
                Toast.MakeText(Application.Context, "Uzupełnij cenę", ToastLength.Long).Show();
            }
            else if (!decimal.TryParse(textServicePrice, out decimal servicePrice))
            {
                Toast.MakeText(Application.Context, "Nie poprawna cena", ToastLength.Long).Show();
            }
            else if (!int.TryParse(testExecutionTimeInMinutes, out int executionTimeInMinutes))
            {
                Toast.MakeText(Application.Context, "Nie poprawny czas wykonywania", ToastLength.Long).Show();
            }
            else
            {
                OperationResult operationResult;
                if (selectedService != null)
                {
                    selectedService.Name = textServiceName;
                    selectedService.Description = textServiceDescription;
                    selectedService.Price = servicePrice;
                    selectedService.ExecutionTimeInMinutes = executionTimeInMinutes;
                    operationResult = serviceService.EditService(selectedService);
                }
                else
                {
                    selectedService = new BLL.Models.Service
                    {
                        Name = textServiceName,
                        Description = textServiceDescription,
                        Price = servicePrice,
                        ExecutionTimeInMinutes = executionTimeInMinutes
                    };
                    operationResult = serviceService.AddService(selectedService);
                }

                if (operationResult.ResultCode == ResultCode.Successful)
                {
                    SetResult(Result.Ok);
                    this.Finish();
                }
                else
                {
                    Toast.MakeText(Application.Context, operationResult.Message, ToastLength.Long).Show();
                }
            }
        }
    }
}