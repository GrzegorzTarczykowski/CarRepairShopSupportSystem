﻿using System;
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
    
    public enum VehicleServiceResponse
    {
        SuccessOperationAdd,
        DuplicateRegistrationNumbers,
        ErrorOperationAdd
    }

    public enum OrderServiceResponse
    {
        SuccessOperationAdd,
        ErrorOperationAdd
    }
}
