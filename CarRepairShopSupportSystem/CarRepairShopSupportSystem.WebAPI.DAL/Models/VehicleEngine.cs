using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopSupportSystem.WebAPI.DAL.Models
{
    public class VehicleEngine
    {
        public int VehicleEngineId { get; set; }
        public decimal Power { get; set; }
        public decimal Capacity { get; set; }
        public decimal Torque { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
