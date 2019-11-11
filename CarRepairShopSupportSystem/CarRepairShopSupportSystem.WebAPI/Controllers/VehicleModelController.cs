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
    public class VehicleModelController : ACRUDApiController<VehicleModel>
    {
        public VehicleModelController(IRepository<VehicleModel> repository) : base(repository)
        {
        }

        [Route("api/VehicleModel/GetByVehicleBrandId")]
        [Authorize(Roles = "SuperAdmin, Admin, User")]
        [HttpGet]
        public IEnumerable<Models.VehicleModel> GetByVehicleBrandId([FromUri]int vehicleBrandId)
        {
            return GetByBase(vm => vm.VehicleBrandId == vehicleBrandId)
                ?.Select(vm => new Models.VehicleModel { VehicleModelId = vm.VehicleModelId, Name = vm.Name });
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpGet]
        // GET api/<controller>/5
        public VehicleModel Get(int id)
        {
            return GetBase(id);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]VehicleModel value)
        {
            return PostBase(value);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPut]
        // PUT api/<controller>/5
        public HttpResponseMessage Put(int id, [FromBody]VehicleModel value)
        {
            return PutBase(id == value.VehicleModelId, value);
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