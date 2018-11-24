using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodCityBackstage.Models;

namespace FoodCityBackstage.Controllers
{
    [Models.MyFilter]
    public class UserController : Controller
    {
        FoodDBEntities5 dc = new FoodDBEntities5();
        // GET: User
        /// <summary>
        /// 用户表
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public ActionResult Index(string UserName, int? s)
        {
            if (s==1)
            {
                var list = dc.User.Where(c => c.UserName.Contains(UserName));
                return View(list);
            }
            else
            {
                var list = dc.User;
                return View(list);
            }
            
        }
        /// <summary>
        /// 留言表
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public ActionResult Message(string UserName, int? s)
        {
            if (s == 1)
            {
                var list = dc.Message.Where(c => c.Content.Contains(UserName));
                return View(list);
            }
            else
            {
                return View(dc.Message);
            }
               
        }
        public ActionResult ReadMessage(int id)
        {
            Message list = dc.Message.Where(c => c.MessageID == id).FirstOrDefault();
            list.State = "已读";
            dc.SaveChanges();
            return Content(list.Content);
        }
        public ActionResult Messages()
        {
            var list = from a in dc.Message
                       join m in dc.User on a.UserID equals m.UserID
                       select new
                       {
                           m.UserName,
                           a.SendTime,
                           a.Content,
                           a.State,
                           a.MessageID
                       };
           
            return Json(list);
        }
        public ActionResult Fhao(int id)
        {
            User list = dc.User.Where(c => c.UserID == id).FirstOrDefault();
            list.State = "封停";
            dc.SaveChanges();
            return Content("封号成功！");
        }
        public ActionResult Jfeng(int id)
        {
            User list = dc.User.Where(c => c.UserID == id).FirstOrDefault();
            list.State = "正常";
            dc.SaveChanges();
            return Content("解除成功！");
        }
    }
}