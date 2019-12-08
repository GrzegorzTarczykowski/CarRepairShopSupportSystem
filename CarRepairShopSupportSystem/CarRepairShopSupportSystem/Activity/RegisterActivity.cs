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
using CarRepairShopSupportSystem.BLL.Extensions;
using CarRepairShopSupportSystem.BLL.IService;
using CarRepairShopSupportSystem.BLL.Models;
using CarRepairShopSupportSystem.BLL.Service;

namespace CarRepairShopSupportSystem.Activity
{
    [Activity(Label = "Konto")]
    public class RegisterActivity : AppCompatActivity
    {
        private readonly IUserService userService;
        private readonly IEmailService emailService;
        private readonly IRegularExpressionService regularExpressionService;
        private readonly IApplicationSessionService applicationSessionService;

        public RegisterActivity()
        {
            userService = new UserService(new HttpClientService(new AccessTokenService(new ApplicationSessionService(), new TokenService())));
            emailService = new EmailService();
            regularExpressionService = new RegularExpressionService();
            applicationSessionService = new ApplicationSessionService();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_register);
            FindViewById<Button>(Resource.Id.btnRegister).Click += BtnRegister_Click;
            if (Intent.GetStringExtra(nameof(OperationType)) == OperationType.Edit.GetDescription())
            {
                User user = applicationSessionService.GetUserFromApplicationSession();
                FindViewById<EditText>(Resource.Id.editTextUsername).Enabled = false;
                FindViewById<EditText>(Resource.Id.editTextFirstName).Enabled = false;
                FindViewById<EditText>(Resource.Id.editTextLastName).Enabled = false;
                
                FindViewById<TextView>(Resource.Id.tvUserDetailsTitle).Text = "Dane o koncie:";
                FindViewById<EditText>(Resource.Id.editTextUsername).Text = user.Username;
                FindViewById<EditText>(Resource.Id.editTextFirstName).Text = user.FirstName;
                FindViewById<EditText>(Resource.Id.editTextLastName).Text = user.LastName;
                FindViewById<EditText>(Resource.Id.editTextEmail).Text = user.Email;
                FindViewById<EditText>(Resource.Id.editTextPhoneNumber).Text = user.PhoneNumber.ToString();
                FindViewById<EditText>(Resource.Id.editTextPassword).Text = user.Password;
                FindViewById<EditText>(Resource.Id.editTextConfirmPassword).Text = user.Password;

                FindViewById<Button>(Resource.Id.btnRegister).Text = "Zapisz zmiany";
            }
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
                Toast.MakeText(Application.Context, "Uzupełnij nazwę użytkownika", ToastLength.Long).Show();
            }
            else if (string.IsNullOrWhiteSpace(textFirstName))
            {
                Toast.MakeText(Application.Context, "Uzupełnij imię", ToastLength.Long).Show();
            }
            else if (!regularExpressionService.IsMatchOnlyAlphabeticCharacters(textFirstName))
            {
                Toast.MakeText(Application.Context, "Imię musi posiadać wyłacznie litery", ToastLength.Long).Show();
            }
            else if (string.IsNullOrWhiteSpace(textLastName))
            {
                Toast.MakeText(Application.Context, "Uzupełnij nazwisko", ToastLength.Long).Show();
            }
            else if (!regularExpressionService.IsMatchOnlyAlphabeticCharacters(textLastName))
            {
                Toast.MakeText(Application.Context, "Nazwisko musi posiadać wyłącznie litery", ToastLength.Long).Show();
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
                Toast.MakeText(Application.Context, "Uzupełnij powtórne hasło", ToastLength.Long).Show();
            }
            else if (textPassword != textConfirmPassword)
            {
                Toast.MakeText(Application.Context, "Hasła są różne", ToastLength.Long).Show();
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

                OperationResult operationResult = null;
                if (Intent.GetStringExtra(nameof(OperationType)) == OperationType.Edit.GetDescription())
                {
                    operationResult = userService.EditUser(user);
                }
                else
                {
                    ApplicationSession.userName = "TestGuest";
                    ApplicationSession.userPassword = "1";
                    operationResult = userService.AddUser(user);
                    applicationSessionService.ClearApplicationSession();
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