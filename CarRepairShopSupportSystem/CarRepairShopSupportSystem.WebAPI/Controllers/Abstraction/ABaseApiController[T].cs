using CarRepairShopSupportSystem.WebAPI.DAL.Abstraction;
using CarRepairShopSupportSystem.WebAPI.DAL.Models;
using CarRepairShopSupportSystem.WebAPI.DAL.MsSqlServerDB;
using CarRepairShopSupportSystem.WebAPI.DAL.Repository;
using CarRepairShopSupportSystem.WebAPI.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace CarRepairShopSupportSystem.WebAPI.Controllers.Abstraction
{
    public abstract class ABaseApiController<T> : ApiController where T : class
    {
        private readonly IRepository<T> repository;

        internal protected ABaseApiController(IRepository<T> repository)
        {
            this.repository = repository;
        }

        internal protected IEnumerable<T> GetBase()
        {
            return repository.GetAll();
        }

        internal protected IEnumerable<T> GetByBase(Expression<Func<T, bool>> predicate)
        {
            return repository.FindBy(predicate);
        }

        internal protected T GetBase(int id)
        {
            return repository.FindById(id);
        }

        internal protected HttpResponseMessage PostBase(T value)
        {
            if (ModelState.IsValid)
            {
                repository.Add(value);
                repository.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        internal protected HttpResponseMessage PutBase(bool isSameEntity, T value)
        {
            if (ModelState.IsValid && isSameEntity)
            {
                repository.Edit(value);
                repository.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        internal protected HttpResponseMessage DeleteBase(int id)
        {
            T entity = repository.FindById(id);
            if (entity != null)
            {
                repository.Remove(entity);
                repository.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
            }
        }
    }
}