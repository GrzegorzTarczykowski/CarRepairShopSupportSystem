using CarRepairShopSupportSystem.WebAPI.BLL.Enums;
using CarRepairShopSupportSystem.WebAPI.BLL.IService;
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
    public class VehicleController : ACRUDApiController<Vehicle>
    {
        private readonly IVehicleService vehicleService;

        public VehicleController(IRepository<Vehicle> repository, IVehicleService vehicleService) : base(repository)
        {
            this.vehicleService = vehicleService;
        }

        [Route("api/Vehicle/GetByUserId")]
        [Authorize(Roles = "SuperAdmin, Admin, User")]
        [HttpGet]
        public IEnumerable<Models.Vehicle> GetByUserId([FromUri]int userId)
        {
            return vehicleService.GetVehicleListByUserId(userId)
                ?.Select(v => new Models.Vehicle 
                {
                    VehicleId = v.VehicleId,
                    EngineMileage = v.EngineMileage,
                    RegistrationNumbers = v.RegistrationNumbers,
                    Description = v.Description,
                    UserId = v.UserId,
                    VehicleBrandId = v.VehicleModel.VehicleBrandId,
                    VehicleBrandName = v.VehicleModel.VehicleBrand.Name,
                    VehicleEngineId = v.VehicleEngineId,
                    VehicleEngineCode = v.VehicleEngine.EngineCode,
                    VehicleFuelId = v.VehicleFuelId,
                    VehicleFuelName = v.VehicleFuel.Name,
                    VehicleModelId = v.VehicleModelId,
                    VehicleModelName = v.VehicleModel.Name,
                    VehicleTypeId = v.VehicleTypeId,
                    VehicleTypeName = v.VehicleType.Name
                });
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpGet]
        // GET api/<controller>/5
        public Vehicle Get(int id)
        {
            return GetBase(id);
        }

        [Authorize(Roles = "SuperAdmin, Admin, User")]
        [HttpPost]
        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]Models.Vehicle value)
        {
            return PostBase(delegate
            {
                Vehicle vehicle = new Vehicle
                {
                    EngineMileage = value.EngineMileage,
                    RegistrationNumbers = value.RegistrationNumbers,
                    UserId = value.UserId,
                    VehicleEngineId = value.VehicleEngineId,
                    VehicleFuelId = value.VehicleFuelId,
                    VehicleModelId = value.VehicleModelId,
                    VehicleTypeId = value.VehicleTypeId
                };
                VehicleServiceResponse vehicleServiceResponse = vehicleService.AddUserVehicle(vehicle);
                switch (vehicleServiceResponse)
                {
                    case VehicleServiceResponse.SuccessOperationAdd:
                        return new HttpResponseMessage(HttpStatusCode.OK);
                    case VehicleServiceResponse.DuplicateRegistrationNumbers:
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Użytkownik posiada już dodany pojazd o podanym numerze rejestracyjnym");
                    case VehicleServiceResponse.ErrorOperationAdd:
                        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Nieoczekiwany bład");
                    default:
                        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Nieoczekiwany bład");
                }
            });
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPut]
        // PUT api/<controller>/5
        public HttpResponseMessage Put(int id, [FromBody]Vehicle value)
        {
            return PutBase(id == value.VehicleId, value);
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