using CarRepairShopSupportSystem.WebAPI.BLL.Enums;
using CarRepairShopSupportSystem.WebAPI.Controllers.Abstraction;
using CarRepairShopSupportSystem.WebAPI.DAL.Abstraction;
using CarRepairShopSupportSystem.WebAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace CarRepairShopSupportSystem.WebAPI.Controllers
{
    public class ServiceController : ACRUDApiController<Service>
    {
        public ServiceController(IRepository<Service> repository) : base(repository)
        {
        }

        [Authorize(Roles = "SuperAdmin, Admin, User")]
        [HttpGet]
        // GET api/<controller>
        public IEnumerable<Models.Service> Get()
        {
            return GetBase()?.Select(s => new Models.Service { ServiceId = s.ServiceId
                                                                , Name = s.Name
                                                                , Description = s.Description
                                                                , Price = s.Price
                                                                , ExecutionTimeInMinutes = s.ExecutionTimeInMinutes });
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpGet]
        // GET api/<controller>/5
        public Service Get(int id)
        {
            return GetBase(id);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]Service value)
        {
            return PostBase(value);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPut]
        // PUT api/<controller>/5
        public HttpResponseMessage Put(int id, [FromBody]Service value)
        {
            return PutBase(id == value.ServiceId, value);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpDelete]
        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(int id)
        {
            return DeleteBase(id);
        }
    }
}