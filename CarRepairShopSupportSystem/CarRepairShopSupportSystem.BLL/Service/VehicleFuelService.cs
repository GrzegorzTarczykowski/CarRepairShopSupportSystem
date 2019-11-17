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
    public class VehicleFuelService : IVehicleFuelService
    {
        private readonly IHttpClientService httpClientService;

        public VehicleFuelService(IHttpClientService httpClientService)
        {
            this.httpClientService = httpClientService;
        }

        public IEnumerable<VehicleFuel> GetAllVehicleFuelList()
        {

            try
            {
                HttpResponseMessage APIResponse = httpClientService.Get($"api/VehicleFuel");
                if (APIResponse.IsSuccessStatusCode)
                {
                    string JsonContent = APIResponse.Content.ReadAsStringAsync().Result;
                    IEnumerable<VehicleFuel> matchingVehicleFuelList = JsonConvert.DeserializeObject<IEnumerable<VehicleFuel>>(JsonContent);
                    if (matchingVehicleFuelList != null)
                    {
                        return matchingVehicleFuelList;
                    }
                }
                return Enumerable.Empty<VehicleFuel>();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
