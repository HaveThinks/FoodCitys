using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodCityBackstage.Models;
using System.Dynamic;
using System.IO;

namespace FoodCityBackstage.Controllers
{
    [Models.MyFilter]
    public class MenuController : Controller
    {
        FoodDBEntities5 dc = new FoodDBEntities5();
        // GET: Menu
        /// <summary>
        /// 菜分类列表和搜索分类
        /// </summary>
        /// <param name="Content"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public ActionResult Menuclass(string Content,int? s)
        {
            if (s==1)
            {
                var list = dc.Category.Where(C=>C.CategoryName.Contains(Content));
                return View(list);
            }
            else
            {
                var list = dc.Category;
                return View(list);
            }
           
        }
        public ActionResult Evaluation(string Content, int? s)
        {
            var list = from a in dc.Menu
                       join b in dc.Evaluation on a.MenuID equals b.MenuID
                       join c in dc.User on b.UserID equals c.UserID
                       select new
                       {
                           a.MenuName,
                           c.UserName,
                           b.EvaluationID,
                           b.CreateTime,
                           b.Star,
                           b.Content
                       };
            if (s == 1)
            {
                var menus = list.Where(C => C.MenuName.Contains(Content));
                List<dynamic> oneList = new List<dynamic>();
                foreach (var item in menus.ToList())
                {
                    dynamic dyObj = new ExpandoObject();
                    dyObj.MenuName = item.MenuName;
                    dyObj.Content = item.Content;
                    dyObj.UserName = item.UserName;
                    dyObj.Star = item.Star;
                    dyObj.EvaluationID = item.EvaluationID;
                    dyObj.CreateTime = item.CreateTime;
                    oneList.Add(dyObj);
                }
                ViewBag.data = oneList;
                return View();
            }
            else
            {
                var menus = list;
                List<dynamic> oneList = new List<dynamic>();
                foreach (var item in menus.ToList())
                {
                    dynamic dyObj = new ExpandoObject();
                    dyObj.MenuName = item.MenuName;
                    dyObj.Content = item.Content;
                    dyObj.UserName = item.UserName;
                    dyObj.Star = item.Star;
                    dyObj.EvaluationID = item.EvaluationID;
                    dyObj.CreateTime = item.CreateTime;
                    oneList.Add(dyObj);
                }
                ViewBag.data = oneList;
                return View();
            }
        }
        public ActionResult DelEva(int id)
        {
            
            var ev = dc.Evaluation.Where(c => c.EvaluationID == id).FirstOrDefault();
            dc.Evaluation.Remove(ev);
            dc.SaveChanges();
            return Content("删除成功！");
        }
        public ActionResult Uedit()
        {
            return View();
        }
        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult Deleteclass(int ID)
        {
            var lisr = dc.Category.Where(c => c.CategoryID == ID).FirstOrDefault();
            dc.Category.Remove(lisr);
            dc.SaveChanges();
            return Content("删除成功!");

        }
        /// <summary>
        /// 添加分类
        /// </summary>
        /// <param name="cc"></param>
        /// <returns></returns>
        public ActionResult AddMenuclass(Category cc)
        {
            dc.Category.Add(cc);
            dc.SaveChanges();
            return Content("添加成功！");
        }
        /// <summary>
        /// 修改分类
        /// </summary>
        /// <param name="cc"></param>
        /// <returns></returns>
        public ActionResult EdMenuclass(Category cc)
        {
            var list = dc.Category.Where(c => c.CategoryID == cc.CategoryID).FirstOrDefault();
            list.CategoryName=cc.CategoryName;
            dc.SaveChanges();
            return Content("修改成功！");
        }
        /// <summary>
        /// 菜分类模态框内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Form(int? id)
        {
            if (id==null)
            {
                ViewBag.edit = 0;
                return View();
            }
            else
            {
                ViewBag.edit = 1;
                var list = dc.Category.Where(c => c.CategoryID == id);
                ViewBag.cate = list;
                return View();
            }
        }



        //存储图片
        public ActionResult AddPric()
        {
            
            if (Request.Files["FileUpload1"] != null)
            {

                string savepath = Server.MapPath("~/Content/Backstage/assets/images/Menu");
                string ext = Path.GetExtension(Request.Files["FileUpload1"].FileName);
                string fileName = "";
                switch (ext)
                {
                    case ".jpg":
                    case ".jpeg":
                    case ".png":
                    case ".gif":
                    case ".bmp":
                    case ".rar":
                    case ".zip":
                        if (Request.Files["FileUpload1"].ContentLength < 1024000)
                        {
                            fileName = Guid.NewGuid().ToString().Substring(0, 8) + ext;
                            Request.Files["FileUpload1"].SaveAs(savepath + "\\" + fileName);
                        }
                        break;
                }
                return Content(fileName);
            }
            else
            {
                return Content("");
            }
        }
        public ActionResult MenuIndex(string Content, int? s)
        {

            var list = from a in dc.Menu
                        join b in dc.Category on a.CategoryId equals b.CategoryID
                        select new
                        {
                            a.MenuID,
                            a.Price,
                            a.Sales,
                            a.Shipping,
                            a.Store,
                            a.image,
                            a.Details,
                            a.MenuName,
                            b.CategoryName,
                        };
            
            if (s == 1)
            {
                var menus = list.Where(C => C.MenuName.Contains(Content));
                List<dynamic> oneList = new List<dynamic>();
                foreach (var item in menus.ToList())
                {
                    dynamic dyObj = new ExpandoObject();//需要引用命名空间 using System.Dynamic;
                    dyObj.MenuName = item.MenuName;
                    dyObj.CategoryName = item.CategoryName;
                    dyObj.MenuID = item.MenuID;
                    dyObj.Price = item.Price;
                    dyObj.Store = item.Store;
                    dyObj.Sales = item.Sales;
                    dyObj.Details = item.Details;
                    dyObj.image = item.image;
                    dyObj.Shipping = item.Shipping;
                    oneList.Add(dyObj);
                }
                ViewBag.data = oneList;
                return View();

            }
            else
            {
                var menus = list;
                List<dynamic> oneList = new List<dynamic>();
                foreach (var item in menus.ToList())
                {
                    dynamic dyObj = new ExpandoObject();
                    dyObj.MenuName = item.MenuName;
                    dyObj.CategoryName = item.CategoryName;
                    dyObj.MenuID = item.MenuID;
                    dyObj.Price = item.Price;
                    dyObj.Store = item.Store;
                    dyObj.Sales = item.Sales;
                    dyObj.Details = item.Details;
                    dyObj.image = item.image;
                    dyObj.Shipping = item.Shipping;
                    oneList.Add(dyObj);
                }
                ViewBag.data = oneList;
                return View();
            }
            
        }

        public ActionResult Menuform(int? id)
        {
            if (id == null)
            {
                ViewBag.cate = dc.Category;
                ViewBag.edit = 0;
                return View();
            }
            else
            {
                ViewBag.edit = 1;
                var list = dc.Menu.Where(c => c.MenuID == id);
                ViewBag.cate = dc.Category;
                ViewBag.menu = list;
                return View();
            }
        }

        public ActionResult Addmenu(Menu menu)
        {

            dc.Menu.Add(menu);
            dc.SaveChanges();
            return Content("添加成功！");
        }
        public ActionResult Eddmenu(Menu menu)
        {

           var list=dc.Menu.Where(c=>c.MenuID==menu.MenuID).FirstOrDefault();
            list.MenuName = menu.MenuName;
            list.Price = menu.Price;
            list.Shipping = menu.Shipping;
            list.Store = menu.Store;
            list.image = menu.image;
            list.Details = menu.Details;
            list.CategoryId = menu.CategoryId;
            dc.SaveChanges();
           return Content("添加成功！");
        }
        public ActionResult DeleteMenu(int ID)
        {
            var lisr = dc.Menu.Where(c => c.MenuID == ID).FirstOrDefault();
            dc.Menu.Remove(lisr);
            dc.SaveChanges();
            return Content("删除成功!");

        }
    }
}