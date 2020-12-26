using Android.Support.V4.Content;
using Android.Support.V7.App;
using System;

namespace CarRepairShopSupportSystem.Handler
{
    internal class PermissionHandler
    {
        private readonly AppCompatActivity activity;

        public PermissionHandler(AppCompatActivity activity)
        {
            this.activity = activity;
        }

        public bool CheckPermissionsToWriteExternalStorage()
        {
            return ContextCompat.CheckSelfPermission(activity, Android.Manifest.Permission.WriteExternalStorage) == Android.Content.PM.Permission.Granted;
        }

        public void RequestPermissionsToWriteExternalStorage()
        {
            const int requestCode = 1;
            var requiredPermissions = new string[] { Android.Manifest.Permission.WriteExternalStorage };
            if (Android.Support.V4.App.ActivityCompat.ShouldShowRequestPermissionRationale(activity, Android.Manifest.Permission.WriteExternalStorage))
            {
                Android.Support.Design.Widget.Snackbar
                    .Make(activity.FindViewById(Android.Resource.Id.Content), "Czy nadać uprawnienia tej aplikacji do pamięci urządzenia?", Android.Support.Design.Widget.Snackbar.LengthIndefinite)
                    .SetAction("OK", new Action<Android.Views.View>(delegate (Android.Views.View obj)
                    {
                        Android.Support.V4.App.ActivityCompat.RequestPermissions(activity, requiredPermissions, requestCode);
                    })).Show();
            }
            else
            {
                Android.Support.V4.App.ActivityCompat.RequestPermissions(activity, requiredPermissions, requestCode);
            }
        }
    }
}
