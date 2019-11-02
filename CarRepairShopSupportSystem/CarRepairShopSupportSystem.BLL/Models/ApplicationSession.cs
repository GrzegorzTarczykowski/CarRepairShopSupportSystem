using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopSupportSystem.BLL.Models
{
    public class ApplicationSession
    {
        public readonly static string clientId = "DOTNET";
        public readonly static string clientSecret = "EEF47D9A-DBA9-4D02-B7B0-04F4279A6D20";
        public static UserToken userToken;
        public static User currentUser;
        public static string userName;
        public static string userPassword;
    }
}
