using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopSupportSystem.WebAPI.DAL.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public byte[] Salt { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? LastLogin { get; set; }

        public int PermissionId { get; set; }
        public Permission Permission { get; set; }

        public virtual ICollection<Vehicle> OwnedVehicles { get; set; }
        public virtual ICollection<Message> SentMessages { get; set; }
        public virtual ICollection<Message> ReceivedMessages { get; set; }
        public virtual ICollection<Order> WorksOnOrders { get; set; }
        public virtual ICollection<Timetable> UserWorkTimetables { get; set; }
    }
}
