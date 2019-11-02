using CarRepairShopSupportSystem.WebAPI.BLL.Enums;
using CarRepairShopSupportSystem.WebAPI.BLL.IService;
using CarRepairShopSupportSystem.WebAPI.DAL.Abstraction;
using CarRepairShopSupportSystem.WebAPI.DAL.Enums;
using CarRepairShopSupportSystem.WebAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopSupportSystem.WebAPI.BLL.Service
{
    public class RegisterService : IRegisterService
    {
        private readonly IRepository<User> userRepository;
        private readonly ICryptographyService cryptographyService;

        public RegisterService(IRepository<User> userRepository, ICryptographyService cryptographyService)
        {
            this.userRepository = userRepository;
            this.cryptographyService = cryptographyService;
        }

        public RegisterServiceResponse Register(User user)
        {
            try
            {
                if (userRepository.Any(u => u.Username == user.Username))
                {
                    return RegisterServiceResponse.DuplicateUsername;
                }
                else if (userRepository.Any(u => u.Email == user.Email))
                {
                    return RegisterServiceResponse.DuplicateEmail;
                }
                else
                {
                    byte[] salt = cryptographyService.GenerateRandomSalt();
                    byte[] hashedPassword = cryptographyService.GenerateSHA512(user.Password, salt);
                    user.Salt = salt;
                    user.Password = Convert.ToBase64String(hashedPassword);
                    user.CreateDate = DateTime.Now;
                    user.PermissionId = (int)PermissionId.User;
                    userRepository.Add(user);
                    userRepository.SaveChanges();
                    return RegisterServiceResponse.SuccessRegister;
                }
            }
            catch (Exception)
            {
                return RegisterServiceResponse.ErrorRegister;
            }
        }
    }
}
