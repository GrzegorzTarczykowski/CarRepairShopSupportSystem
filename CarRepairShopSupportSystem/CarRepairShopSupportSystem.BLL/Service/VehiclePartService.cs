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
    public class VehiclePartService : IVehiclePartService
    {
        private readonly IHttpClientService httpClientService;

        public VehiclePartService(IHttpClientService httpClientService)
        {
            this.httpClientService = httpClientService;
        }

        public OperationResult AddVehiclePart(VehiclePart vehiclePart)
        {
            try
            {
                HttpResponseMessage tokenResponse = httpClientService.Post("api/VehiclePart", vehiclePart);

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
                return new OperationResult { ResultCode = ResultCode.Error, Message = "Wystąpił problem z dodaniem częsci pojazdu" };
            }
        }

        public OperationResult DeleteVehiclePart(int vehiclePartId)
        {
            try
            {
                HttpResponseMessage tokenResponse = httpClientService.Delete($"api/VehiclePart/{vehiclePartId}");

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
                return new OperationResult { ResultCode = ResultCode.Error, Message = "Wystąpił problem z usuwaniem częsci pojazdu" };
            }
        }

        public OperationResult EditVehiclePart(VehiclePart vehiclePart)
        {
            try
            {
                HttpResponseMessage tokenResponse = httpClientService.Put("api/VehiclePart", vehiclePart);

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
                return new OperationResult { ResultCode = ResultCode.Error, Message = "Wystąpił problem z edycja częsci pojazdu" };
            }
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
