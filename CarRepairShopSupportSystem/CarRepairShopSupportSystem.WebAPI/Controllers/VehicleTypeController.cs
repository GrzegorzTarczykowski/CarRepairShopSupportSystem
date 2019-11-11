using CarRepairShopSupportSystem.WebAPI.Controllers.Abstraction;
using CarRepairShopSupportSystem.WebAPI.DAL.Abstraction;
using CarRepairShopSupportSystem.WebAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CarRepairShopSupportSystem.WebAPI.Controllers
{
    public class VehicleTypeController : ACRUDApiController<VehicleType>
    {
        public VehicleTypeController(IRepository<VehicleType> repository) : base(repository)
        {
        }

        
        [Authorize(Roles = "SuperAdmin, Admin, User")]
        // GET api/<controller>
        public IEnumerable<VehicleType> Get()
        {
            return GetBase();
        }

        // GET api/<controller>/5
        public VehicleType Get(int id)
        {
            return GetBase(id);
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]VehicleType value)
        {
            return PostBase(value);
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put(int id, [FromBody]VehicleType value)
        {
            return PutBase(id == value.VehicleTypeId, value);
        }

        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(int id)
        {
            return DeleteBase(id);
        }
    }
}