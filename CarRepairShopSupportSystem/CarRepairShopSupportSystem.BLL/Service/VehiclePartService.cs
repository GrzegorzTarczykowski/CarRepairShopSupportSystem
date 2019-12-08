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
    public class VehiclePartService : IVehiclePartService
    {
        private readonly IHttpClientService httpClientService;

        public VehiclePartService(IHttpClientService httpClientService)
        {
            this.httpClientService = httpClientService;
        }

        public IEnumerable<VehiclePart> GetAllVehiclePartList()
        {
            try
            {
                HttpResponseMessage APIResponse = httpClientService.Get($"api/VehiclePart");
                if (APIResponse.IsSuccessStatusCode)
                {
                    string JsonContent = APIResponse.Content.ReadAsStringAsync().Result;
                    IEnumerable<VehiclePart> matchingVehiclePartList = JsonConvert.DeserializeObject<IEnumerable<VehiclePart>>(JsonContent);
                    if (matchingVehiclePartList != null)
                    {
                        return matchingVehiclePartList;
                    }
                }
                return Enumerable.Empty<VehiclePart>();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
