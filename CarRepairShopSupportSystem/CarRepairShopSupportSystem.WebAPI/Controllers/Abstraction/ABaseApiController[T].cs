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

        [HttpGet]
        internal protected IEnumerable<T> GetBase()
        {
            try
            {
                return repository.GetAll();
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpGet]
        internal protected IEnumerable<T> GetByBase(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return repository.FindBy(predicate);
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpGet]
        internal protected T GetBase(int id)
        {
            try
            {
                return repository.FindById(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpGet]
        internal protected HttpResponseMessage GetIsAny(Expression<Func<T, bool>> predicate)
        {
            try
            {
                if (repository.Any(predicate))
                {
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                }
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        internal protected HttpResponseMessage PostBase(T value)
        {
            try
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
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut]
        internal protected HttpResponseMessage PutBase(bool isSameEntity, T value)
        {
            try
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
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        internal protected HttpResponseMessage DeleteBase(int id)
        {
            try
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
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
    }
}