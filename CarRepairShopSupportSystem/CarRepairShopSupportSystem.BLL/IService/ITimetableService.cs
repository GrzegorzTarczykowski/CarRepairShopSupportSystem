using CarRepairShopSupportSystem.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopSupportSystem.BLL.IService
{
    public interface ITimetableService
    {
        IEnumerable<Timetable> GetTimetableListPerHour(int year, int month, int day, int hour);
        IEnumerable<Timetable> GetTimetableListPerDay(int year, int month, int day);
        IEnumerable<Timetable> GetTimetableListPerMonth(int year, int month);
    }
}
