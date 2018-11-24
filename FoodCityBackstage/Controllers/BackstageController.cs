using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodCityBackstage.Models;

namespace FoodCityBackstage.Controllers
{
    public class BackstageController : Controller
    {
        FoodDBEntities5 dc = new FoodDBEntities5();
        // GET: Backstage
        public ActionResult Index()
        {
            
            return View();
        }     
        public PartialViewResult Header()
        {
            return PartialView();
        }
        public PartialViewResult Nav()
        {
            int id;
            if (Session["adid"]!=null)
            {
                 id = (int)Session["adid"];
            }
            else
            {
                 id = 1005;
            }
           
            var admin= dc.Admin.Where(c => c.AdID == id).FirstOrDefault();
            ViewBag.adnameme = admin.AdName;
            ViewBag.adimgme = admin.ImageUrl;
            return PartialView();
        }
    }
}