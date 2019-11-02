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
    public class LoginService : ILoginService
    {
        private readonly IApplicationSessionService applicationSessionService;
        private readonly IAccessTokenService accessTokenService;

        public LoginService(IApplicationSessionService applicationSessionService, IAccessTokenService accessTokenService)
        {
            this.applicationSessionService = applicationSessionService;
            this.accessTokenService = accessTokenService;
        }

        public bool Login(string username, string password)
        {
            try
            {
                ApplicationSession.userName = username;
                ApplicationSession.userPassword = password;

                HttpClientHandler handler = new HttpClientHandler();
                HttpClient client = new HttpClient(handler);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessTokenService.GetAccessToken());
                var APIResponse = client.GetAsync(Setting.addressAPI + $"api/Users?username={username}&password={password}").Result;
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
