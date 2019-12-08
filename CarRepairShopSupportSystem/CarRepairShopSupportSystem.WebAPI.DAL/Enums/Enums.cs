using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopSupportSystem.WebAPI.DAL.Enums
{
    public enum PermissionId
    {
        SuperAdmin = 1,
        Admin = 2,
        User = 3,
        Guest = 4
    }

    public enum OrderStatusId
    {
        Planned = 1,
        InProgress = 2,
        Completed = 3
    }
}
