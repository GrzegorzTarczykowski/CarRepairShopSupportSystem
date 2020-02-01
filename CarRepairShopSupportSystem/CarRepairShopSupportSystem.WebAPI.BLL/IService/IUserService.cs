using CarRepairShopSupportSystem.WebAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopSupportSystem.WebAPI.BLL.IService
{
    public interface IUserService
    {
        bool ChangeUserDetails(int userId, User user);
        IEnumerable<User> GetAllWorkerList();
    }
}
