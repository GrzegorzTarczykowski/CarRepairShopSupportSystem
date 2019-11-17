using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopSupportSystem.BLL.IService
{
    public interface IHttpClientService
    {
        HttpResponseMessage Get(string requestUri);
        HttpResponseMessage Post(string requestUri, object content);
        HttpResponseMessage Put(string requestUri, object content);
    }
}
