using CarRepairShopSupportSystem.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopSupportSystem.BLL.IService
{
    public interface IApplicationSessionService
    {
        void AddTokenIntoApplicationSession(Token token);
        Token GetTokenFromApplicationSession();
        void AddUserIntoApplicationSession(User user);
        User GetUserFromApplicationSession();
        void ClearApplicationSession();
    }
}
