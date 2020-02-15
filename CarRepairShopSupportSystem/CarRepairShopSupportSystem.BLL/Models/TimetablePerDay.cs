using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopSupportSystem.BLL.Models
{
    public class TimetablePerDay
    {
        public int Day { get; set; }
        public int SumNumberOfEmployeesForCustomer { get; set; }
        public int SumNumberOfEmployeesReservedForCustomer { get; set; }
        public int SumNumberOfEmployeesForManager { get; set; }
        public int SumNumberOfEmployeesReservedForManager { get; set; }

        public bool IsAvailableEmployeesForCustomer => SumNumberOfEmployeesForCustomer - SumNumberOfEmployeesReservedForCustomer > 0;
        public bool IsAvailableOfEmployeesForManager => SumNumberOfEmployeesForManager - SumNumberOfEmployeesReservedForManager > 0;
    }
}
