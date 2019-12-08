using CarRepairShopSupportSystem.BLL.IService;
using CarRepairShopSupportSystem.BLL.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopSupportSystem.BLL.Service
{
    public class MessageService : IMessageService
    {
        private readonly IHttpClientService httpClientService;

        public MessageService(IHttpClientService httpClientService)
        {
            this.httpClientService = httpClientService;
        }

        public IEnumerable<Message> GetMessageListByOrderIdAndUserReceiverId(int orderId, int userReceiverId)
        {
            try
            {
                HttpResponseMessage APIResponse = httpClientService.Get($"api/Message/GetByOrderIdAndUserReceiverId?orderId={orderId}&userReceiverId={userReceiverId}");
                if (APIResponse.IsSuccessStatusCode)
                {
                    string JsonContent = APIResponse.Content.ReadAsStringAsync().Result;
                    IEnumerable<Message> matchingOrderList = JsonConvert.DeserializeObject<IEnumerable<Message>>(JsonContent);
                    if (matchingOrderList != null)
                    {
                        return matchingOrderList;
                    }
                }
                return Enumerable.Empty<Message>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public OperationResult SendMessage(Message message)
        {
            throw new NotImplementedException();
        }
    }
}
