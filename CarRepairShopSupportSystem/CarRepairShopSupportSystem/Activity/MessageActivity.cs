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
using CarRepairShopSupportSystem.BLL.IService;
using CarRepairShopSupportSystem.BLL.Models;
using CarRepairShopSupportSystem.BLL.Service;

namespace CarRepairShopSupportSystem.Activity
{
    [Activity(Label = "Konwersacja")]
    public class MessageActivity : AppCompatActivity
    {
        private readonly IMessageService messageService;

        private IList<BLL.Models.Message> messages;

        public MessageActivity()
        {
            messageService = new MessageService(new HttpClientService(new AccessTokenService(new ApplicationSessionService(), new TokenService())));
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_message);
            FindViewById<Button>(Resource.Id.btnSentMessages).Click += BtnSentMessages_Click;
            GridView gvMessageList = FindViewById<GridView>(Resource.Id.gvMessageList);
            RefreshGvMessageList(gvMessageList);
        }

        private void BtnSentMessages_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void RefreshGvMessageList(GridView gvMessageList)
        {
            messages = messageService.GetMessageListByOrderIdAndUserReceiverId(0,0).ToList();
            gvMessageList.Adapter = new MessageAdapter(this, messages.ToArray());
        }
    }
}