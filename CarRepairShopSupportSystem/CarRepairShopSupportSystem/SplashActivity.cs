using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Widget;
using CarRepairShopSupportSystem.BLL.IService;
using CarRepairShopSupportSystem.BLL.Models;
using CarRepairShopSupportSystem.BLL.Service;
using CarRepairShopSupportSystem.Handler;
using System.Threading.Tasks;

namespace CarRepairShopSupportSystem
{
    [Activity(Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : AppCompatActivity
    {
        private readonly IAndroidPackageKitService androidPackageKitService;

        public SplashActivity()
        {
            androidPackageKitService = new AndroidPackageKitService(new HttpClientService(new AccessTokenService(new ApplicationSessionService(), new TokenService())));
        }

        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);
        }

        protected override void OnResume()
        {
            base.OnResume();

            //TODO GT uruchomic jedno pobieranie apk
            StartActivity(new Intent(Application.Context, typeof(MainActivity))); //TODO to usunac

            //Task startupWork = new Task(delegate
            //{
            //    PermissionHandler permissionHandler = new PermissionHandler(this);
            //    if (permissionHandler.CheckPermissionsToWriteExternalStorage())
            //    {
            //        DownloadApkAndRedirectToInstallation();
            //    }
            //    else
            //    {
            //        permissionHandler.RequestPermissionsToWriteExternalStorage();
            //    }
            //});
            //startupWork.Start();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            if (grantResults.Length > 0 && grantResults[0] == Permission.Denied)
            {
                RunOnUiThread(delegate
                {
                    Toast.MakeText(ApplicationContext, "Aplikacja nie może działać bez dostepu do pamięci urządzenia", ToastLength.Long).Show();
                });
            }
            else
            {
                DownloadApkAndRedirectToInstallation();
            }
        }

        private void DownloadApkAndRedirectToInstallation()
        {
            ApplicationSession.userName = "TestGuest";
            ApplicationSession.userPassword = "1";
            long versionCode = PackageManager.GetPackageInfo(PackageName, 0).LongVersionCode;
            if (versionCode < androidPackageKitService.GetApkVersion())
            {
                RunOnUiThread(delegate
                {
                    Toast.MakeText(ApplicationContext, "Znaleziono nową aktualizacjie, rozpoczęto pobieranie, proszę czekać...", ToastLength.Long).Show();
                });
                DownloadHandler downloadHandler = new DownloadHandler(this);
                downloadHandler.DownloadApkAndRedirectToInstallation();
            }
            else
            {
                StartActivity(new Intent(Application.Context, typeof(MainActivity)));
            }
        }
    }

}
