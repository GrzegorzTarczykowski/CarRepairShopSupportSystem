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
    public class VehicleModelController : ABaseApiController<VehicleModel>
    {
        //new Repository<T>(new MsSqlServerContext(), new UnitOfWork())
        public VehicleModelController(IRepository<VehicleModel> repository) : base(repository)
        {
        }

        // GET api/<controller>
        public IEnumerable<VehicleModel> Get()
        {
            return GetBase();
        }

        // GET api/<controller>/5
        public VehicleModel Get(int id)
        {
            return GetBase(id);
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]VehicleModel value)
        {
            return PostBase(value);
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put(int id, [FromBody]VehicleModel value)
        {
            return PutBase(id == value.VehicleModelId, value);
        }

        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(int id)
        {
            return DeleteBase(id);
        }
    }
}