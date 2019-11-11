using CarRepairShopSupportSystem.WebAPI.DAL.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CarRepairShopSupportSystem.WebAPI.Controllers.Abstraction
{
    public abstract class ACRUDApiController<T> : ABaseApiController where T : class
    {
        private readonly IRepository<T> repository;

        internal protected ACRUDApiController(IRepository<T> repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        internal protected IEnumerable<T> GetBase()
        {
            return BaseAction(repository.GetAll);
        }

        [HttpGet]
        internal protected IEnumerable<T> GetByBase(Expression<Func<T, bool>> predicate)
        {
            return BaseAction(delegate 
            { 
                return repository.FindBy(predicate); 
            });
        }

        [HttpGet]
        internal protected T GetBase(int id)
        {
            return BaseAction(delegate
            {
                return repository.FindById(id);
            });
        }

        [HttpGet]
        internal protected HttpResponseMessage GetIsAny(Expression<Func<T, bool>> predicate)
        {
            return BaseAction(delegate
            {
                if (repository.Any(predicate))
                {
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                }
            });
        }

        [HttpPost]
        internal protected HttpResponseMessage PostBase(T value)
        {
            return BaseAction(delegate
            {
                repository.Add(value);
                repository.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.OK);
            });
        }

        [HttpPost]
        internal protected HttpResponseMessage PostBase(Func<HttpResponseMessage> func)
        {
            return BaseAction(func);
        }

        [HttpPut]
        internal protected HttpResponseMessage PutBase(bool isSameEntity, T value)
        {
            return BaseAction(delegate
            {
                if (isSameEntity)
                {
                    repository.Edit(value);
                    repository.SaveChanges();
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
            });
        }

        [HttpPut]
        internal protected HttpResponseMessage PutBase(Func<HttpResponseMessage> func)
        {
            return BaseAction(func);
        }

        [HttpDelete]
        internal protected HttpResponseMessage DeleteBase(int id)
        {
            return BaseAction(delegate
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
            });
        }
    }
}