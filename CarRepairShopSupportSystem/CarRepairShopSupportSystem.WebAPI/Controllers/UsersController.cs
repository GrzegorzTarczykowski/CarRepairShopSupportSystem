using CarRepairShopSupportSystem.WebAPI.Controllers.Abstraction;
using CarRepairShopSupportSystem.WebAPI.DAL.Abstraction;
using CarRepairShopSupportSystem.WebAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CarRepairShopSupportSystem.WebAPI.Controllers
{
    public class UsersController : ABaseApiController<User>
    {
        //new Repository<T>(new MsSqlServerContext(), new UnitOfWork())
        public UsersController(IRepository<User> repository) : base(repository)
        {
        }

        // GET api/<controller>
        public IEnumerable<User> Get()
        {
            return GetBase();
        }

        // GET api/<controller>/5
        public User Get(int id)
        {
            return GetBase(id);
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]User value)
        {
            return PostBase(value);
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put(int id, [FromBody]User value)
        {
            return PutBase(id == value.UserId, value);
        }

        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(int id)
        {
            return DeleteBase(id);
        }
    }
}