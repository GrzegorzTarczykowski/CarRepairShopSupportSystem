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
    public class VehicleEngineController : ABaseApiController<VehicleEngine>
    {
        //new Repository<T>(new MsSqlServerContext(), new UnitOfWork())
        public VehicleEngineController(IRepository<VehicleEngine> repository) : base(repository)
        {
        }

        // GET api/<controller>
        public IEnumerable<VehicleEngine> Get()
        {
            return GetBase();
        }

        // GET api/<controller>/5
        public VehicleEngine Get(int id)
        {
            return GetBase(id);
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]VehicleEngine value)
        {
            return PostBase(value);
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put(int id, [FromBody]VehicleEngine value)
        {
            return PutBase(id == value.VehicleEngineId, value);
        }

        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(int id)
        {
            return DeleteBase(id);
        }
    }
}