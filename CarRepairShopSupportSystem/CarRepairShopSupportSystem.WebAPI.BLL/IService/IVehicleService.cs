using CarRepairShopSupportSystem.WebAPI.BLL.Enums;
using CarRepairShopSupportSystem.WebAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopSupportSystem.WebAPI.BLL.IService
{
    public interface IVehicleService
    {
        IEnumerable<Vehicle> GetVehicleListByUserId(int userId);
        VehicleServiceResponse AddUserVehicle(Vehicle vehicle);
    }
}
