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
    public class HttpClientService : IHttpClientService
    {
        private readonly IAccessTokenService accessTokenService;
        public HttpClientService(IAccessTokenService accessTokenService)
        {
            this.accessTokenService = accessTokenService;
        }

        public HttpResponseMessage Get(string requestUri)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessTokenService.GetAccessToken());
                    return client.GetAsync(Setting.addressAPI + requestUri).Result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public HttpResponseMessage Post(string requestUri, object content)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessTokenService.GetAccessToken());
                    HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
                    return client.PostAsync(Setting.addressAPI + requestUri, httpContent).Result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
