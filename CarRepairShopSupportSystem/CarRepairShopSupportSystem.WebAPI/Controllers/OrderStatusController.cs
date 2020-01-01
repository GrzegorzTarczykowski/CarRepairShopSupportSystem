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
    public class OrderStatusController : ACRUDApiController<OrderStatus>
    {
        public OrderStatusController(IRepository<OrderStatus> repository) : base(repository)
        {
        }


        [Authorize(Roles = "SuperAdmin, Admin, User")]
        [HttpGet]
        // GET api/<controller>
        public IEnumerable<Models.OrderStatus> Get()
        {
            return GetBase()?.Select(os => new Models.OrderStatus { OrderStatusId = os.OrderStatusId, Name = os.Name });
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpGet]
        // GET api/<controller>/5
        public OrderStatus Get(int id)
        {
            return GetBase(id);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]OrderStatus value)
        {
            return PostBase(value);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPut]
        // PUT api/<controller>/5
        public HttpResponseMessage Put(int id, [FromBody]OrderStatus value)
        {
            return PutBase(id == value.OrderStatusId, value);
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