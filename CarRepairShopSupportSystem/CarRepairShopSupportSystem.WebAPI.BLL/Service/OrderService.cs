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
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> orderRepository;

        public OrderService(IRepository<Order> vehicleRepository)
        {
            this.orderRepository = vehicleRepository;
        }

        public IEnumerable<Order> GetOrderListByVehicleId(int vehicleId)
        {
            return orderRepository.GetAll(nameof(OrderStatus)).Where(o => o.VehicleId == vehicleId);
        }

        public IEnumerable<Order> GetOrderListByWorker(int userId)
        {
            return orderRepository.GetAll(nameof(OrderStatus)).Where(o => o.WorkByUsers.Any(u => u.UserId == userId));
        }

        public bool AddOrder(Order order)
        {
            ICollection<User> workByUsers = order.WorkByUsers;
            order.WorkByUsers = null;
            ICollection<DAL.Models.Service> containsServices = order.ContainsServices;
            order.ContainsServices = null;
            ICollection<VehiclePart> usedVehicleParts = order.UsedVehicleParts;
            order.UsedVehicleParts = null;
            orderRepository.Add(order);
            orderRepository.SaveChanges();

            if (workByUsers != null)
            {
                orderRepository.EditManyToMany<User>(o => o.OrderId == order.OrderId
                                        , nameof(order.WorkByUsers)
                                        , nameof(User.UserId)
                                        , workByUsers);
                orderRepository.SaveChanges();
            }
            if (containsServices != null)
            {
                orderRepository.EditManyToMany<DAL.Models.Service>(o => o.OrderId == order.OrderId
                                        , nameof(order.ContainsServices)
                                        , nameof(DAL.Models.Service.ServiceId)
                                        , containsServices);
                orderRepository.SaveChanges();
            }
            if (usedVehicleParts != null)
            {
                orderRepository.EditManyToMany<VehiclePart>(o => o.OrderId == order.OrderId
                                        , nameof(order.UsedVehicleParts)
                                        , nameof(VehiclePart.VehiclePartId)
                                        , usedVehicleParts);
                orderRepository.SaveChanges();
            }
            return true;
        }

        public bool EditOrder(Order order)
        {
            ICollection<User> workByUsers = order.WorkByUsers;
            order.WorkByUsers = null;
            ICollection<DAL.Models.Service> containsServices = order.ContainsServices;
            order.ContainsServices = null;
            ICollection<VehiclePart> usedVehicleParts = order.UsedVehicleParts;
            order.UsedVehicleParts = null;
            orderRepository.Edit(order);
            orderRepository.SaveChanges();

            if (workByUsers != null)
            {
                orderRepository.EditManyToMany<User>(o => o.OrderId == order.OrderId
                                        , nameof(order.WorkByUsers)
                                        , nameof(User.UserId)
                                        , workByUsers);
                orderRepository.SaveChanges();
            }
            if (containsServices != null)
            {
                orderRepository.EditManyToMany<DAL.Models.Service>(o => o.OrderId == order.OrderId
                                        , nameof(order.ContainsServices)
                                        , nameof(DAL.Models.Service.ServiceId)
                                        , containsServices);
                orderRepository.SaveChanges();
            }
            if (usedVehicleParts != null)
            {
                orderRepository.EditManyToMany<VehiclePart>(o => o.OrderId == order.OrderId
                                        , nameof(order.UsedVehicleParts)
                                        , nameof(VehiclePart.VehiclePartId)
                                        , usedVehicleParts);
                orderRepository.SaveChanges();
            }
            return true;
        }
    }
}
