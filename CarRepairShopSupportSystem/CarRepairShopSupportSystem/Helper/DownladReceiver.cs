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

namespace CarRepairShopSupportSystem.Helper
{
    internal class DownladReceiver : BroadcastReceiver
    {
        private readonly Android.Net.Uri uri;
        private readonly DownloadManager manager;
        private readonly long downloadId;
        private readonly AppCompatActivity activity;

        public DownladReceiver(Android.Net.Uri uri, DownloadManager manager, long downloadId, AppCompatActivity activity)
        {
            this.uri = uri;
            this.manager = manager;
            this.downloadId = downloadId;
            this.activity = activity;
        }

        public override void OnReceive(Context context, Intent intent)
        {
            string installerPackageName = intent.GetStringExtra(Intent.ExtraInstallerPackageName);
            Intent install = new Intent(Intent.ActionView);
            install.PutExtra(Intent.ExtraInstallerPackageName, installerPackageName);
            install.SetFlags(ActivityFlags.ClearTop | ActivityFlags.NewTask | ActivityFlags.GrantReadUriPermission);
            install.SetDataAndType(uri, manager.GetMimeTypeForDownloadedFile(downloadId));
            context.StartActivity(install);
            context.UnregisterReceiver(this);
            activity.Finish();
        }
    }
}

