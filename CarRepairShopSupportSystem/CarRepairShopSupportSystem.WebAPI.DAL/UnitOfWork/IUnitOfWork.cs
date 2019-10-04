using CarRepairShopSupportSystem.WebAPI.DAL.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopSupportSystem.WebAPI.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        void Register(IRepository repository);
        void SaveChanges();
    }
}
