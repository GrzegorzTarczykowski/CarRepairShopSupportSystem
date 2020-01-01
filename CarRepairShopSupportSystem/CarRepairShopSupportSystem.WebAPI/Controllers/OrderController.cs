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
using System.Web;
using System.Web.Http;

namespace CarRepairShopSupportSystem.WebAPI.Controllers
{
    public class OrderController : ACRUDApiController<Order>
    {
        private readonly IOrderService orderService;

        public OrderController(IRepository<Order> repository, IOrderService orderService) : base(repository)
        {
            this.orderService = orderService;
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpGet]
        // GET api/<controller>
        public IEnumerable<Models.Order> Get()
        {
            return GetBase(nameof(OrderStatus))
                ?.Select(o => new Models.Order
                {
                    OrderId = o.OrderId,
                    TotalCost = o.TotalCost,
                    Description = o.Description,
                    CreateDate = o.CreateDate,
                    PlannedStartDateOfRepair = o.PlannedStartDateOfRepair,
                    StartDateOfRepair = o.StartDateOfRepair,
                    PlannedEndDateOfRepair = o.PlannedEndDateOfRepair,
                    EndDateOfRepair = o.EndDateOfRepair,
                    OrderStatusId = o.OrderStatusId,
                    OrderStatusName = o.OrderStatus.Name,
                    VehicleId = o.VehicleId
                });
        }

        [Route("api/Order/GetByVehicleId")]
        [Authorize(Roles = "SuperAdmin, Admin, User")]
        [HttpGet]
        public IEnumerable<Models.Order> GetByVehicleId([FromUri]int vehicleId)
        {
            return orderService.GetOrderListByVehicleId(vehicleId)
                ?.Select(o => new Models.Order
                {
                    OrderId = o.OrderId,
                    TotalCost = o.TotalCost,
                    Description = o.Description,
                    CreateDate = o.CreateDate,
                    PlannedStartDateOfRepair = o.PlannedStartDateOfRepair,
                    StartDateOfRepair = o.StartDateOfRepair,
                    PlannedEndDateOfRepair = o.PlannedEndDateOfRepair,
                    EndDateOfRepair = o.EndDateOfRepair,
                    OrderStatusId = o.OrderStatusId,
                    OrderStatusName = o.OrderStatus.Name,
                    VehicleId = o.VehicleId
                });
        }

        [Route("api/Order/GetOrderListByWorker")]
        [Authorize(Roles = "SuperAdmin, Admin")]
        [HttpGet]
        public IEnumerable<Models.Order> GetOrderListByWorker([FromUri]int userId)
        {
            return orderService.GetOrderListByWorker(userId)
                ?.Select(o => new Models.Order
                {
                    OrderId = o.OrderId,
                    TotalCost = o.TotalCost,
                    Description = o.Description,
                    CreateDate = o.CreateDate,
                    PlannedStartDateOfRepair = o.PlannedStartDateOfRepair,
                    StartDateOfRepair = o.StartDateOfRepair,
                    PlannedEndDateOfRepair = o.PlannedEndDateOfRepair,
                    EndDateOfRepair = o.EndDateOfRepair,
                    OrderStatusId = o.OrderStatusId,
                    OrderStatusName = o.OrderStatus.Name,
                    VehicleId = o.VehicleId
                });
        }

        [Authorize(Roles = "SuperAdmin, Admin, User")]
        [HttpGet]
        // GET api/<controller>/5
        public Order Get(int id)
        {
            return GetBase(id);
        }

        [Authorize(Roles = "SuperAdmin, Admin, User")]
        [HttpPost]
        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]Order value)
        {
            return PostBase(value);
        }

        [Authorize(Roles = "SuperAdmin, Admin, User")]
        [HttpPut]
        // PUT api/<controller>/5
        public HttpResponseMessage Put(int id, [FromBody]Order value)
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