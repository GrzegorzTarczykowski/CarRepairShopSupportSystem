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

namespace CarRepairShopSupportSystem.Handler
{
    static class AlertHandler
    {
        internal static void AlertDialogShow(AppCompatActivity context, string title, string message, bool isCloseCurrentActivity)
        {
            context.RunOnUiThread(() =>
            {
                var alertDialog = new Android.App.AlertDialog.Builder(context);
                alertDialog.SetTitle(title)
                    .SetMessage(message)
                    .SetPositiveButton("OK", (senderAlert, args) =>
                    {
                        if (isCloseCurrentActivity)
                        {
                            context.Finish();
                        }
                    })
                    .Create();
                alertDialog.Show();
            });
        }
    }
}