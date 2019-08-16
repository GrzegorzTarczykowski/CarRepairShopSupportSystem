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
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            CarBrand carBrand = new CarBrand();
            MsSqlServerContext db = new MsSqlServerContext();
            db.CarBrands.FirstOrDefault();
            return View();
        }
    }
}
