using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopSupportSystem.WebAPI.DAL.Models
{
    public class VehicleModel
    {
        public int VehicleModelId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
