using CarRepairShopSupportSystem.WebAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopSupportSystem.WebAPI.BLL.IService
{
    public interface IMessageService
    {
        bool SendMessage(Message message);
        IEnumerable<Message> GetMessageListByOrderIdAndUserReceiverId(int orderId, int userReceiverId);
    }
}
