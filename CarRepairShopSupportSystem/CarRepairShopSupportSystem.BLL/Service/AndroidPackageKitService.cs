using CarRepairShopSupportSystem.BLL.IService;
using System;
using System.Net.Http;

namespace CarRepairShopSupportSystem.BLL.Service
{
    public class AndroidPackageKitService : IAndroidPackageKitService
    {
        private readonly IHttpClientService httpClientService;

        public AndroidPackageKitService(IHttpClientService httpClientService)
        {
            this.httpClientService = httpClientService;
        }

        public long GetApkVersion()
        {
            try
            {
                HttpResponseMessage APIResponse = httpClientService.Get($"api/AndroidPackageKit");
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
