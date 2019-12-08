using CarRepairShopSupportSystem.WebAPI.BLL.IService;
using CarRepairShopSupportSystem.WebAPI.DAL.Abstraction;
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
        private readonly IRepository<Message> messageRepository;

        public MessageService(IRepository<Message> messageRepository)
        {
            this.messageRepository = messageRepository;
        }

        public bool SendMessage(Message message)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Message> GetMessageListByOrderId(int orderId)
        {
            return messageRepository.GetAll(nameof(Message.UserSender), nameof(Message.UserReceiver))
                                    .Where(m => m.OrderId == orderId);
        }

        public IEnumerable<Message> GetMessageListByOrderIdAndUserId(int orderId, int userId)
        {
            return messageRepository.GetAll(nameof(Message.UserSender), nameof(Message.UserReceiver))
                                    .Where(m => m.OrderId == orderId)
                                    .Where(m => m.UserReceiverId == userId || m.UserSenderId == userId);
        }
    }
}
