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
    [Authorize(Roles = "SuperAdmin")]
    public class VehicleController : ABaseApiController<Vehicle>
    {
        public VehicleController(IRepository<Vehicle> repository) : base(repository)
        {
        }

        [Route("api/Vehicles/GetByUserId/{userId}")]
        [Authorize(Roles = "SuperAdmin, Admin, User")]
        [HttpGet]
        public IEnumerable<Vehicle> GetByUserId(int userId)
        {
            return GetByBase(v => v.UserId == userId);
        }

        [HttpGet]
        // GET api/<controller>/5
        public Vehicle Get(int id)
        {
            return GetBase(id);
        }

        [HttpPost]
        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]Vehicle value)
        {
            return PostBase(value);
        }

        [HttpPut]
        // PUT api/<controller>/5
        public HttpResponseMessage Put(int id, [FromBody]Vehicle value)
        {
            return PutBase(id == value.VehicleId, value);
        }

        [HttpDelete]
        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(int id)
        {
            return DeleteBase(id);
        }
    }
}