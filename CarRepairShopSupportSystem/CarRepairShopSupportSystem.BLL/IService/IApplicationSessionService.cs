using CarRepairShopSupportSystem.BLL.Models;

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
