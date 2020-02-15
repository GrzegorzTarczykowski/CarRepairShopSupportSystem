using CarRepairShopSupportSystem.WebAPI.BLL.IService;
using CarRepairShopSupportSystem.WebAPI.Controllers.Abstraction;
using CarRepairShopSupportSystem.WebAPI.DAL.Abstraction;
using CarRepairShopSupportSystem.WebAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace CarRepairShopSupportSystem.WebAPI.Controllers
{
    public class TimetableController : ACRUDApiController<Timetable>
    {
        private readonly ITimetableService timetableService;

        public TimetableController(IRepository<Timetable> repository, ITimetableService timetableService) : base(repository)
        {
            this.timetableService = timetableService;
        }

        [Route("api/Timetable/GetPerHour")]
        [Authorize(Roles = "SuperAdmin, Admin, User")]
        [HttpGet]
        // GET api/<controller>
        public IEnumerable<Models.Timetable> GetPerHour([FromUri]int year, int month, int day, int hour)
        {
            DateTime dateTimeFrom = new DateTime(year, month, day, hour, 0, 0);
            DateTime dateTimeTo = new DateTime(year, month, day, hour, 0, 0).AddHours(1);
            return GetByBase(t => t.DateTime >= dateTimeFrom && t.DateTime < dateTimeTo)
                   ?.Select(x => new Models.Timetable
                   {
                       TimetableId = x.TimetableId,
                       DateTime = x.DateTime,
                       NumberOfEmployeesForCustomer = x.NumberOfEmployeesForCustomer,
                       NumberOfEmployeesForManager = x.NumberOfEmployeesForManager,
                       NumberOfEmployeesReservedForCustomer = x.NumberOfEmployeesReservedForCustomer,
                       NumberOfEmployeesReservedForManager = x.NumberOfEmployeesReservedForManager,
                       WorkingUsersId = x.WorkingUsers?.Select(u => u.UserId)
                   });
        }

        [Route("api/Timetable/GetPerDay")]
        [Authorize(Roles = "SuperAdmin, Admin, User")]
        [HttpGet]
        // GET api/<controller>
        public IEnumerable<Models.Timetable> GetPerDay([FromUri]int year, int month, int day)
        {
            DateTime dateTimeFrom = new DateTime(year, month, day);
            DateTime dateTimeTo = new DateTime(year, month, day).AddDays(1);
            return GetByBase(t => t.DateTime >= dateTimeFrom && t.DateTime < dateTimeTo)
                   ?.Select(x => new Models.Timetable
                   {
                       TimetableId = x.TimetableId,
                       DateTime = x.DateTime,
                       NumberOfEmployeesForCustomer = x.NumberOfEmployeesForCustomer,
                       NumberOfEmployeesForManager = x.NumberOfEmployeesForManager,
                       NumberOfEmployeesReservedForCustomer = x.NumberOfEmployeesReservedForCustomer,
                       NumberOfEmployeesReservedForManager = x.NumberOfEmployeesReservedForManager,
                       WorkingUsersId = x.WorkingUsers?.Select(u => u.UserId)
                   });
        }

        [Route("api/Timetable/GetPerMonth")]
        [Authorize(Roles = "SuperAdmin, Admin, User")]
        [HttpGet]
        // GET api/<controller>
        public IEnumerable<Models.Timetable> GetPerMonth([FromUri]int year, int month)
        {
            DateTime dateTimeFrom = new DateTime(year, month, 1);
            DateTime dateTimeTo = new DateTime(year, month, 1).AddMonths(1);
            return GetByBase(t => t.DateTime >= dateTimeFrom && t.DateTime < dateTimeTo)
                   ?.Select(x => new Models.Timetable
                   {
                       TimetableId = x.TimetableId,
                       DateTime = x.DateTime,
                       NumberOfEmployeesForCustomer = x.NumberOfEmployeesForCustomer,
                       NumberOfEmployeesForManager = x.NumberOfEmployeesForManager,
                       NumberOfEmployeesReservedForCustomer = x.NumberOfEmployeesReservedForCustomer,
                       NumberOfEmployeesReservedForManager = x.NumberOfEmployeesReservedForManager,
                       WorkingUsersId = x.WorkingUsers?.Select(u => u.UserId)
                   });
        }

        [Route("api/Timetable/GetPerYear")]
        [Authorize(Roles = "SuperAdmin, Admin, User")]
        [HttpGet]
        // GET api/<controller>
        public IEnumerable<Models.Timetable> GetPerYear([FromUri]int year)
        {
            DateTime dateTimeFrom = new DateTime(year, 1, 1);
            DateTime dateTimeTo = new DateTime(year, 1, 1).AddYears(1);
            return GetByBase(t => t.DateTime >= dateTimeFrom && t.DateTime < dateTimeTo)
                   ?.Select(x => new Models.Timetable
                   {
                       TimetableId = x.TimetableId,
                       DateTime = x.DateTime,
                       NumberOfEmployeesForCustomer = x.NumberOfEmployeesForCustomer,
                       NumberOfEmployeesForManager = x.NumberOfEmployeesForManager,
                       NumberOfEmployeesReservedForCustomer = x.NumberOfEmployeesReservedForCustomer,
                       NumberOfEmployeesReservedForManager = x.NumberOfEmployeesReservedForManager,
                       WorkingUsersId = x.WorkingUsers?.Select(u => u.UserId)
                   });
        }

        [Route("api/Timetable/GetPerYearByUserId")]
        [Authorize(Roles = "SuperAdmin")]
        [HttpGet]
        // GET api/<controller>
        public IEnumerable<Models.Timetable> GetPerYearByUserId([FromUri]int year, int userId)
        {
            DateTime dateTimeFrom = new DateTime(year, 1, 1);
            DateTime dateTimeTo = new DateTime(year, 1, 1).AddYears(1);
            return GetByBase(t => t.DateTime >= dateTimeFrom && t.DateTime < dateTimeTo && t.WorkingUsers.Any(u => u.UserId == userId))
                   ?.Select(x => new Models.Timetable
                   {
                       TimetableId = x.TimetableId,
                       DateTime = x.DateTime,
                       NumberOfEmployeesForCustomer = x.NumberOfEmployeesForCustomer,
                       NumberOfEmployeesForManager = x.NumberOfEmployeesForManager,
                       NumberOfEmployeesReservedForCustomer = x.NumberOfEmployeesReservedForCustomer,
                       NumberOfEmployeesReservedForManager = x.NumberOfEmployeesReservedForManager,
                       WorkingUsersId = x.WorkingUsers?.Select(u => u.UserId)
                   });
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpGet]
        // GET api/<controller>/5
        public Timetable Get(int id)
        {
            return GetBase(id);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]Models.Timetable value)
        {
            timetableService.SaveTimetableForUser(new Timetable
            {
                TimetableId = value.TimetableId,
                DateTime = value.DateTime,
                NumberOfEmployeesForCustomer = value.NumberOfEmployeesForCustomer,
                NumberOfEmployeesForManager = value.NumberOfEmployeesForManager,
                NumberOfEmployeesReservedForCustomer = value.NumberOfEmployeesReservedForCustomer,
                NumberOfEmployeesReservedForManager = value.NumberOfEmployeesReservedForManager,
                WorkingUsers = value.WorkingUsersId?.Select(u => new User { UserId = u })?.ToList()
            });
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPut]
        // PUT api/<controller>/5
        public HttpResponseMessage Put(int id, [FromBody]Timetable value)
        {
            return PutBase(id == value.TimetableId, value);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpDelete]
        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(int id)
        {
            return DeleteBase(id);
        }
    }
}