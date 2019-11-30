using CarRepairShopSupportSystem.WebAPI.Controllers.Abstraction;
using CarRepairShopSupportSystem.WebAPI.DAL.Abstraction;
using CarRepairShopSupportSystem.WebAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace CarRepairShopSupportSystem.WebAPI.Controllers
{
    public class TimetableController : ACRUDApiController<Timetable>
    {
        public TimetableController(IRepository<Timetable> repository) : base(repository)
        {
        }

        [Route("api/Timetable/GetPerHour")]
        [Authorize(Roles = "SuperAdmin, Admin, User")]
        [HttpGet]
        // GET api/<controller>
        public IEnumerable<Timetable> GetPerHour([FromUri]int year, int month, int day, int hour)
        {
            return GetByBase(t => t.DateTime == new DateTime(year, month, day, hour, 0, 0));
        }

        [Route("api/Timetable/GetPerDay")]
        [Authorize(Roles = "SuperAdmin, Admin, User")]
        [HttpGet]
        // GET api/<controller>
        public IEnumerable<Timetable> GetPerDay([FromUri]int year, int month, int day)
        {
            return GetByBase(t => t.DateTime == new DateTime(year, month, day));
        }

        [Route("api/Timetable/GetPerMonth")]
        [Authorize(Roles = "SuperAdmin, Admin, User")]
        [HttpGet]
        // GET api/<controller>
        public IEnumerable<Timetable> GetPerMonth([FromUri]int year, int month)
        {
            return GetByBase(t => t.DateTime == new DateTime(year, month, 0));
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
        public HttpResponseMessage Post([FromBody]Timetable value)
        {
            return PostBase(value);
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