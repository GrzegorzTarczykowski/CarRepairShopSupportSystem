using CarRepairShopSupportSystem.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopSupportSystem.BLL.IService
{
    public interface IMessageService
    {
        OperationResult SendMessage(Message message);
        IEnumerable<Message> GetMessageListByOrderId(int orderId);
        IEnumerable<Message> GetMessageListByOrderIdAndUserId(int orderId, int userId);
    }
}
