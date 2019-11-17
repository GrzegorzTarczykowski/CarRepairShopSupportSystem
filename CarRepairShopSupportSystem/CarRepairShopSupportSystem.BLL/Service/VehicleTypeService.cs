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
    public class VehicleTypeService : IVehicleTypeService
    {
        private readonly IHttpClientService httpClientService;

        public VehicleTypeService(IHttpClientService httpClientService)
        {
            this.httpClientService = httpClientService;
        }

        public IEnumerable<VehicleType> GetAllVehicleTypeList()
        {

            try
            {
                HttpResponseMessage APIResponse = httpClientService.Get($"api/VehicleType");
                if (APIResponse.IsSuccessStatusCode)
                {
                    string JsonContent = APIResponse.Content.ReadAsStringAsync().Result;
                    IEnumerable<VehicleType> matchingVehicleTypeList = JsonConvert.DeserializeObject<IEnumerable<VehicleType>>(JsonContent);
                    if (matchingVehicleTypeList != null)
                    {
                        return matchingVehicleTypeList;
                    }
                }
                return Enumerable.Empty<VehicleType>();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
