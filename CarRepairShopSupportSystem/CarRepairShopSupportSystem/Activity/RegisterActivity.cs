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

namespace CarRepairShopSupportSystem.Activity
{
    [Activity(Label = "Rejestracja")]
    public class RegisterActivity : AppCompatActivity
    {
        private readonly IRegisterService registerService;
        private readonly IEmailService emailService;
        public RegisterActivity()
        {
            registerService = new RegisterService(new HttpClientService(new AccessTokenService(new ApplicationSessionService(), new TokenService())), new ApplicationSessionService());
            emailService = new EmailService();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_register);
            FindViewById<Button>(Resource.Id.btnRegister).Click += BtnRegister_Click;
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FindViewById<EditText>(Resource.Id.editTextUsername).Text))
            {
                Toast.MakeText(Application.Context, "Uzupełnij nazwe użytkownika", ToastLength.Long).Show();
            }
            else if (string.IsNullOrWhiteSpace(FindViewById<EditText>(Resource.Id.editTextFirstName).Text))
            {
                Toast.MakeText(Application.Context, "Uzupełnij imię", ToastLength.Long).Show();
            }
            else if (string.IsNullOrWhiteSpace(FindViewById<EditText>(Resource.Id.editTextLastName).Text))
            {
                Toast.MakeText(Application.Context, "Uzupełnij nazwiesko", ToastLength.Long).Show();
            }
            else if (string.IsNullOrWhiteSpace(FindViewById<EditText>(Resource.Id.editTextEmail).Text))
            {
                Toast.MakeText(Application.Context, "Uzupełnij email", ToastLength.Long).Show();
            }
            else if (!emailService.IsValid(FindViewById<EditText>(Resource.Id.editTextEmail).Text))
            {
                Toast.MakeText(Application.Context, "Niepoprawny mail", ToastLength.Long).Show();
            }
            else if (string.IsNullOrWhiteSpace(FindViewById<EditText>(Resource.Id.editTextPhoneNumber).Text))
            {
                Toast.MakeText(Application.Context, "Uzupełnij numer telefonu", ToastLength.Long).Show();
            }
            else if (string.IsNullOrWhiteSpace(FindViewById<EditText>(Resource.Id.editTextPassword).Text))
            {
                Toast.MakeText(Application.Context, "Uzupełnij hasło", ToastLength.Long).Show();
            }
            else if (string.IsNullOrWhiteSpace(FindViewById<EditText>(Resource.Id.editTextConfirmPassword).Text))
            {
                Toast.MakeText(Application.Context, "Uzupełnij powtorne hasło", ToastLength.Long).Show();
            }
            else if (FindViewById<EditText>(Resource.Id.editTextPassword).Text 
                != FindViewById<EditText>(Resource.Id.editTextConfirmPassword).Text)
            {
                Toast.MakeText(Application.Context, "Hasła są rózne", ToastLength.Long).Show();
            }
            else
            {
                User user = new User()
                {
                    Username = FindViewById<EditText>(Resource.Id.editTextUsername).Text,
                    Password = FindViewById<EditText>(Resource.Id.editTextPassword).Text,
                    FirstName = FindViewById<EditText>(Resource.Id.editTextFirstName).Text,
                    LastName = FindViewById<EditText>(Resource.Id.editTextLastName).Text,
                    Email = FindViewById<EditText>(Resource.Id.editTextEmail).Text,
                    PhoneNumber = FindViewById<EditText>(Resource.Id.editTextPhoneNumber).Text
                };

                OperationResult operationResult = registerService.Register(user);
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