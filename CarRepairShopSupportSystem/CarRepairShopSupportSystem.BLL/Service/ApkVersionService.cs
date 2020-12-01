using CarRepairShopSupportSystem.BLL.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopSupportSystem.BLL.Service
{
    public class ApkVersionService : IApkVersionService
    {
        private readonly IHttpClientService httpClientService;

        public ApkVersionService(IHttpClientService httpClientService)
        {
            this.httpClientService = httpClientService;
        }

        public long GetApkVersion()
        {
            try
            {
                HttpResponseMessage APIResponse = httpClientService.Get($"api/ApkVersion");
                if (APIResponse.IsSuccessStatusCode)
                {
                    string result = APIResponse.Content.ReadAsStringAsync().Result;
                    return long.Parse(result);
                }
                return 0;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
