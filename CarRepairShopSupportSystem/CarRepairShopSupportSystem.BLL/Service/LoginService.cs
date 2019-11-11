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
    public class LoginService : ILoginService
    {
        private readonly IHttpClientService httpClientService;
        private readonly IApplicationSessionService applicationSessionService;

        public LoginService(IHttpClientService httpClientService, IApplicationSessionService applicationSessionService)
        {
            this.httpClientService = httpClientService;
            this.applicationSessionService = applicationSessionService;
        }

        public bool Login(string username, string password)
        {
            try
            {
                ApplicationSession.userName = username;
                ApplicationSession.userPassword = password;
                HttpResponseMessage APIResponse = httpClientService.Get($"api/User?username={username}&password={password}");
                if (APIResponse.IsSuccessStatusCode)
                {
                    string JsonContent = APIResponse.Content.ReadAsStringAsync().Result;
                    User matchingUser = JsonConvert.DeserializeObject<User>(JsonContent);
                    if (matchingUser != null)
                    {
                        applicationSessionService.AddUserIntoApplicationSession(matchingUser);
                        return true;
                    }
                }

                ApplicationSession.userName = string.Empty;
                ApplicationSession.userPassword = string.Empty;
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Logout()
        {
            applicationSessionService.ClearApplicationSession();
        }
    }
}
