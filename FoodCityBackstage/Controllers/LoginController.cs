using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodCityBackstage.Models;

namespace FoodCityBackstage.Controllers
{
    
    public class LoginController : Controller
    {
        FoodDBEntities5 dc = new FoodDBEntities5();
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username,string pwd)
        {
             Admin list =(from a in dc.Admin where a.AdName == username && a.AdPwd == pwd select a).FirstOrDefault();
            if (list!=null)
            {
                Session["power"] = list.Power;
                Session["adid"] = list.AdID;
                Session["name"] = list.AdName;
                return RedirectToAction("Index","Backstage");
            }
            else
            {
                ViewBag.yanzheng = "用户名或者密码错误！";
                return View();
            }

           
        }
        public ActionResult Layout()
        {

            return RedirectToAction("Login", "Login");
        }

    }
}