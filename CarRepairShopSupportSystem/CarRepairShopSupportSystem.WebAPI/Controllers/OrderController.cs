﻿using CarRepairShopSupportSystem.WebAPI.BLL.IService;
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
            return GetBase(nameof(OrderStatus), nameof(Vehicle))
                ?.Select(o => new Models.Order
                {
                    OrderId = o.OrderId,
                    TotalCost = o.TotalCost,
                    Description = o.Description,
                    CreateDate = o.CreateDate,
                    StartDateOfRepair = o.StartDateOfRepair,
                    EndDateOfRepair = o.EndDateOfRepair,
                    OrderStatusId = o.OrderStatusId,
                    OrderStatusName = o.OrderStatus?.Name,
                    VehicleId = o.VehicleId,
                    VehicleRegistrationNumbers = o.Vehicle?.RegistrationNumbers,
                    WorkByUsers = o.WorkByUsers?.Select(u => new Models.User
                    {
                        UserId = u.UserId,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        PermissionId = u.PermissionId
                    }),
                    ContainsServices = o.ContainsServices?.Select(s => new Models.Service
                    {
                        ServiceId = s.ServiceId,
                        Name = s.Name,
                        Description = s.Description,
                        ExecutionTimeInMinutes = s.ExecutionTimeInMinutes,
                        Price = s.Price
                    }),
                    UsedVehicleParts = o.UsedVehicleParts?.Select(vp => new Models.VehiclePart
                    {
                        VehiclePartId = vp.VehiclePartId,
                        Name = vp.Name,
                        Price = vp.Price
                    })
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
                    StartDateOfRepair = o.StartDateOfRepair,
                    EndDateOfRepair = o.EndDateOfRepair,
                    OrderStatusId = o.OrderStatusId,
                    OrderStatusName = o.OrderStatus?.Name,
                    VehicleId = o.VehicleId,
                    VehicleRegistrationNumbers = o.Vehicle?.RegistrationNumbers,
                    WorkByUsers = o.WorkByUsers?.Select(u => new Models.User
                    {
                        UserId = u.UserId,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        PermissionId = u.PermissionId
                    }),
                    ContainsServices = o.ContainsServices?.Select(s => new Models.Service
                    {
                        ServiceId = s.ServiceId,
                        Name = s.Name,
                        Description = s.Description,
                        ExecutionTimeInMinutes = s.ExecutionTimeInMinutes,
                        Price = s.Price
                    }),
                    UsedVehicleParts = o.UsedVehicleParts?.Select(vp => new Models.VehiclePart
                    {
                        VehiclePartId = vp.VehiclePartId,
                        Name = vp.Name,
                        Price = vp.Price
                    })
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
                    StartDateOfRepair = o.StartDateOfRepair,
                    EndDateOfRepair = o.EndDateOfRepair,
                    OrderStatusId = o.OrderStatusId,
                    OrderStatusName = o.OrderStatus?.Name,
                    VehicleId = o.VehicleId,
                    VehicleRegistrationNumbers = o.Vehicle?.RegistrationNumbers,
                    WorkByUsers = o.WorkByUsers?.Select(u => new Models.User
                    {
                        UserId = u.UserId,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        PermissionId = u.PermissionId
                    }),
                    ContainsServices = o.ContainsServices?.Select(s => new Models.Service
                    {
                        ServiceId = s.ServiceId,
                        Name = s.Name,
                        Description = s.Description,
                        ExecutionTimeInMinutes = s.ExecutionTimeInMinutes,
                        Price = s.Price
                    }),
                    UsedVehicleParts = o.UsedVehicleParts?.Select(vp => new Models.VehiclePart
                    {
                        VehiclePartId = vp.VehiclePartId,
                        Name = vp.Name,
                        Price = vp.Price
                    })
                });
        }

        [Authorize(Roles = "SuperAdmin, Admin, User")]
        [HttpGet]
        // GET api/<controller>/5
        public Models.Order Get(int id)
        {
            var order = GetBase(id);
            return new Models.Order
            {
                OrderId = order.OrderId,
                TotalCost = order.TotalCost,
                Description = order.Description,
                CreateDate = order.CreateDate,
                StartDateOfRepair = order.StartDateOfRepair,
                EndDateOfRepair = order.EndDateOfRepair,
                OrderStatusId = order.OrderStatusId,
                OrderStatusName = order.OrderStatus?.Name,
                VehicleId = order.VehicleId,
                VehicleRegistrationNumbers = order.Vehicle?.RegistrationNumbers,
                WorkByUsers = order.WorkByUsers?.Select(u => new Models.User
                {
                    UserId = u.UserId,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    PermissionId = u.PermissionId
                }),
                ContainsServices = order.ContainsServices?.Select(s => new Models.Service
                {
                    ServiceId = s.ServiceId,
                    Name = s.Name,
                    Description = s.Description,
                    ExecutionTimeInMinutes = s.ExecutionTimeInMinutes,
                    Price = s.Price
                }),
                UsedVehicleParts = order.UsedVehicleParts?.Select(vp => new Models.VehiclePart
                {
                    VehiclePartId = vp.VehiclePartId,
                    Name = vp.Name,
                    Price = vp.Price
                })
            };
        }

        [Authorize(Roles = "SuperAdmin, Admin, User")]
        [HttpPost]
        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]Models.Order value)
        {
            Order order = new Order
            {
                OrderId = value.OrderId,
                TotalCost = value.TotalCost,
                Description = value.Description,
                CreateDate = value.CreateDate,
                StartDateOfRepair = value.StartDateOfRepair,
                EndDateOfRepair = value.EndDateOfRepair,
                OrderStatusId = value.OrderStatusId,
                VehicleId = value.VehicleId,
                WorkByUsers = value.WorkByUsers?.Select(u => new User
                {
                    UserId = u.UserId
                }).ToList(),
                ContainsServices = value.ContainsServices?.Select(s => new Service
                {
                    ServiceId = s.ServiceId
                }).ToList(),
                UsedVehicleParts = value.UsedVehicleParts?.Select(vp => new VehiclePart
                {
                    VehiclePartId = vp.VehiclePartId
                }).ToList()
            };

            if (orderService.AddOrder(order))
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Nieoczekiwany bład");
            }
        }

        [Authorize(Roles = "SuperAdmin, Admin, User")]
        [HttpPut]
        // PUT api/<controller>/5
        public HttpResponseMessage Put(int id, [FromBody]Models.Order value)
        {
            Order order = new Order
            {
                OrderId = value.OrderId,
                TotalCost = value.TotalCost,
                Description = value.Description,
                CreateDate = value.CreateDate,
                StartDateOfRepair = value.StartDateOfRepair,
                EndDateOfRepair = value.EndDateOfRepair,
                OrderStatusId = value.OrderStatusId,
                VehicleId = value.VehicleId,
                WorkByUsers = value.WorkByUsers?.Select(u => new User
                {
                    UserId = u.UserId
                }).ToList(),
                ContainsServices = value.ContainsServices?.Select(s => new Service
                {
                    ServiceId = s.ServiceId
                }).ToList(),
                UsedVehicleParts = value.UsedVehicleParts?.Select(vp => new VehiclePart
                {
                    VehiclePartId = vp.VehiclePartId
                }).ToList()
            };

            if (id == order.OrderId && orderService.EditOrder(order))
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Nieoczekiwany bład");
            }
        }

        [Authorize(Roles = "SuperAdmin, Admin, User")]
        [HttpDelete]
        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(int id)
        {
            return DeleteBase(id);
        }
    }
}