using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopSupportSystem.WebAPI.BLL.Enums
{
    public enum RegisterServiceResponse
    {
        SuccessRegister,
        DuplicateUsername,
        DuplicateEmail,
        ErrorRegister
    }
}
