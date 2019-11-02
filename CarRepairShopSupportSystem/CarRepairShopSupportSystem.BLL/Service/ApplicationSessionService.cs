using CarRepairShopSupportSystem.BLL.IService;
using CarRepairShopSupportSystem.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopSupportSystem.BLL.Service
{
    public class ApplicationSessionService : IApplicationSessionService
    {
        public void AddTokenIntoApplicationSession(Token token)
        {
            ApplicationSession.userToken = new UserToken()
            {
                AccessToken = token.AccessToken,
                RefreshToken = token.RefreshToken,
                ExpiredDateTime = token.ExpiredDateTime
            };
        }

        public void AddUserIntoApplicationSession(User user)
        {
            ApplicationSession.currentUser = user;
        }

        public void ClearApplicationSession()
        {
            ApplicationSession.currentUser = null;
            ApplicationSession.userToken = null;
            ApplicationSession.userName = string.Empty;
            ApplicationSession.userPassword = string.Empty;
        }

        public Token GetTokenFromApplicationSession()
        {
            UserToken userToken = ApplicationSession.userToken;
            Token token = null;

            if (userToken != null)
            {
                token = new Token()
                {
                    AccessToken = userToken.AccessToken,
                    RefreshToken = userToken.RefreshToken,
                    ExpiredDateTime = userToken.ExpiredDateTime
                };
            }
            else
            {
                token = new Token()
                {
                    Error = "No token in the session"
                };
            }
            return token;
        }

        public User GetUserFromApplicationSession()
        {
            return ApplicationSession.currentUser;
        }
    }
}
