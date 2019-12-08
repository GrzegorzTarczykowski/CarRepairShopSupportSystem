using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopSupportSystem.BLL.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime? SentDate { get; set; }

        public int OrderId { get; set; }
        public int UserSenderId { get; set; }
        public string UserSenderFirstName { get; set; }
        public string UserSenderLastName { get; set; }
        public int UserReceiverId { get; set; }
        public string UserReceiverFirstName { get; set; }
        public string UserReceiverLastName { get; set; }
    }
}
