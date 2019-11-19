using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace CarRepairShopSupportSystem.WebAPI.Controllers.Abstraction
{
    public abstract class ABaseApiController : ApiController
    {
        internal protected HttpResponseMessage BaseAction(Func<HttpResponseMessage> func)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return func();
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        internal protected T BaseAction<T>(Func<T> func)
        {
            try
            {
                return func();
            }
            catch (Exception)
            {
                return default;
            }
        }
    }
}