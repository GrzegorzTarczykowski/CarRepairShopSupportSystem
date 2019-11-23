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
    public class ServiceService : IServiceService
    {
        private readonly IHttpClientService httpClientService;

        public ServiceService(IHttpClientService httpClientService)
        {
            this.httpClientService = httpClientService;
        }

        public IEnumerable<Models.Service> GetAllServiceList()
        {
            try
            {
                HttpResponseMessage APIResponse = httpClientService.Get($"api/Service");
                if (APIResponse.IsSuccessStatusCode)
                {
                    string JsonContent = APIResponse.Content.ReadAsStringAsync().Result;
                    IEnumerable<Models.Service> matchingServiceList = JsonConvert.DeserializeObject<IEnumerable<Models.Service>>(JsonContent);
                    if (matchingServiceList != null)
                    {
                        return matchingServiceList;
                    }
                }
                return Enumerable.Empty<Models.Service>();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
