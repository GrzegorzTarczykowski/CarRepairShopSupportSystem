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
    }
}
