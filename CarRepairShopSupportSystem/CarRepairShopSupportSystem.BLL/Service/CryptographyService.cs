using CarRepairShopSupportSystem.BLL.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopSupportSystem.BLL.Service
{
    public class CryptographyService : ICryptographyService
    {
        public byte[] GenerateRandomSalt()
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] salt = new byte[32];
                rng.GetBytes(salt);
                return salt;
            }
        }

        public byte[] GenerateSHA512(string input)
        {
            using (HashAlgorithm hashAlgorithm = new SHA512CryptoServiceProvider())
            {
                byte[] byteValue = Encoding.UTF8.GetBytes(input);
                return hashAlgorithm.ComputeHash(byteValue);
            }
        }

        public byte[] GenerateSHA512(string explicitPassword, byte[] salt)
        {
            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] password = Encoding.UTF8.GetBytes(explicitPassword);
                byte[] saltedPassword = password.Concat(salt).ToArray();
                byte[] implicitPassword = sha512.ComputeHash(saltedPassword);
                return implicitPassword;
            }
        }
    }
}
