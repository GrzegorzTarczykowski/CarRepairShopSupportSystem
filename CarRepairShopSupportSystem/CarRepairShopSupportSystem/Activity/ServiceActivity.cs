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
using CarRepairShopSupportSystem.BLL.Enums;
using CarRepairShopSupportSystem.BLL.IService;
using CarRepairShopSupportSystem.BLL.Models;
using CarRepairShopSupportSystem.BLL.Service;
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
            serviceService = new ServiceService(new HttpClientService(new AccessTokenService(new ApplicationSessionService(), new TokenService())));
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
            else
            {
                OperationResult operationResult = null;
                if (selectedService != null)
                {
                    selectedService.Name = textServiceName;
                    selectedService.Description = textServiceDescription;
                    selectedService.Price = servicePrice;
                    operationResult = serviceService.EditService(selectedService);
                }
                else
                {
                    selectedService = new BLL.Models.Service
                    {
                        Name = textServiceName,
                        Description = textServiceDescription,
                        Price = servicePrice
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