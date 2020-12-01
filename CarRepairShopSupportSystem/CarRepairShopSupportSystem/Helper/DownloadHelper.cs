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
    internal class DownloadHelper
    {
        private readonly AppCompatActivity activity;

        public DownloadHelper(AppCompatActivity activity)
        {
            this.activity = activity;
        }

        internal void DownloadApk()
        {
            string destinationPath
                = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads) + "/CarRepairShopSupportSystem.apk";

            DeleteExistingFile(destinationPath);

            Android.Net.Uri uri = Android.Net.Uri.FromFile(new Java.IO.File(destinationPath));
            DownloadManager manager = (DownloadManager)activity.GetSystemService(Context.DownloadService);
            long downloadId = SetDownloadRequestToDownloadManager(uri, manager);
            SetBroadcastReceiverToInstallAppWhenDownloadIsComplete(uri, manager, downloadId);
        }

        private long SetDownloadRequestToDownloadManager(Android.Net.Uri uri, DownloadManager manager)
        {
            DownloadManager.Request request = new DownloadManager.Request(Android.Net.Uri.Parse("http://10.0.2.2:7575"));
            request.SetDescription("Plik instalacyjny CarRepairShopSupportSystem");
            request.SetTitle("CarRepairShopSupportSystem");
            request.SetDestinationUri(uri);
            return manager.Enqueue(request);
        }

        private void SetBroadcastReceiverToInstallAppWhenDownloadIsComplete(Android.Net.Uri uri, DownloadManager manager, long downloadId)
        {
            DownladReceiver onComplete = new DownladReceiver(uri, manager, downloadId, activity);
            activity.RegisterReceiver(onComplete, new IntentFilter(DownloadManager.ActionDownloadComplete));
        }

        private void DeleteExistingFile(string destinationPath)
        {
            Java.IO.File file = new Java.IO.File(destinationPath);
            if (file.Exists())
            {
                file.Delete();
            }
            file.SetReadable(true, false);
        }
    }
}