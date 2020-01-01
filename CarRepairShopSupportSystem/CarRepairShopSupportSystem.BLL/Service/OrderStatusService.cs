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
    public class OrderStatusService : IOrderStatusService
    {
        private readonly IHttpClientService httpClientService;

        public OrderStatusService(IHttpClientService httpClientService)
        {
            this.httpClientService = httpClientService;
        }

        public IEnumerable<OrderStatus> GetAllOrderStatusList()
        {
            try
            {
                HttpResponseMessage APIResponse = httpClientService.Get($"api/OrderStatus");
                if (APIResponse.IsSuccessStatusCode)
                {
                    string JsonContent = APIResponse.Content.ReadAsStringAsync().Result;
                    IEnumerable<OrderStatus> matchingOrderStatusList = JsonConvert.DeserializeObject<IEnumerable<OrderStatus>>(JsonContent);
                    if (matchingOrderStatusList != null)
                    {
                        return matchingOrderStatusList;
                    }
                }
                return Enumerable.Empty<OrderStatus>();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
