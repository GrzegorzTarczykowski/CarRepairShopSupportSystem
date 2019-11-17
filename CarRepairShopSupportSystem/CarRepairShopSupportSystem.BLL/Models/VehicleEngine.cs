using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopSupportSystem.BLL.Models
{
    public class VehicleEngine
    {
        public int VehicleEngineId { get; set; }
        public decimal PowerKW { get; set; }
        public decimal CapacityCCM { get; set; }
        public decimal Torque { get; set; }
        public string EngineCode { get; set; }
    }
}
