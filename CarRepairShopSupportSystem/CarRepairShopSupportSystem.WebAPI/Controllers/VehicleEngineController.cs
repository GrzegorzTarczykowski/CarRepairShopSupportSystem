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
    public class VehicleEngineController : ACRUDApiController<VehicleEngine>
    {
        public VehicleEngineController(IRepository<VehicleEngine> repository) : base(repository)
        {
        }

        [Authorize(Roles = "SuperAdmin, Admin, User")]
        [HttpGet]
        // GET api/<controller>
        public IEnumerable<Models.VehicleEngine> Get()
        {
            return GetBase()?.Select(ve => new Models.VehicleEngine { VehicleEngineId = ve.VehicleEngineId
                                                                        , PowerKW = ve.PowerKW
                                                                        , CapacityCCM = ve.CapacityCCM
                                                                        , EngineCode = ve.EngineCode });
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpGet]
        // GET api/<controller>/5
        public VehicleEngine Get(int id)
        {
            return GetBase(id);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]VehicleEngine value)
        {
            return PostBase(value);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPut]
        // PUT api/<controller>/5
        public HttpResponseMessage Put(int id, [FromBody]VehicleEngine value)
        {
            return PutBase(id == value.VehicleEngineId, value);
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