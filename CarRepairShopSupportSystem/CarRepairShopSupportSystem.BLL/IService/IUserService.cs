using CarRepairShopSupportSystem.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopSupportSystem.BLL.IService
{
    public interface IUserService
    {
        OperationResult AddUser(User user);
        OperationResult EditUser(User user);
        IEnumerable<User> GetAllUserList();
        IEnumerable<User> GetAllWorkerList();
    }
}
