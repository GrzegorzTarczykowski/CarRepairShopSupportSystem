using CarRepairShopSupportSystem.WebAPI.Controllers.Abstraction;
using CarRepairShopSupportSystem.WebAPI.DAL.Abstraction;
using CarRepairShopSupportSystem.WebAPI.DAL.Models;
using CarRepairShopSupportSystem.WebAPI.DAL.MsSqlServerDB;
using CarRepairShopSupportSystem.WebAPI.DAL.Repository;
using CarRepairShopSupportSystem.WebAPI.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CarRepairShopSupportSystem.WebAPI.Controllers
{
    public class VehiclesController : ABaseApiController<Vehicle>
    {
        //new Repository<T>(new MsSqlServerContext(), new UnitOfWork())
        public VehiclesController(IRepository<Vehicle> repository) : base(repository)
        {
        }

        // GET api/<controller>
        [Route("api/Vehicles/GetByUserId/{userId}")]
        public IEnumerable<Vehicle> GetByUserId(int userId)
        {
            return GetByBase(v => v.UserId == userId);
        }

        // GET api/<controller>/5
        public Vehicle Get(int id)
        {
            return GetBase(id);
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]Vehicle value)
        {
            return PostBase(value);
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put(int id, [FromBody]Vehicle value)
        {
            return PutBase(id == value.VehicleId, value);
        }

        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(int id)
        {
            return DeleteBase(id);
        }
    }
}