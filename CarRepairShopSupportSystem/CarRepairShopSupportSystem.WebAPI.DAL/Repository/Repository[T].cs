using CarRepairShopSupportSystem.WebAPI.DAL.Abstraction;
using CarRepairShopSupportSystem.WebAPI.DAL.MsSqlServerDB;
using CarRepairShopSupportSystem.WebAPI.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopSupportSystem.WebAPI.DAL.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly MsSqlServerContext msSqlServerContext;
        private readonly DbSet<T> set;

        public Repository(MsSqlServerContext msSqlServerContext, IUnitOfWork unitOfWork)
        {
            this.msSqlServerContext = msSqlServerContext;
            set = msSqlServerContext.Set<T>();
            unitOfWork.Register(this);
        }

        public bool Add(T entity)
        {
            set.Add(entity);
            return true;
        }

        public bool AddRange(IEnumerable<T> entities)
        {
            set.AddRange(entities);
            return true;
        }

        public bool Any(Expression<Func<T, bool>> predicate)
        {
            return set.Any(predicate);
        }

        public bool Edit(T entity)
        {
            msSqlServerContext.Entry(entity).State = EntityState.Modified;
            return true;
        }

        public void EditManyToMany<TMany>(Expression<Func<T, bool>> filter
                                                , string collectionPropertyName
                                                , string idPropertyName
                                                , IEnumerable<object> updatedSet)
        {
            var previous = msSqlServerContext
                .Set<T>()
                .Include(collectionPropertyName)
                .FirstOrDefault(filter);

            IList<TMany> values = new List<TMany>();

            foreach (var entry in updatedSet
                                .Select(obj => (int)obj
                                    .GetType()
                                    .GetProperty(idPropertyName)
                                    .GetValue(obj, null))
                                .Select(value => (TMany)msSqlServerContext.Set(typeof(TMany)).Find(value)))
            {
                values.Add(entry);
            }

            msSqlServerContext.Entry(previous).Collection(collectionPropertyName).CurrentValue = values;
            set.Attach(previous);
        }

        public T FindById(int id)
        {
            return set.Find(id);
        }

        public T FindFirstBy(Expression<Func<T, bool>> predicate)
        {
            return set.Where(predicate).FirstOrDefault();
        }

        public async Task<T> FindFirstByAsync(Expression<Func<T, bool>> predicate)
        {
            return await set.Where(predicate).FirstOrDefaultAsync();
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return set.Where(predicate);
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, params string[] includeProperties)
        {
            return includeProperties.Aggregate(set.Where(predicate).AsQueryable(), (query, path) => query.Include(path));
        }

        public IQueryable<T> GetAll()
        {
            return set;
        }

        public IQueryable<T> GetAll(params string[] includeProperties)
        {
            return includeProperties.Aggregate(set.AsQueryable(), (query, path) => query.Include(path));
        }

        public void Remove(params object[] keyValues)
        {
            T entityToDelete = set.Find(keyValues);
            if (msSqlServerContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                set.Attach(entityToDelete);
            }
            set.Remove(entityToDelete);
        }

        public void Remove(int id)
        {
            T entityToDelete = set.Find(id);
            if (msSqlServerContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                set.Attach(entityToDelete);
            }
            set.Remove(entityToDelete);
        }

        public bool SaveChanges()
        {
            return msSqlServerContext.SaveChanges() > 0;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await msSqlServerContext.SaveChangesAsync() > 0;
        }

        public bool SaveChanges(T entity)
        {
            if (msSqlServerContext.Entry(entity).State == EntityState.Detached)
            {
                set.Attach(entity);
            }
            msSqlServerContext.Entry(entity).State = EntityState.Modified;
            return msSqlServerContext.SaveChanges() > 0;
        }

        public void Dispose()
        {
            msSqlServerContext.Dispose();
        }
    }
}
