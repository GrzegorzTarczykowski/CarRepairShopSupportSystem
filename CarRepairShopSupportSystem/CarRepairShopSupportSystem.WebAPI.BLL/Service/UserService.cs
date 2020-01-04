using CarRepairShopSupportSystem.WebAPI.BLL.IService;
using CarRepairShopSupportSystem.WebAPI.DAL.Abstraction;
using CarRepairShopSupportSystem.WebAPI.DAL.Enums;
using CarRepairShopSupportSystem.WebAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopSupportSystem.WebAPI.BLL.Service
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> userRepository;

        public UserService(IRepository<User> userRepository)
        {
            this.userRepository = userRepository;
        }

        public IEnumerable<User> GetAllWorkerList()
        {
            return userRepository.FindBy(u => u.PermissionId == (int)PermissionId.Admin);
        }
    }
}
