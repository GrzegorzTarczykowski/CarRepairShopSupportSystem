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
        private readonly ICryptographyService cryptographyService;

        public UserService(IRepository<User> userRepository, ICryptographyService cryptographyService)
        {
            this.userRepository = userRepository;
            this.cryptographyService = cryptographyService;
        }

        public bool ChangeUserDetails(int userId, User user)
        {
            User matchingUser = userRepository.FindBy(u => u.UserId == userId).FirstOrDefault();

            if (matchingUser == null)
            {
                return false;
            }
            else
            {
                matchingUser.Email = user.Email;
                matchingUser.PhoneNumber = user.PhoneNumber;
                byte[] hashedPassword = cryptographyService.GenerateSHA512(user.Password, matchingUser.Salt);
                matchingUser.Password = Convert.ToBase64String(hashedPassword);
                userRepository.Edit(matchingUser);
                userRepository.SaveChanges();
                return true;
            }
        }

        public IEnumerable<User> GetAllWorkerList()
        {
            return userRepository.FindBy(u => u.PermissionId == (int)PermissionId.Admin);
        }
    }
}
