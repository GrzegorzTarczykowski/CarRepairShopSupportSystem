using CarRepairShopSupportSystem.WebAPI.BLL.IService;
using CarRepairShopSupportSystem.WebAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopSupportSystem.WebAPI.BLL.Service
{
    public class MessageService : IMessageService
    {
        public bool SendMessage(Message message)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Message> GetMessageListByOrderIdAndUserReceiverId(int orderId, int userReceiverId)
        {
            throw new NotImplementedException();
        }
    }
}
