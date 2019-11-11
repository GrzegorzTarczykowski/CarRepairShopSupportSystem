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
    public class VehicleBrandService : IVehicleBrandService
    {
        private readonly IHttpClientService httpClientService;

        public VehicleBrandService(IHttpClientService httpClientService)
        {
            this.httpClientService = httpClientService;
        }

        public IEnumerable<VehicleBrand> GetAllVehicleBrandList()
        {
            try
            {
                HttpResponseMessage APIResponse = httpClientService.Get($"api/VehicleBrand");
                if (APIResponse.IsSuccessStatusCode)
                {
                    string JsonContent = APIResponse.Content.ReadAsStringAsync().Result;
                    IEnumerable<VehicleBrand> matchingVehicleBrandList = JsonConvert.DeserializeObject<IEnumerable<VehicleBrand>>(JsonContent);
                    if (matchingVehicleBrandList != null)
                    {
                        return matchingVehicleBrandList;
                    }
                }
                return Enumerable.Empty<VehicleBrand>();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
