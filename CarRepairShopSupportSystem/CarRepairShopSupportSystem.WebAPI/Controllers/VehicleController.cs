using CarRepairShopSupportSystem.WebAPI.Controllers.Abstraction;
using CarRepairShopSupportSystem.WebAPI.DAL.Abstraction;
using CarRepairShopSupportSystem.WebAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace CarRepairShopSupportSystem.WebAPI.Controllers
{
    public class VehicleController : ACRUDApiController<Vehicle>
    {
        public VehicleController(IRepository<Vehicle> repository) : base(repository)
        {
        }

        [Route("api/Vehicle/GetByUserId")]
        [Authorize(Roles = "SuperAdmin, Admin, User")]
        [HttpGet]
        public IEnumerable<Vehicle> GetByUserId([FromUri]int userId)
        {
            return GetByBase(v => v.UserId == userId);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpGet]
        // GET api/<controller>/5
        public Vehicle Get(int id)
        {
            return GetBase(id);
        }

        [Authorize(Roles = "SuperAdmin, Admin, User")]
        [HttpPost]
        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]Vehicle value)
        {
            return PostBase(value);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPut]
        // PUT api/<controller>/5
        public HttpResponseMessage Put(int id, [FromBody]Vehicle value)
        {
            return PutBase(id == value.VehicleId, value);
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