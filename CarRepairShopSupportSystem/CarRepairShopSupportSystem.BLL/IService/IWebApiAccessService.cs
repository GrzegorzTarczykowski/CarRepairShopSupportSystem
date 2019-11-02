using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopSupportSystem.BLL.IService
{
    public interface IWebApiAccessService
    {
        void CheckAccess();
        void SetAccess();
    }
}
