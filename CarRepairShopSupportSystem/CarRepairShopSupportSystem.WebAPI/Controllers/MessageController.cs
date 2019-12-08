using CarRepairShopSupportSystem.WebAPI.BLL.IService;
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
    public class MessageController : ACRUDApiController<Message>
    {
        private readonly IMessageService messageService;

        public MessageController(IRepository<Message> repository, IMessageService messageService) : base(repository)
        {
            this.messageService = messageService;
        }

        [Route("api/Order/GetByOrderIdAndUserReceiverId")]
        [Authorize(Roles = "SuperAdmin, Admin, User")]
        [HttpGet]
        public IEnumerable<Models.Message> GetByOrderIdAndUserReceiverId([FromUri]int orderId, int userReceiverId)
        {
            return messageService.GetMessageListByOrderIdAndUserReceiverId(orderId, userReceiverId)
                ?.Select(m => new Models.Message
                {
                    MessageId = m.MessageId,
                    Title = m.Title,
                    Content = m.Content,
                    SentDate = m.SentDate,
                    OrderId = m.OrderId,
                    UserSenderId = m.UserSenderId,
                    UserReceiverId = m.UserReceiverId
                });
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpGet]
        // GET api/<controller>/5
        public Message Get(int id)
        {
            return GetBase(id);
        }

        [Authorize(Roles = "SuperAdmin, Admin, User")]
        [HttpPost]
        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]Message value)
        {
            return PostBase(value);
        }

        [Authorize(Roles = "SuperAdmin, Admin, User")]
        [HttpPut]
        // PUT api/<controller>/5
        public HttpResponseMessage Put(int id, [FromBody]Message value)
        {
            return PutBase(id == value.OrderId, value);
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