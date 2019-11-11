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
    public class VehicleService : IVehicleService
    {
        private readonly IHttpClientService httpClientService;

        public VehicleService(IHttpClientService httpClientService)
        {
            this.httpClientService = httpClientService;
        }

        public OperationResult AddUserVehicle(Vehicle vehicle)
        {
            try
            {
                HttpResponseMessage tokenResponse = httpClientService.Post("api/Vehicle", vehicle);

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
                return new OperationResult { ResultCode = ResultCode.Error, Message = "Wystąpił problem z rejestracją" };
            }
        }

        public IEnumerable<Vehicle> GetVehicleListByUserId(int userId)
        {
            try
            {
                HttpResponseMessage APIResponse = httpClientService.Get($"api/Vehicle/GetByUserId?userId={userId}");
                if (APIResponse.IsSuccessStatusCode)
                {
                    string JsonContent = APIResponse.Content.ReadAsStringAsync().Result;
                    IEnumerable<Vehicle> matchingVehicleList = JsonConvert.DeserializeObject<IEnumerable<Vehicle>>(JsonContent);
                    if (matchingVehicleList != null)
                    {
                        return matchingVehicleList;
                    }
                }
                return Enumerable.Empty<Vehicle>();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
