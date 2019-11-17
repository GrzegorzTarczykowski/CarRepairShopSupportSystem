using CarRepairShopSupportSystem.BLL.IService;
using CarRepairShopSupportSystem.BLL.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace CarRepairShopSupportSystem.BLL.Service
{
    public class VehicleEngineService : IVehicleEngineService
    {
        private readonly IHttpClientService httpClientService;

        public VehicleEngineService(IHttpClientService httpClientService)
        {
            this.httpClientService = httpClientService;
        }

        public IEnumerable<VehicleEngine> GetAllVehicleEngineList()
        {
            try
            {
                HttpResponseMessage APIResponse = httpClientService.Get($"api/VehicleEngine");
                if (APIResponse.IsSuccessStatusCode)
                {
                    string JsonContent = APIResponse.Content.ReadAsStringAsync().Result;
                    IEnumerable<VehicleEngine> matchingVehicleEngineList = JsonConvert.DeserializeObject<IEnumerable<VehicleEngine>>(JsonContent);
                    if (matchingVehicleEngineList != null)
                    {
                        return matchingVehicleEngineList;
                    }
                }
                return Enumerable.Empty<VehicleEngine>();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
