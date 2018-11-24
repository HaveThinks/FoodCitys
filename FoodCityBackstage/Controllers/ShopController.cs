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
    public class ShopController : Controller
    {
        FoodDBEntities5 dc = new FoodDBEntities5();
        // GET: Shop
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Shopcar(string Content, int? s)
        {
            var list = from a in dc.ShopCar
                       join b in dc.Menu on a.MenuID equals b.MenuID
                       join c in dc.User on a.UserID equals c.UserID
                       select new
                       {
                           a.ShopCarID,
                           a.Quantity,
                           b.MenuName,
                           c.UserName,
                           price = a.Quantity * b.Price,
                           a.CreateTime
                       };
            if (s == 1)
            {
                var menus = list.Where(C => C.UserName.Contains(Content));
                List<dynamic> oneList = new List<dynamic>();
                foreach (var item in menus.ToList())
                {
                    dynamic dyObj = new ExpandoObject();//需要引用命名空间 using System.Dynamic;
                    dyObj.MenuName = item.MenuName;
                    dyObj.Quantity = item.Quantity;
                    dyObj.UserName = item.UserName;
                    dyObj.price = item.price;
                    dyObj.CreateTime = item.CreateTime;
                    dyObj.ShopCarID = item.ShopCarID;
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
                    dynamic dyObj = new ExpandoObject();//需要引用命名空间 using System.Dynamic;
                    dyObj.MenuName = item.MenuName;
                    dyObj.UserName = item.UserName;
                    dyObj.Quantity = item.Quantity;
                    dyObj.price = item.price;
                    dyObj.CreateTime = item.CreateTime;
                    dyObj.ShopCarID = item.ShopCarID;
                    oneList.Add(dyObj);
                }
                ViewBag.data = oneList;

                return View();
            }         
        }
        public ActionResult Adress(string Content, int? s)
        {
            var list = from a in dc.ShipAddress
                       join b in dc.User on a.UserID equals b.UserID
                       select new
                       {
                           b.UserName,
                           a.Address,
                           a.Consignee,
                           a.CreatTime,
                           a.Ctel,
                           a.Default,
                           a.IsValid,
                          
                       };

            if (s == 1)
            {
                var menus = list.Where(C => C.UserName.Contains(Content));
                List<dynamic> oneList = new List<dynamic>();
                foreach (var item in menus.ToList())
                {
                    dynamic dyObj = new ExpandoObject();//需要引用命名空间 using System.Dynamic;
                    dyObj.UserName = item.UserName;
                    dyObj.Address = item.Address;
                    dyObj.Consignee = item.Consignee;
                    dyObj.CreatTime = item.CreatTime;
                    dyObj.Ctel = item.Ctel;
                    dyObj.Default = item.Default;
                    dyObj.IsValid = item.IsValid;
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
                    dynamic dyObj = new ExpandoObject();//需要引用命名空间 using System.Dynamic;
                    dyObj.UserName = item.UserName;
                    dyObj.Address = item.Address;
                    dyObj.Consignee = item.Consignee;
                    dyObj.CreatTime = item.CreatTime;
                    dyObj.Ctel = item.Ctel;
                    dyObj.Default = item.Default;
                    dyObj.IsValid = item.IsValid;
                    oneList.Add(dyObj);
                }
                ViewBag.data = oneList;

                return View();
            }
        }
        public ActionResult Order(string Content, int? s)
        {
            var list = from a in dc.Order
                       join b in dc.User on a.UserID equals b.UserID
                      
                       select new
                       {
                           a.OrderID,
                           b.UserName,
                           a.AddressInfo,
                           a.ExpressNumber,
                           a.OrderMoney,
                           a.PostMoney,
                           a.CreateTime,
                           a.Isvaild,
                           a.OrderState,

                       };
            if (s == 1)
            {
                var menus = list.Where(C => C.UserName.Contains(Content));
                List<dynamic> oneList = new List<dynamic>();
                foreach (var item in menus.ToList())
                {
                    dynamic dyObj = new ExpandoObject();//需要引用命名空间 using System.Dynamic;
                    dyObj.UserName = item.UserName;
                    dyObj.OrderID = item.OrderID;
                    dyObj.AddressInfo = item.AddressInfo;
                    dyObj.ExpressNumber = item.ExpressNumber;
                    dyObj.OrderMoney = item.OrderMoney;
                    dyObj.PostMoney = item.PostMoney;
                    dyObj.CreateTime = item.CreateTime;
                    dyObj.Isvaild = item.Isvaild;
                    dyObj.OrderState = item.OrderState;
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
                    dynamic dyObj = new ExpandoObject();//需要引用命名空间 using System.Dynamic;
                    dyObj.UserName = item.UserName;
                    dyObj.OrderID = item.OrderID;
                    dyObj.AddressInfo = item.AddressInfo;
                    dyObj.ExpressNumber = item.ExpressNumber;
                    dyObj.OrderMoney = item.OrderMoney;
                    dyObj.PostMoney = item.PostMoney;
                    dyObj.CreateTime = item.CreateTime;
                    dyObj.Isvaild = item.Isvaild;
                    dyObj.OrderState = item.OrderState;
                    oneList.Add(dyObj);
                }
                ViewBag.data = oneList;

                return View();
            }
        }
        public ActionResult OrderDetails(int id=1)
        {
            var order = dc.Order.Where(c => c.OrderID == id).FirstOrDefault();
            ViewBag.OrderState = order.OrderState;
            ViewBag.ExpressNumber = order.ExpressNumber;
            ViewBag.Count = dc.OrderDetail.Where(c => c.OrderID == id).Count();
            ViewBag.money = order.OrderMoney;
            ViewBag.note = order.Note;
            var list = from a in dc.Order
                       join b in dc.OrderDetail on a.OrderID equals b.OrderID
                       join c in dc.Menu on b.MenuID equals c.MenuID
                       select new {
                           c.MenuName,
                           c.image,
                           c.Price,
                           b.Quantity,
                           a.AddressInfo,
                           a.OrderMoney,
                           a.PostMoney                         
                       };
            List<dynamic> oneList = new List<dynamic>();
            foreach (var item in list.ToList())
            {
                dynamic dyObj = new ExpandoObject();//需要引用命名空间 using System.Dynamic;
                dyObj.MenuName = item.MenuName;
                dyObj.image = item.image;               
                dyObj.Quantity = item.Quantity;
                dyObj.AddressInfo = item.AddressInfo;
                dyObj.OrderMoney = item.OrderMoney;
                dyObj.PostMoney = item.PostMoney;
                dyObj.Price = item.Price;
                oneList.Add(dyObj);
            }
            ViewBag.data1 = oneList;

            return View();
           
        }
    }
}