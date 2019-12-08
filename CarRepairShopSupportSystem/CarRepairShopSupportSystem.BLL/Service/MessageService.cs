using CarRepairShopSupportSystem.BLL.Enums;
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

        public IEnumerable<Message> GetMessageListByOrderId(int orderId)
        {
            try
            {
                HttpResponseMessage APIResponse = httpClientService.Get($"api/Message/GetByOrderId?orderId={orderId}");
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

        public IEnumerable<Message> GetMessageListByOrderIdAndUserId(int orderId, int userId)
        {
            try
            {
                HttpResponseMessage APIResponse = httpClientService.Get($"api/Message/GetByOrderIdAndUserId?orderId={orderId}&userId={userId}");
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
            try
            {
                HttpResponseMessage tokenResponse = httpClientService.Post("api/Message", message);

                if (tokenResponse.IsSuccessStatusCode)
                {
                    return new OperationResult { ResultCode = ResultCode.Successful };
                }

                OperationResult operationResult = JsonConvert.DeserializeObject<OperationResult>(tokenResponse.Content.ReadAsStringAsync().Result);
                operationResult.ResultCode = ResultCode.Error;
                return operationResult;
            }
            catch (Exception)
            {
                return new OperationResult { ResultCode = ResultCode.Error, Message = "Wystąpił problem z wysłanie wiadomości" };
            }
        }
    }
}
