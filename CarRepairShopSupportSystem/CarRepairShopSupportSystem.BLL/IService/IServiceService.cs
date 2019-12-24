using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRepairShopSupportSystem.BLL.Models;

namespace CarRepairShopSupportSystem.BLL.IService
{
    public interface IServiceService
    {
        OperationResult AddService(Models.Service service);
        OperationResult DeleteService(int serviceId);
        OperationResult EditService(Models.Service service);
        IEnumerable<Models.Service> GetAllServiceList();
    }
}
