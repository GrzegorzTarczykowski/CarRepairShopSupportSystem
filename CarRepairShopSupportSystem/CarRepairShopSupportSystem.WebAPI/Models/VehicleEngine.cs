using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRepairShopSupportSystem.WebAPI.Models
{
    public class VehicleEngine
    {
        public int VehicleEngineId { get; set; }
        public decimal PowerKW { get; set; }
        public decimal CapacityCCM { get; set; }
        public string EngineCode { get; set; }
    }
}