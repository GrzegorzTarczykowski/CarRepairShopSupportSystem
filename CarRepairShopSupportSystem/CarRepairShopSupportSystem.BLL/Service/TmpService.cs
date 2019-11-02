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
    public class TmpService
    {
        public string GetResource(string accessToken)
        {
            HttpClientHandler handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);
            // Need to set the Access Token in the Authorization Header as shown below
            client.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", accessToken);
            // Make a Get request for the authorized resource by invoking 
            // the PostAsync method on the client object as shown below
            var APIResponse = client.GetAsync(Setting.addressAPI + "api/Users").Result;
            if (APIResponse.IsSuccessStatusCode)
            {
                var JsonContent = APIResponse.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<string>(JsonContent);
            }
            else
            {
                return APIResponse.StatusCode.ToString();
            }
        }
    }
}
