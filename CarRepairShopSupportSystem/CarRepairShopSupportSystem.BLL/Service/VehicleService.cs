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
        private readonly IApplicationSessionService applicationSessionService;

        public VehicleService(IHttpClientService httpClientService, IApplicationSessionService applicationSessionService)
        {
            this.httpClientService = httpClientService;
            this.applicationSessionService = applicationSessionService;
        }

        public OperationResult AddUserVehicle(Vehicle vehicle)
        {
            try
            {
                vehicle.UserId = applicationSessionService.GetUserFromApplicationSession().UserId;
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

        public OperationResult EditUserVehicle(Vehicle vehicle)
        {
            try
            {
                vehicle.UserId = applicationSessionService.GetUserFromApplicationSession().UserId;
                HttpResponseMessage tokenResponse = httpClientService.Put($"api/Vehicle/{vehicle.VehicleId}", vehicle);

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
                return new OperationResult { ResultCode = ResultCode.Error, Message = "Wystąpił problem edycja pojazdu" };
            }
        }

        public int GetUserIdOwnerByVehicleId(int vehicleId)
        {
            try
            {
                HttpResponseMessage APIResponse = httpClientService.Get($"api/Vehicle/GetUserIdOwnerByVehicleId?vehicleId={vehicleId}");
                if (APIResponse.IsSuccessStatusCode)
                {
                    string JsonContent = APIResponse.Content.ReadAsStringAsync().Result;
                    int userIdOwner = JsonConvert.DeserializeObject<int>(JsonContent);
                    return userIdOwner;
                }
                return 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Vehicle> GetVehicleList()
        {
            try
            {
                HttpResponseMessage APIResponse = httpClientService.Get($"api/Vehicle");
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

        public IEnumerable<Vehicle> GetVehicleListByUserId()
        {
            try
            {
                int userId = applicationSessionService.GetUserFromApplicationSession().UserId;
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
