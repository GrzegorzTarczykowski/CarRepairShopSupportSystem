using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRepairShopSupportSystem.WebAPI.Models
{
    public class VehiclePart
    {
        public int VehiclePartId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}