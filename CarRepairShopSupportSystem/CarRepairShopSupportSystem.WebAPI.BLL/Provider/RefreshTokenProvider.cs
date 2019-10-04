using CarRepairShopSupportSystem.WebAPI.BLL.IService;
using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopSupportSystem.WebAPI.BLL.Provider
{
    public class RefreshTokenProvider : IAuthenticationTokenProvider
    {
        private readonly IRefreshTokenService refreshTokenService;

        public RefreshTokenProvider(IRefreshTokenService refreshTokenService)
        {
            this.refreshTokenService = refreshTokenService;
        }

        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            await refreshTokenService.CreateAsync(context);
        }

        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            await refreshTokenService.ReceiveAsync(context);
        }

        public void Create(AuthenticationTokenCreateContext context)
        {
            throw new NotImplementedException();
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
        }
    }
}
