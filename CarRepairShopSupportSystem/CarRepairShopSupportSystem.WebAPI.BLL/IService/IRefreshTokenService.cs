using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopSupportSystem.WebAPI.BLL.IService
{
    public interface IRefreshTokenService
    {
        Task CreateAsync(AuthenticationTokenCreateContext context);
        Task ReceiveAsync(AuthenticationTokenReceiveContext context);
    }
}
