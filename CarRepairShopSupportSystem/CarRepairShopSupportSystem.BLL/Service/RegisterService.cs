using CarRepairShopSupportSystem.BLL.IService;
using CarRepairShopSupportSystem.BLL.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopSupportSystem.BLL.Service
{
    public class RegisterService : IRegisterService
    {
        private readonly IAccessTokenService accessTokenService;
        private readonly IApplicationSessionService applicationSessionService;

        public RegisterService(IAccessTokenService accessTokenService, IApplicationSessionService applicationSessionService)
        {
            this.accessTokenService = accessTokenService;
            this.applicationSessionService = applicationSessionService;
        }

        public bool Register(User user)
        {
            try
            {
                ApplicationSession.userName = "TestGuest";
                ApplicationSession.userPassword = "1";
                HttpClientHandler handler = new HttpClientHandler();
                HttpClient client = new HttpClient(handler);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessTokenService.GetAccessToken());
                var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                var tokenResponse = client.PostAsync(Setting.addressAPI + "api/Users", content).Result;
                if (tokenResponse.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                applicationSessionService.ClearApplicationSession();
            }
        }
    }
}
