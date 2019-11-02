using CarRepairShopSupportSystem.BLL.IService;
using CarRepairShopSupportSystem.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopSupportSystem.BLL.Service
{
    public class AccessTokenService : IAccessTokenService
    {
        private readonly IApplicationSessionService applicationSessionService;
        private readonly ITokenService tokenService;

        public AccessTokenService(IApplicationSessionService applicationSessionService, ITokenService tokenService)
        {
            this.applicationSessionService = applicationSessionService;
            this.tokenService = tokenService;
        }

        public string GetAccessToken()
        {
            try
            {
                Token token = applicationSessionService.GetTokenFromApplicationSession();
                if (string.IsNullOrWhiteSpace(token.Error))
                {
                    if (token.ExpiredDateTime > DateTime.Now)
                    {
                        return token.AccessToken;
                    }
                    else
                    {
                        token = tokenService.GetTokenByRefreshToken(ApplicationSession.clientId
                                                                    , ApplicationSession.clientSecret
                                                                    , token.RefreshToken);
                    }
                }

                if (!string.IsNullOrWhiteSpace(token.Error))
                {
                    token = tokenService.GetToken(ApplicationSession.clientId
                                                  , ApplicationSession.clientSecret
                                                  , ApplicationSession.userName
                                                  , ApplicationSession.userPassword);

                    if (!string.IsNullOrWhiteSpace(token.Error))
                    {
                        throw new Exception(token.Error);
                    }
                }

                token.ExpiredDateTime = DateTime.Now.AddSeconds(token.ExpiresIn);
                applicationSessionService.AddTokenIntoApplicationSession(token);
                return token.AccessToken;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
