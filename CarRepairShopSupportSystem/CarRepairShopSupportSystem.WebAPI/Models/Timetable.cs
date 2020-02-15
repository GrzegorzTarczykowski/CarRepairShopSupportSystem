using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRepairShopSupportSystem.WebAPI.Models
{
    public class Timetable
    {
        public int TimetableId { get; set; }
        public DateTime DateTime { get; set; }
        public int NumberOfEmployeesForCustomer { get; set; }
        public int NumberOfEmployeesReservedForCustomer { get; set; }
        public int NumberOfEmployeesForManager { get; set; }
        public int NumberOfEmployeesReservedForManager { get; set; }
        public IEnumerable<int> WorkingUsersId { get; set; }
    }
}