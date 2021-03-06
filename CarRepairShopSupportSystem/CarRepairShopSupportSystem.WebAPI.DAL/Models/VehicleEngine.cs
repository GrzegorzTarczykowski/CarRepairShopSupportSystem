﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopSupportSystem.WebAPI.DAL.Models
{
    public class VehicleEngine
    {
        public int VehicleEngineId { get; set; }
        public decimal PowerKW { get; set; }
        public decimal CapacityCCM { get; set; }
        public string EngineCode { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
