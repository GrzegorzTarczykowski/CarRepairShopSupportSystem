using CarRepairShopSupportSystem.BLL.IService;
using CarRepairShopSupportSystem.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopSupportSystem.BLL.Service
{
    public class VehicleService : IVehicleService
    {
        public IEnumerable<Vehicle> GetVehiclesByUserId(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
