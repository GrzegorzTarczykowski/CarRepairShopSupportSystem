using Android.App;
using Android.Content;
using Android.Support.V7.App;
using CarRepairShopSupportSystem.BLL.Models;
using System;

namespace CarRepairShopSupportSystem.Handler
{
    internal class DownloadHandler
    {
        private readonly AppCompatActivity activity;

        public DownloadHandler(AppCompatActivity activity)
        {
            this.activity = activity;
        }

        internal void DownloadApkAndRedirectToInstallation()
        {
            try
            {
                string destinationPath 
                    = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads) + "/WMSkanerAndroid.apk";
                DeleteExistingFile(destinationPath);
                Android.Net.Uri uri = Android.Net.Uri.FromFile(new Java.IO.File(destinationPath));
                DownloadManager manager = (DownloadManager)activity.GetSystemService(Context.DownloadService);
                long downloadId = SetDownloadRequestToDownloadManager(uri, manager);
                SetBroadcastReceiverToInstallAppWhenDownloadIsComplete(
                    Android.Support.V4.Content.FileProvider.GetUriForFile(
                        activity, activity.ApplicationContext.PackageName + ".provider", new Java.IO.File(destinationPath))
                , manager, downloadId);
            }
            catch (Java.Lang.SecurityException sex)
            {
                //TODO GT obsluga braku uprawnien
            }
            catch (Exception ex)
            {
            }
        }

        private long SetDownloadRequestToDownloadManager(Android.Net.Uri uri, DownloadManager manager)
        {
            DownloadManager.Request request = new DownloadManager.Request(Android.Net.Uri.Parse(Setting.apkPath));
            request.SetDescription("Plik instalacyjny CarRepairShopSupportSystem");
            request.SetTitle("CarRepairShopSupportSystem");
            request.SetDestinationUri(uri);
            return manager.Enqueue(request);
        }

        private void SetBroadcastReceiverToInstallAppWhenDownloadIsComplete(Android.Net.Uri uri, DownloadManager manager, long downloadId)
        {
            DownloadReceiver onComplete = new DownloadReceiver(uri, manager, downloadId, activity);
            activity.RegisterReceiver(onComplete, new IntentFilter(DownloadManager.ActionDownloadComplete));
        }

        private void DeleteExistingFile(string destinationPath)
        {
            //TODO GT do zweryfikowania działanie, bo cos nie działa
            Java.IO.File file = new Java.IO.File(destinationPath);
            if (file.Exists())
            {
                file.Delete();
            }
            file.SetReadable(true, false);
        }
    }
}
