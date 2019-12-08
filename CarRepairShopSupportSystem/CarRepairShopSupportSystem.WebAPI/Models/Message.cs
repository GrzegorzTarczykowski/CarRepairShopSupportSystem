using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRepairShopSupportSystem.WebAPI.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime? SentDate { get; set; }

        public int OrderId { get; set; }
        public int UserSenderId { get; set; }
        public User UserSender { get; set; }
        public int UserReceiverId { get; set; }
        public User UserReceiver { get; set; }
    }
}