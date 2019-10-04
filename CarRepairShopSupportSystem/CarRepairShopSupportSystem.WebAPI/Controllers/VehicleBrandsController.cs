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
    public class VehicleBrandsController : ABaseApiController<VehicleBrand>
    {
        //new Repository<T>(new MsSqlServerContext(), new UnitOfWork())
        public VehicleBrandsController(IRepository<VehicleBrand> repository) : base(repository)
        {
        }

        // GET api/<controller>
        public IEnumerable<VehicleBrand> Get()
        {
            return GetBase();
        }

        // GET api/<controller>/5
        public VehicleBrand Get(int id)
        {
            return GetBase(id);
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]VehicleBrand value)
        {
            return PostBase(value);
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put(int id, [FromBody]VehicleBrand value)
        {
            return PutBase(id == value.VehicleBrandId, value);
        }

        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(int id)
        {
            return DeleteBase(id);
        }
    }
}