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
    public class ServiceService : IServiceService
    {
        private readonly IHttpClientService httpClientService;

        public ServiceService(IHttpClientService httpClientService)
        {
            this.httpClientService = httpClientService;
        }

        public OperationResult AddService(Models.Service service)
        {
            try
            {
                HttpResponseMessage tokenResponse = httpClientService.Post("api/Service", service);

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
                return new OperationResult { ResultCode = ResultCode.Error, Message = "Wystąpił problem z dodaniem usługi" };
            }
        }

        public OperationResult DeleteService(int serviceId)
        {
            try
            {
                HttpResponseMessage tokenResponse = httpClientService.Delete($"api/Service/{serviceId}");

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
                return new OperationResult { ResultCode = ResultCode.Error, Message = "Wystąpił problem z usuwaniem usługi" };
            }
        }

        public OperationResult EditService(Models.Service service)
        {
            try
            {
                HttpResponseMessage tokenResponse = httpClientService.Put("api/Service", service);

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
                return new OperationResult { ResultCode = ResultCode.Error, Message = "Wystąpił problem z edycja usługi" };
            }
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
