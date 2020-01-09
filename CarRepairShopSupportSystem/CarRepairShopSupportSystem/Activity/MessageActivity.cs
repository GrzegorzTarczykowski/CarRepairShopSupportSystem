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
using CarRepairShopSupportSystem.Adapter;
using CarRepairShopSupportSystem.BLL.Enums;
using CarRepairShopSupportSystem.BLL.IService;
using CarRepairShopSupportSystem.BLL.Models;
using CarRepairShopSupportSystem.BLL.Service;

namespace CarRepairShopSupportSystem.Activity
{
    [Activity(Label = "Konwersacja")]
    public class MessageActivity : AppCompatActivity
    {
        private readonly IMessageService messageService;
        private readonly IApplicationSessionService applicationSessionService;

        private IList<BLL.Models.Message> messages;

        public MessageActivity()
        {
            messageService = new MessageService(new HttpClientService(new AccessTokenService(new ApplicationSessionService(), new TokenService())));
            applicationSessionService = new ApplicationSessionService();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_message);
            FindViewById<Button>(Resource.Id.btnSentMessage).Click += BtnSentMessage_Click;
            GridView gvMessageList = FindViewById<GridView>(Resource.Id.gvMessageList);
            RefreshGvMessageList(gvMessageList);
        }

        private void BtnSentMessage_Click(object sender, EventArgs e)
        {
            BLL.Models.Message message = new BLL.Models.Message
            {
                Title = string.Empty,
                Content = FindViewById<EditText>(Resource.Id.etMessageCreateor).Text,
                SentDate = DateTime.Now,
                OrderId = int.Parse(Intent.GetStringExtra("OrderId")),
                UserReceiverId = int.Parse(Intent.GetStringExtra("UserReceiverId")),
                UserSenderId = applicationSessionService.GetUserFromApplicationSession().UserId
            };

            OperationResult operationResult = messageService.SendMessage(message);

            if (operationResult.ResultCode != ResultCode.Successful)
            {
                Toast.MakeText(Application.Context, operationResult.Message, ToastLength.Long).Show();
            }
            else
            {
                FindViewById<EditText>(Resource.Id.etMessageCreateor).Text = string.Empty;
            }

            RefreshGvMessageList(FindViewById<GridView>(Resource.Id.gvMessageList));
        }

        private void RefreshGvMessageList(GridView gvMessageList)
        {
            int orderId = int.Parse(Intent.GetStringExtra("OrderId"));
            int userId = applicationSessionService.GetUserFromApplicationSession().UserId;
            if (applicationSessionService.GetUserFromApplicationSession().PermissionId == (int)PermissionId.SuperAdmin)
            {
                messages = messageService.GetMessageListByOrderId(orderId).OrderByDescending(m => m.SentDate).ToList();
            }
            else
            {
                messages = messageService.GetMessageListByOrderIdAndUserId(orderId, userId).OrderByDescending(m => m.SentDate).ToList();
            }
            gvMessageList.Adapter = new MessageAdapter(this, messages.ToArray(), userId);
        }
    }
}