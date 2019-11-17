using CarRepairShopSupportSystem.WebAPI.BLL.Enums;
using CarRepairShopSupportSystem.WebAPI.BLL.IService;
using CarRepairShopSupportSystem.WebAPI.DAL.Abstraction;
using CarRepairShopSupportSystem.WebAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopSupportSystem.WebAPI.BLL.Service
{
    public class VehicleService : IVehicleService
    {
        private readonly IRepository<Vehicle> vehicleRepository;

        public VehicleService(IRepository<Vehicle> vehicleRepository)
        {
            this.vehicleRepository = vehicleRepository;
        }

        public IEnumerable<Vehicle> GetVehicleListByUserId(int userId)
        {
            return vehicleRepository.GetAll(nameof(VehicleEngine)
                                            , nameof(VehicleFuel)
                                            , $"{nameof(VehicleModel)}.{nameof(VehicleBrand)}"
                                            , nameof(VehicleType)).Where(v => v.UserId == userId);
        }

        public VehicleServiceResponse AddUserVehicle(Vehicle vehicle)
        {
            try
            {
                if (vehicleRepository.Any(v => v.RegistrationNumbers == vehicle.RegistrationNumbers && v.UserId == vehicle.UserId))
                {
                    return VehicleServiceResponse.DuplicateRegistrationNumbers;
                }
                else
                {
                    vehicleRepository.Add(vehicle);
                    vehicleRepository.SaveChanges();
                    return VehicleServiceResponse.SuccessOperationAdd;
                }
            }
            catch (Exception)
            {
                return VehicleServiceResponse.ErrorOperationAdd;
            }
        }
    }
}
