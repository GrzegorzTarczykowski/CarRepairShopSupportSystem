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
        IEnumerable<Message> GetMessageListByOrderIdAndUserReceiverId(int orderId, int userReceiverId);
    }
}
