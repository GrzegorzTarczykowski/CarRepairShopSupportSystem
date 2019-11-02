using CarRepairShopSupportSystem.WebAPI.BLL.IService;
using CarRepairShopSupportSystem.WebAPI.DAL.Abstraction;
using CarRepairShopSupportSystem.WebAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopSupportSystem.WebAPI.BLL.Service
{
    public class LoginService : ILoginService
    {
        private readonly IRepository<User> userRepository;
        private readonly ICryptographyService cryptographyService;

        public LoginService(IRepository<User> userRepository, ICryptographyService cryptographyService)
        {
            this.userRepository = userRepository;
            this.cryptographyService = cryptographyService;
        }

        public User Login(string username, string password)
        {
            try
            {
                User user = userRepository.FindBy(u => u.Username == username, nameof(User.Permission)).FirstOrDefault();

                if (user != null)
                {
                    byte[] hash = cryptographyService.GenerateSHA512(password, user.Salt);
                    string hashedPassword = Convert.ToBase64String(hash);

                    if (user.Password != hashedPassword)
                    {
                        user = null;
                    }
                    else
                    {
                        user.LastLogin = DateTime.Now;
                        userRepository.SaveChanges();
                    }
                }
                return user;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
