using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRepairShopSupportSystem.WebAPI.Models
{
    public class Service
    {
        public int ServiceId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int ExecutionTimeInMinutes { get; set; }
    }
}