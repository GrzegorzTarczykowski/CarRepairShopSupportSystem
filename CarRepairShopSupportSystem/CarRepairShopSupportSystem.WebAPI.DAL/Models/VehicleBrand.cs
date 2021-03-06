﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopSupportSystem.WebAPI.DAL.Models
{
    public class VehicleBrand
    {
        public int VehicleBrandId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<VehicleModel> VehicleModels { get; set; }
    }
}
