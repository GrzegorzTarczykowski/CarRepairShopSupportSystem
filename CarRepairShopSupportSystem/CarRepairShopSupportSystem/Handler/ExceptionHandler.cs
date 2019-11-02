using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CarRepairShopSupportSystem.BLL.Models;

namespace CarRepairShopSupportSystem.Handler
{
    static class ExceptionHandler
    {
        internal static void LogAppDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var newExc = new Exception("CurrentDomainOnUnhandledException", e.ExceptionObject as Exception);
            LogUnhandledException(newExc);
        }

        internal static void LogTaskSchedulerUnhandledException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            var newExc = new Exception("TaskSchedulerOnUnobservedTaskException", e.Exception);
            LogUnhandledException(newExc);
        }

        private static void LogUnhandledException(Exception exception)
        {
            ApplicationSession.currentUser = null;
            ApplicationSession.userToken = null;
            ApplicationSession.userName = string.Empty;
            ApplicationSession.userPassword = string.Empty;
            Toast.MakeText(Application.Context, "Nieoczekiwany bład. Przepraszamy", ToastLength.Long).Show();
        }
    }

}