using CarRepairShopSupportSystem.WebAPI.BLL.Enums;
using CarRepairShopSupportSystem.WebAPI.BLL.IService;
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
    public class UserController : ACRUDApiController<User>
    {
        private readonly ILoginService loginService;
        private readonly IRegisterService registerService;
        private readonly IUserService userService;

        public UserController(IRepository<User> repository, ILoginService loginService, IRegisterService registerService, IUserService userService) : base(repository)
        {
            this.loginService = loginService;
            this.registerService = registerService;
            this.userService = userService;
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpGet]
        // GET api/<controller>
        public IEnumerable<Models.User> Get()
        {
            return GetBase()?.Select(u => new Models.User
            {
                Username = u.Username,
                Password = u.Password,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber
            });
        }

        [Route("api/User/GetAllWorkerList")]
        [Authorize(Roles = "SuperAdmin")]
        [HttpGet]
        public IEnumerable<Models.User> GetAllWorkerList()
        {
            return userService.GetAllWorkerList()
                ?.Select(u => new Models.User
                {
                    UserId = u.UserId,
                    Username = u.Username,
                    Password = u.Password,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber
                });
        }

        [Authorize(Roles = "SuperAdmin, Admin, User")]
        [HttpGet]
        // GET api/<controller>?username=username&password=password
        public Models.User Get([FromUri]string username, string password)
        {
            User user = loginService.Login(username, password);
            if (user != null)
            {
                return new Models.User()
                {
                    UserId = user.UserId,
                    PermissionId = user.PermissionId,
                    PermissionName = user.Permission.Name,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.Username
                };
            }
            return null;
        }

        [Authorize(Roles = "SuperAdmin, Admin, User, Guest")]
        [HttpPost]
        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]Models.User value)
        {
            return PostBase(delegate
            {
                User user = new User()
                {
                    Username = value.Username,
                    Password = value.Password,
                    FirstName = value.FirstName,
                    LastName = value.LastName,
                    Email = value.Email,
                    PhoneNumber = value.PhoneNumber
                };
                RegisterServiceResponse registerServiceResponse = registerService.Register(user);
                switch (registerServiceResponse)
                {
                    case RegisterServiceResponse.SuccessRegister:
                        return new HttpResponseMessage(HttpStatusCode.OK);
                    case RegisterServiceResponse.DuplicateUsername:
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Istnieje juz użykownik o podanej nazwie");
                    case RegisterServiceResponse.DuplicateEmail:
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Istnieje juz użykownik o podanym emailu");
                    case RegisterServiceResponse.ErrorRegister:
                        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Nieoczekiwany bład");
                    default:
                        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Nieoczekiwany bład");
                }
            });
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPut]
        // PUT api/<controller>/5
        public HttpResponseMessage Put(int id, [FromBody]User value)
        {
            return PutBase(id == value.UserId, value);
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