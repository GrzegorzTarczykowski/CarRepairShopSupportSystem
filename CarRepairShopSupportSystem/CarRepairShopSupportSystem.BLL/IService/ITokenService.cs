using CarRepairShopSupportSystem.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopSupportSystem.BLL.IService
{
    public interface ITokenService
    {
        Token GetToken(string clientId, string clientSecret, string username, string password);
        Token GetTokenByRefreshToken(string clientId, string clientSecret, string refreshToken);
    }
}
