using CarRepairShopSupportSystem.WebAPI.DAL.Models;
using CarRepairShopSupportSystem.WebAPI.DAL.MsSqlServerDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarRepairShopSupportSystem.WebAPI.Controllers
{
    public class HomeController : Controller
    {
        [Authorize(Roles = "SuperAdmin, Admin, User")]
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            VehicleType vehicleType = new VehicleType();
            using (MsSqlServerContext db = new MsSqlServerContext())
            {
                db.VehicleTypes.FirstOrDefault();
            }
            return View();
        }
    }
}
