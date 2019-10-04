using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopSupportSystem.WebAPI.DAL.Abstraction
{
    public interface IRepository : IDisposable
    {
        bool SaveChanges();
    }
}
