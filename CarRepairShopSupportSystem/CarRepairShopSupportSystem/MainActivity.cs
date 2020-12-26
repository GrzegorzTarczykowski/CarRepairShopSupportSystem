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
using CarRepairShopSupportSystem.BLL.Enums;
using CarRepairShopSupportSystem.BLL.Extensions;
using System.Threading;
using Android.Graphics;
using Android.Locations;

namespace CarRepairShopSupportSystem
{
    [Activity(Label = "Auto Centrum", Theme = "@style/AppTheme")]
    public class MainActivity : AppCompatActivity, ILocationListener
    {
        private const int menuRequestCode = 1;
        private const int registerRequestCode = 2;
        private readonly ILoginService loginService;
        private readonly IApplicationSessionService applicationSessionService;

        LocationManager locationManager;
        string provider;

        public MainActivity()
        {
            loginService = new LoginService(new HttpClientService(new AccessTokenService(new ApplicationSessionService(), new TokenService())), new ApplicationSessionService());
            applicationSessionService = new ApplicationSessionService();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            AppDomain.CurrentDomain.UnhandledException += ExceptionHandler.LogAppDomainUnhandledException;
            TaskScheduler.UnobservedTaskException += ExceptionHandler.LogTaskSchedulerUnhandledException;

            FindViewById<Android.Support.V7.Widget.AppCompatTextView>(Resource.Id.txtRegister).Click += TxtViewRegister_Click;
            Button button = FindViewById<Button>(Resource.Id.btnLogin);
            button.Click += BtnLogin_Click;

            //ICON
            //Android.Graphics.Typeface font = Android.Graphics.Typeface.CreateFromAsset(Assets, "fa-regular-400.ttf");
            button.Typeface = Typeface.CreateFromAsset(Application.Assets, "fa-regular-400.ttf");
            //button.SetTypeface(font, Android.Graphics.TypefaceStyle.Normal);

            //LOCATION
            locationManager = (LocationManager)GetSystemService(LocationService);
            provider = locationManager.GetBestProvider(new Criteria(), false);
            Location location = locationManager.GetLastKnownLocation(provider);
            FindViewById<TextView>(Resource.Id.tvMyLastLocation).Text = $"{provider}: {location?.Latitude} - {location?.Longitude}";
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(delegate
            {
                if (loginService.Login(FindViewById<EditText>(Resource.Id.editTextLogin).Text
                    , FindViewById<EditText>(Resource.Id.editTextPassword).Text))
                {
                    Intent nextActivity = new Intent(this, typeof(MenuActivity));
                    nextActivity.PutExtra("HellowMessage", "Hellow world");
                    StartActivityForResult(nextActivity, menuRequestCode);
                }
                else
                {
                    RunOnUiThread(delegate
                    {
                        Toast.MakeText(Application.Context, "Niepoprawne login lub hasło", ToastLength.Long).Show();
                    });
                }
            });
        }

        private void TxtViewRegister_Click(object sender, EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(RegisterActivity));
            nextActivity.PutExtra(nameof(OperationType), OperationType.Add.GetDescription());
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

        protected override void OnResume()
        {
            base.OnResume();
            locationManager.RequestLocationUpdates(provider, 400, 1, this);
        }

        protected override void OnPause()
        {
            base.OnPause();
            locationManager.RemoveUpdates(this);
        }

        public void OnLocationChanged(Location location)
        {
            FindViewById<TextView>(Resource.Id.tvMyLocation).Text = $"{location?.Provider}: {location?.Latitude} - {location?.Longitude}";
        }

        public void OnProviderDisabled(string provider)
        {
            //throw new NotImplementedException();
        }

        public void OnProviderEnabled(string provider)
        {
            //throw new NotImplementedException();
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
            //throw new NotImplementedException();
        }
    }
}