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
    public class VehiclePartController : ACRUDApiController<VehiclePart>
    {
        public VehiclePartController(IRepository<VehiclePart> repository) : base(repository)
        {
        }

        // GET api/<controller>
        public IEnumerable<VehiclePart> Get()
        {
            return GetBase();
        }

        // GET api/<controller>/5
        public VehiclePart Get(int id)
        {
            return GetBase(id);
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]VehiclePart value)
        {
            return PostBase(value);
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put(int id, [FromBody]VehiclePart value)
        {
            return PutBase(id == value.VehiclePartId, value);
        }

        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(int id)
        {
            return DeleteBase(id);
        }
    }
}