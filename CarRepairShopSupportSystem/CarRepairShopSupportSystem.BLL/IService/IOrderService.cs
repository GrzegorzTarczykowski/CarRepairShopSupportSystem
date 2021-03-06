﻿using CarRepairShopSupportSystem.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopSupportSystem.BLL.IService
{
    public interface IOrderService
    {
        OperationResult AddVehicleOrder(Order order);
        OperationResult EditVehicleOrder(Order order);
        IEnumerable<Order> GetAllOrderList();
        Order GetOrderByOrderId(int orderId);
        IEnumerable<Order> GetOrderListByVehicleId(int vehicleId);
        IEnumerable<Order> GetOrderListByWorker(int userId);
    }
}
