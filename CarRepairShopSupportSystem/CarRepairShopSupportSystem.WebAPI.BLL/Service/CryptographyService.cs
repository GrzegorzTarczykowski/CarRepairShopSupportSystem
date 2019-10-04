using CarRepairShopSupportSystem.WebAPI.BLL.IService;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CarRepairShopSupportSystem.WebAPI.BLL.Service
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
            using (HashAlgorithm hashAlgorithm = new SHA512CryptoServiceProvider())
            {
                byte[] password = Encoding.UTF8.GetBytes(explicitPassword);
                byte[] saltedPassword = password.Concat(salt).ToArray();
                byte[] implicitPassword = hashAlgorithm.ComputeHash(saltedPassword);
                return implicitPassword;
            }
        }
    }
}
