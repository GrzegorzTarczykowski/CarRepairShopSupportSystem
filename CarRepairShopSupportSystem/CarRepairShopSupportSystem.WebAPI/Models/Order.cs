using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRepairShopSupportSystem.WebAPI.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public decimal TotalCost { get; set; }
        public string Description { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? PlannedStartDateOfRepair { get; set; }
        public DateTime? StartDateOfRepair { get; set; }
        public DateTime? PlannedEndDateOfRepair { get; set; }
        public DateTime? EndDateOfRepair { get; set; }
        public int OrderStatusId { get; set; }
        public string OrderStatusName { get; set; }
        public int VehicleId { get; set; }
    }
}