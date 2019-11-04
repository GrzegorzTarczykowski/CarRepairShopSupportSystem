using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;
using System;
using CarRepairShopSupportSystem.Handler;
using System.Threading.Tasks;
using CarRepairShopSupportSystem.Activity;
using CarRepairShopSupportSystem.BLL.Service;
using CarRepairShopSupportSystem.BLL.IService;

namespace CarRepairShopSupportSystem
{
    [Activity(Label = "Auto Centrum", /*Theme = "@style/AppTheme",*/ MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private const int menuRequestCode = 1;
        private const int registerRequestCode = 2;
        private readonly ILoginService loginService;

        public MainActivity()
        {
            loginService = new LoginService(new ApplicationSessionService(), new AccessTokenService(new ApplicationSessionService(), new TokenService()));
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            AppDomain.CurrentDomain.UnhandledException += ExceptionHandler.LogAppDomainUnhandledException;
            TaskScheduler.UnobservedTaskException += ExceptionHandler.LogTaskSchedulerUnhandledException;

            FindViewById<Button>(Resource.Id.btnLogin).Click += BtnLogin_Click;
            FindViewById<Android.Support.V7.Widget.AppCompatTextView>(Resource.Id.txtRegister).Click += TxtViewRegister_Click;
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            bool succesfull = loginService.Login(FindViewById<EditText>(Resource.Id.editTextLogin).Text, FindViewById<EditText>(Resource.Id.editTextPassword).Text);
            if (succesfull)
            {
                Intent nextActivity = new Intent(this, typeof(MenuActivity));
                nextActivity.PutExtra("HellowMessage", "Hellow world");
                StartActivityForResult(nextActivity, menuRequestCode);
            }
            else
            {
                Toast.MakeText(Application.Context, "Niepoprawne login lub hasło", ToastLength.Long).Show();
            }
        }

        private void TxtViewRegister_Click(object sender, EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(RegisterActivity));
            StartActivityForResult(nextActivity, registerRequestCode);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == menuRequestCode)
            {
                loginService.Logout();
                Toast.MakeText(Application.Context, "Do zobaczenia", ToastLength.Long).Show();
            }
            else if (requestCode == registerRequestCode)
            {
                if (resultCode == Result.Ok)
                {
                    Toast.MakeText(Application.Context, "Pomyślnie zarejstrowano użytkownika", ToastLength.Long).Show();
                }
            }
        }
    }
}