using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopSupportSystem.BLL.Enums
{
    public enum ResultCode
    {
        Successful,
        Error
    }

    public enum OperationType
    {
        [Description("Dodaj")]
        Add,
        [Description("Edytuj")]
        Edit
    }
}
