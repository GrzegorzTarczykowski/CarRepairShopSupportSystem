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
    public class VehicleFuelController : ABaseApiController<VehicleFuel>
    {
        //new Repository<T>(new MsSqlServerContext(), new UnitOfWork())
        public VehicleFuelController(IRepository<VehicleFuel> repository) : base(repository)
        {
        }

        // GET api/<controller>
        public IEnumerable<VehicleFuel> Get()
        {
            return GetBase();
        }

        // GET api/<controller>/5
        public VehicleFuel Get(int id)
        {
            return GetBase(id);
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]VehicleFuel value)
        {
            return PostBase(value);
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put(int id, [FromBody]VehicleFuel value)
        {
            return PutBase(id == value.VehicleFuelId, value);
        }

        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(int id)
        {
            return DeleteBase(id);
        }
    }
}