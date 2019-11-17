using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRepairShopSupportSystem.WebAPI.Models
{
    public class Vehicle
    {
        public int VehicleId { get; set; }
        public int EngineMileage { get; set; }
        public string RegistrationNumbers { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public int VehicleBrandId { get; set; }
        public string VehicleBrandName { get; set; }
        public int VehicleColourId { get; set; }
        public int VehicleEngineId { get; set; }
        public string VehicleEngineCode { get; set; }
        public int VehicleFuelId { get; set; }
        public string VehicleFuelName { get; set; }
        public int VehicleModelId { get; set; }
        public string VehicleModelName { get; set; }
        public int VehicleTypeId { get; set; }
        public string VehicleTypeName { get; set; }
    }
}