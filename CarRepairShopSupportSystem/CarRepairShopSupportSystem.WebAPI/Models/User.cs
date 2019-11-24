using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRepairShopSupportSystem.WebAPI.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public int PermissionId { get; set; }
        public string PermissionName { get; set; }
    }
}