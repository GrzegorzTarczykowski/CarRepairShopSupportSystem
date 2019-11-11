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
    public class RegisterService : IRegisterService
    {
        private readonly IHttpClientService httpClientService;
        private readonly IApplicationSessionService applicationSessionService;

        public RegisterService(IHttpClientService httpClientService, IApplicationSessionService applicationSessionService)
        {
            this.httpClientService = httpClientService;
            this.applicationSessionService = applicationSessionService;
        }

        public OperationResult Register(User user)
        {
            try
            {
                ApplicationSession.userName = "TestGuest";
                ApplicationSession.userPassword = "1";
                HttpResponseMessage tokenResponse = httpClientService.Post("api/User", user);

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
            finally
            {
                applicationSessionService.ClearApplicationSession();
            }
        }
    }
}
