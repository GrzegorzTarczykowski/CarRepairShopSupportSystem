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

    public enum Month
    {
        [Description("Styczeń")]
        January = 1,
        [Description("Luty")]
        February = 2,
        [Description("Marzec")]
        March = 3,
        [Description("Kwiecień")]
        April = 4,
        [Description("Maj")]
        May = 5,
        [Description("Czerwiec")]
        June = 6,
        [Description("Lipiec")]
        July = 7,
        [Description("Sierpień")]
        August = 8,
        [Description("Wrzesień")]
        September = 9,
        [Description("Pażdziernik")]
        October = 10,
        [Description("Listopad")]
        November = 11,
        [Description("Grudzień")]
        December = 12
    }

    public enum OrderStatusId
    {
        [Description("Zaplanowana")]
        Planned = 1,
        [Description("W trakcie")]
        InProgress = 2,
        [Description("Zakończona")]
        Completed = 3,
        [Description("Niezrealizowane")]
        Unrealized = 4
    }

    public enum PermissionId
    {
        SuperAdmin = 1,
        Admin = 2,
        User = 3,
        Guest = 4
    }
}
