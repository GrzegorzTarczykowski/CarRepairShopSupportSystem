using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopSupportSystem.WebAPI.DAL.Models
{
    public class Vehicle
    {
        public int VehicleId { get; set; }
        public int EngineMileage { get; set; }
        public string RegistrationNumbers { get; set; }
        public string Description { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        public int VehicleBrandId { get; set; }
        public VehicleBrand VehicleBrand { get; set; }
        public int VehicleColourId { get; set; }
        public VehicleColour VehicleColour { get; set; }
        public int VehicleEngineId { get; set; }
        public VehicleEngine VehicleEngine { get; set; }
        public int VehicleFuelId { get; set; }
        public VehicleFuel VehicleFuel { get; set; }
        public int VehicleModelId { get; set; }
        public VehicleModel VehicleModel { get; set; }
        public int VehicleTypeId { get; set; }
        public VehicleType VehicleType { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
