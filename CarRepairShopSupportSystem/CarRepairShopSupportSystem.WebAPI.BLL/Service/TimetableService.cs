using CarRepairShopSupportSystem.WebAPI.BLL.IService;
using CarRepairShopSupportSystem.WebAPI.DAL.Abstraction;
using CarRepairShopSupportSystem.WebAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopSupportSystem.WebAPI.BLL.Service
{
    public class TimetableService : ITimetableService
    {
        private readonly IRepository<Timetable> timetableRepository;
        private readonly IRepository<User> userRepository;

        public TimetableService(IRepository<Timetable> timetableRepository, IRepository<User> userRepository)
        {
            this.timetableRepository = timetableRepository;
            this.userRepository = userRepository;
        }

        public void SaveTimetableForUser(Timetable timetable)
        {
            DateTime dateTimeFriday = timetable.DateTime.AddDays(4);
            int currentUserId = timetable.WorkingUsers.First().UserId;
            IEnumerable<Timetable> existingTimetables = timetableRepository.FindBy(t => t.DateTime.Year == timetable.DateTime.Year
                                                                                    && t.DateTime.Month == timetable.DateTime.Month
                                                                                    && t.DateTime.Day >= timetable.DateTime.Day
                                                                                    && t.DateTime.Day <= dateTimeFriday.Day
                                                                                    && t.WorkingUsers.Any(u => u.UserId == currentUserId));

            foreach (Timetable item in existingTimetables)
            {
                timetableRepository.RemoveManyToMany<User>(t => t.TimetableId == item.TimetableId
                                                            , nameof(timetable.WorkingUsers)
                                                            , nameof(User.UserId)
                                                            , new User[] { timetable.WorkingUsers.First() });
            }
            timetableRepository.SaveChanges();

            IList<Timetable> newTimetables = new List<Timetable>();
            for (int d = timetable.DateTime.Day; d < timetable.DateTime.Day + 5; d++)
            {
                for (int h = timetable.DateTime.Hour; h < timetable.DateTime.Hour + 8; h++)
                {
                    if (!timetableRepository.Any(t => t.DateTime.Year == timetable.DateTime.Year 
                                                    && t.DateTime.Month == timetable.DateTime.Month 
                                                    && t.DateTime.Day == d 
                                                    && t.DateTime.Hour == h))
                    {
                        Timetable newTimetable = new Timetable
                        {
                            DateTime = new DateTime(timetable.DateTime.Year, timetable.DateTime.Month, d, h, 0, 0),
                            NumberOfEmployeesForCustomer = timetable.NumberOfEmployeesForCustomer,
                            NumberOfEmployeesForManager = timetable.NumberOfEmployeesForManager,
                            NumberOfEmployeesReservedForCustomer = timetable.NumberOfEmployeesReservedForCustomer,
                            NumberOfEmployeesReservedForManager = timetable.NumberOfEmployeesReservedForManager
                        };
                        newTimetables.Add(newTimetable);
                    }
                }
            }

            if (newTimetables.Count > 0)
            {
                timetableRepository.AddRange(newTimetables);
                timetableRepository.SaveChanges();
            }

            for (int d = timetable.DateTime.Day; d < timetable.DateTime.Day + 5; d++)
            {
                for (int h = timetable.DateTime.Hour; h < timetable.DateTime.Hour + 8; h++)
                {
                    timetableRepository.EditManyToMany<User>(t => t.DateTime.Year == timetable.DateTime.Year
                                                               && t.DateTime.Month == timetable.DateTime.Month
                                                               && t.DateTime.Day == d
                                                               && t.DateTime.Hour == h
                                                                , nameof(timetable.WorkingUsers)
                                                                , nameof(User.UserId)
                                                                , new User[] { timetable.WorkingUsers.First() });
                }
            }
            timetableRepository.SaveChanges();
        }
    }
}
