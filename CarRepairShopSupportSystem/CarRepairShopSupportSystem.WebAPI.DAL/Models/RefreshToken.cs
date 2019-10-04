using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopSupportSystem.WebAPI.DAL.Models
{
    public class RefreshToken
    {
        public string RefreshTokenId { get; set; }
        public string UserName { get; set; }
        public string ClientCode { get; set; }
        public DateTime IssuedTime { get; set; }
        public DateTime ExpiredTime { get; set; }
        public string ProtectedTicket { get; set; }
    }
}
