using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Autofac;
using CarRepairShopSupportSystem.BLL.Enums;
using CarRepairShopSupportSystem.BLL.Extensions;
using CarRepairShopSupportSystem.BLL.IService;
using CarRepairShopSupportSystem.BLL.Models;
using Newtonsoft.Json;

namespace CarRepairShopSupportSystem.Activity
{
    [Activity(Label = "Konto")]
    public class RegisterActivity : AppCompatActivity
    {
        private readonly IUserService userService;
        private readonly IEmailService emailService;
        private readonly IRegularExpressionService regularExpressionService;
        private readonly IApplicationSessionService applicationSessionService;
        private int permissionId;
        private int userId;

        public RegisterActivity()
        {
            userService = MainApplication.Container.Resolve<IUserService>();
            emailService = MainApplication.Container.Resolve<IEmailService>();
            regularExpressionService = MainApplication.Container.Resolve<IRegularExpressionService>();
            applicationSessionService = MainApplication.Container.Resolve<IApplicationSessionService>();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_register);
            FindViewById<Button>(Resource.Id.btnRegister).Click += BtnRegister_Click;
            if (Intent.GetStringExtra(nameof(OperationType)) == OperationType.Edit.GetDescription())
            {
                User user = JsonConvert.DeserializeObject<User>(Intent.GetStringExtra("SelectedWorker") ?? string.Empty) 
                    ?? applicationSessionService.GetUserFromApplicationSession();
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
                permissionId = user.PermissionId;
                userId = user.UserId;

                FindViewById<Button>(Resource.Id.btnRegister).Text = "Zapisz zmiany";
            }
            else
            {
                if (applicationSessionService.GetUserFromApplicationSession()?.PermissionId == (int)PermissionId.SuperAdmin)
                {
                    permissionId = (int)PermissionId.Admin;
                }
                else
                {
                    permissionId = (int)PermissionId.User;
                }
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
            else if (textPassword.Length < 6)
            {
                Toast.MakeText(Application.Context, "Hasło musi mięc co najmniej 6 znaków", ToastLength.Long).Show();
            }
            else if (!textPassword.Any(char.IsDigit))
            {
                Toast.MakeText(Application.Context, "Hasło musi zawierać cyfry", ToastLength.Long).Show();
            }
            else if (!textPassword.Any(char.IsLetter))
            {
                Toast.MakeText(Application.Context, "Hasło musi zawierać litery", ToastLength.Long).Show();
            }
            else if (!textPassword.Any(char.IsUpper))
            {
                Toast.MakeText(Application.Context, "Hasło musi zawierać duże litery", ToastLength.Long).Show();
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
                    PhoneNumber = phoneNumber,
                    PermissionId = permissionId
                };

                OperationResult operationResult = null;
                if (Intent.GetStringExtra(nameof(OperationType)) == OperationType.Edit.GetDescription())
                {
                    user.UserId = userId;
                    operationResult = userService.EditUser(user); 
                    if (operationResult.ResultCode == ResultCode.Successful)
                    {
                        userService.GetUser(user.Username, user.Password);
                    }
                }
                else
                {
                    if (applicationSessionService.GetUserFromApplicationSession() == null)
                    {
                        ApplicationSession.userName = "TestGuest";
                        ApplicationSession.userPassword = "1";
                        operationResult = userService.AddUser(user);
                        applicationSessionService.ClearApplicationSession();
                    }
                    else
                    {
                        operationResult = userService.AddUser(user);
                    }
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