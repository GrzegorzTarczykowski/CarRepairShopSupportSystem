using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopSupportSystem.WebAPI.DAL.Models
{
    public class Timetable
    {
        public int TimetableId { get; set; }
        public DateTime DateTime { get; set; }
        public int NumberOfEmployeesForCustomer { get; set; }
        public int NumberOfEmployeesReservedForCustomer { get; set; }
        public int NumberOfEmployeesForManager { get; set; }
        public int NumberOfEmployeesReservedForManager { get; set; }
    }
}
