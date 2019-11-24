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
        private readonly IRegularExpressionService regularExpressionService;
        public RegisterActivity()
        {
            registerService = new RegisterService(new HttpClientService(new AccessTokenService(new ApplicationSessionService(), new TokenService())), new ApplicationSessionService());
            emailService = new EmailService();
            regularExpressionService = new RegularExpressionService();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_register);
            FindViewById<Button>(Resource.Id.btnRegister).Click += BtnRegister_Click;
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            string textUsername = FindViewById<EditText>(Resource.Id.editTextUsername).Text;
            string textFirstName = FindViewById<EditText>(Resource.Id.editTextFirstName).Text;
            string textLastName = FindViewById<EditText>(Resource.Id.editTextLastName).Text;
            string textEmail = FindViewById<EditText>(Resource.Id.editTextEmail).Text;
            string textPhoneNumber = FindViewById<EditText>(Resource.Id.editTextPhoneNumber).Text;
            string textPassword = FindViewById<EditText>(Resource.Id.editTextPassword).Text;
            string textConfirmPassword = FindViewById<EditText>(Resource.Id.editTextConfirmPassword).Text;

            if (string.IsNullOrWhiteSpace(textUsername))
            {
                Toast.MakeText(Application.Context, "Uzupełnij nazwe użytkownika", ToastLength.Long).Show();
            }
            else if (string.IsNullOrWhiteSpace(textFirstName))
            {
                Toast.MakeText(Application.Context, "Uzupełnij imię", ToastLength.Long).Show();
            }
            else if (!regularExpressionService.IsMatchOnlyAlphabeticCharacters(textFirstName))
            {
                Toast.MakeText(Application.Context, "Imię musi posiadać wyłacznie litry", ToastLength.Long).Show();
            }
            else if (string.IsNullOrWhiteSpace(textLastName))
            {
                Toast.MakeText(Application.Context, "Uzupełnij nazwiesko", ToastLength.Long).Show();
            }
            else if (!regularExpressionService.IsMatchOnlyAlphabeticCharacters(textLastName))
            {
                Toast.MakeText(Application.Context, "Nazwisko musi posiadać wyłacznie litry", ToastLength.Long).Show();
            }
            else if (string.IsNullOrWhiteSpace(textEmail))
            {
                Toast.MakeText(Application.Context, "Uzupełnij email", ToastLength.Long).Show();
            }
            else if (!emailService.IsValid(textEmail))
            {
                Toast.MakeText(Application.Context, "Niepoprawny mail", ToastLength.Long).Show();
            }
            else if (string.IsNullOrWhiteSpace(textPhoneNumber))
            {
                Toast.MakeText(Application.Context, "Uzupełnij numer telefonu", ToastLength.Long).Show();
            }
            else if (!int.TryParse(textPhoneNumber, out int phoneNumber))
            {
                Toast.MakeText(Application.Context, "Numer telefonu musi być liczbą", ToastLength.Long).Show();
            }
            else if (textPhoneNumber.Length > 9)
            {
                Toast.MakeText(Application.Context, "Numer telefonu może mieć maksymalnie 9 cyfr", ToastLength.Long).Show();
            }
            else if (string.IsNullOrWhiteSpace(textPassword))
            {
                Toast.MakeText(Application.Context, "Uzupełnij hasło", ToastLength.Long).Show();
            }
            else if (string.IsNullOrWhiteSpace(textConfirmPassword))
            {
                Toast.MakeText(Application.Context, "Uzupełnij powtorne hasło", ToastLength.Long).Show();
            }
            else if (textPassword != textConfirmPassword)
            {
                Toast.MakeText(Application.Context, "Hasła są rózne", ToastLength.Long).Show();
            }
            else
            {
                User user = new User()
                {
                    Username = textUsername,
                    Password = textPassword,
                    FirstName = textFirstName,
                    LastName = textLastName,
                    Email = textEmail,
                    PhoneNumber = phoneNumber
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