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
    public class OrderService : IOrderService
    {
        private readonly IHttpClientService httpClientService;

        public OrderService(IHttpClientService httpClientService)
        {
            this.httpClientService = httpClientService;
        }

        public OperationResult AddVehicleOrder(Order order)
        {
            try
            {
                HttpResponseMessage tokenResponse = httpClientService.Post("api/Order", order);

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
                return new OperationResult { ResultCode = ResultCode.Error, Message = "Wystąpił problem z tworzeniem zlecenia" };
            }
        }

        public OperationResult EditVehicleOrder(Order order)
        {
            try
            {
                HttpResponseMessage tokenResponse = httpClientService.Put($"api/Order/{order.OrderId}", order);

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
                return new OperationResult { ResultCode = ResultCode.Error, Message = "Wystąpił problem edycja zlecenia" };
            }
        }

        public IEnumerable<Order> GetOrderListByVehicleId(int vehicleId)
        {
            try
            {
                HttpResponseMessage APIResponse = httpClientService.Get($"api/Order/GetByVehicleId?vehicleId={vehicleId}");
                if (APIResponse.IsSuccessStatusCode)
                {
                    string JsonContent = APIResponse.Content.ReadAsStringAsync().Result;
                    IEnumerable<Order> matchingOrderList = JsonConvert.DeserializeObject<IEnumerable<Order>>(JsonContent);
                    if (matchingOrderList != null)
                    {
                        return matchingOrderList;
                    }
                }
                return Enumerable.Empty<Order>();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
