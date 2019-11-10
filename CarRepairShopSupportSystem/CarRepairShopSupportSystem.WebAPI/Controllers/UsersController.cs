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
    public class UsersController : ABaseApiController<User>
    {
        private readonly ILoginService loginService;
        private readonly IRegisterService registerService;

        public UsersController(IRepository<User> repository, ILoginService loginService, IRegisterService registerService) : base(repository)
        {
            this.loginService = loginService;
            this.registerService = registerService;
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpGet]
        // GET api/<controller>
        public IEnumerable<User> Get()
        {
            return GetBase();
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpGet]
        // GET api/<controller>/5
        public User Get(int id)
        {
            return GetBase(id);
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
                    PermissionName = user.Permission.Name
                };
            }
            return null;
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpGet]
        // GET api/<controller>?username=username
        public HttpResponseMessage GetIsAnyByUsername([FromUri]string username)
        {
            return GetIsAny(u => u.Username == username);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpGet]
        // GET api/<controller>?email=email
        public HttpResponseMessage GetIsAnyByEmail([FromUri]string email)
        {
            return GetIsAny(u => u.Email == email);
        }

        [Authorize(Roles = "SuperAdmin, Admin, User, Guest")]
        [HttpPost]
        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]Models.User value)
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
            RegisterServiceResponse registerServiceResponse =  registerService.Register(user);
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