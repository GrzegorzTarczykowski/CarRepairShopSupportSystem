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
    public class VehicleModelService : IVehicleModelService
    {
        private readonly IHttpClientService httpClientService;

        public VehicleModelService(IHttpClientService httpClientService)
        {
            this.httpClientService = httpClientService;
        }

        public IEnumerable<VehicleModel> GetVehicleModelListByVehicleBrandId(int vehicleBrandId)
        {
            try
            {
                HttpResponseMessage APIResponse = httpClientService.Get($"api/VehicleModel/GetByVehicleBrandId?vehicleBrandId={vehicleBrandId}");
                if (APIResponse.IsSuccessStatusCode)
                {
                    string JsonContent = APIResponse.Content.ReadAsStringAsync().Result;
                    IEnumerable<VehicleModel> matchingVehicleModelList = JsonConvert.DeserializeObject<IEnumerable<VehicleModel>>(JsonContent);
                    if (matchingVehicleModelList != null)
                    {
                        return matchingVehicleModelList;
                    }
                }
                return Enumerable.Empty<VehicleModel>();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
