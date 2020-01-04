using CarRepairShopSupportSystem.WebAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopSupportSystem.WebAPI.BLL.IService
{
    public interface IOrderService
    {
        IEnumerable<Order> GetOrderListByVehicleId(int vehicleId);
        IEnumerable<Order> GetOrderListByWorker(int userId);
        bool AddOrder(Order order);
        bool EditOrder(Order order);
    }
}
