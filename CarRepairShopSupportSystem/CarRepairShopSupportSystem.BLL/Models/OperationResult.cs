using CarRepairShopSupportSystem.BLL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopSupportSystem.BLL.Models
{
    public class OperationResult
    {
        public ResultCode ResultCode { get; set; }
        public string Message { get; set; }
    }
}
