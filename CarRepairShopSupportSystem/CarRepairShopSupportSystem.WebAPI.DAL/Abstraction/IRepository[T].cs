using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopSupportSystem.WebAPI.DAL.Abstraction
{
    public interface IRepository<T> : IRepository where T : class
    {
        T FindById(int id);
        T FindFirstBy(Expression<Func<T, bool>> predicate);
        Task<T> FindFirstByAsync(Expression<Func<T, bool>> predicate);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, params string[] includeProperties);
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(params string[] includeProperties);
        bool Any(Expression<Func<T, bool>> predicate);
        bool Add(T entity);
        bool AddRange(IEnumerable<T> entities);
        bool Edit(T entity);
        void EditManyToMany<TMany>(Expression<Func<T, bool>> filter, string collectionPropertyName, string idPropertyName, IEnumerable<object> updatedSet);
        void Remove(int id);
        void Remove(params object[] keyValues);
        void RemoveManyToMany<TMany>(Expression<Func<T, bool>> filter, string collectionPropertyName, string idPropertyName, IEnumerable<object> removeSet);
        bool SaveChanges(T entity);
        Task<bool> SaveChangesAsync();
    }
}
