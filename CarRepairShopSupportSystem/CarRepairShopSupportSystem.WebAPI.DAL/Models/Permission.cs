using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopSupportSystem.WebAPI.DAL.Models
{
    public class Permission
    {
        public int PermissiondId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<User> AssignedToUsers { get; set; }
    }
}
