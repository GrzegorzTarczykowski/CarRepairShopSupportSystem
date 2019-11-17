﻿using CarRepairShopSupportSystem.WebAPI.Controllers.Abstraction;
using CarRepairShopSupportSystem.WebAPI.DAL.Abstraction;
using CarRepairShopSupportSystem.WebAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CarRepairShopSupportSystem.WebAPI.Controllers
{
    public class VehicleTypeController : ACRUDApiController<VehicleType>
    {
        public VehicleTypeController(IRepository<VehicleType> repository) : base(repository)
        {
        }


        [Authorize(Roles = "SuperAdmin, Admin, User")]
        [HttpGet]
        // GET api/<controller>
        public IEnumerable<Models.VehicleType> Get()
        {
            return GetBase()?.Select(vt => new Models.VehicleType { VehicleTypeId = vt.VehicleTypeId, Name = vt.Name });
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpGet]
        // GET api/<controller>/5
        public VehicleType Get(int id)
        {
            return GetBase(id);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]VehicleType value)
        {
            return PostBase(value);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPut]
        // PUT api/<controller>/5
        public HttpResponseMessage Put(int id, [FromBody]VehicleType value)
        {
            return PutBase(id == value.VehicleTypeId, value);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpDelete]
        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(int id)
        {
            return DeleteBase(id);
        }
    }
}