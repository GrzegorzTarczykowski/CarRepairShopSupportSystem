using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopSupportSystem.BLL.IService
{
    public interface IServiceService
    {
        IEnumerable<Models.Service> GetAllServiceList();
    }
}
