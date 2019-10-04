﻿using CarRepairShopSupportSystem.WebAPI.DAL.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopSupportSystem.WebAPI.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Dictionary<string, IRepository> repositories;

        public UnitOfWork()
        {
            repositories = new Dictionary<string, IRepository>();
        }

        public void Register(IRepository repository)
        {
            repositories.Add(repository.GetType().Name, repository);
        }

        public void SaveChanges()
        {
            repositories.ToList().ForEach(x => x.Value.SaveChanges());
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    repositories.ToList().ForEach(x => x.Value.Dispose());
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
