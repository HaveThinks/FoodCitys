using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FoodCityApi.Models;
using System.Web;

namespace FoodCityApi.Controllers
{
    public class OrderController : ApiController
    {
        FoodDBEntities db = new FoodDBEntities();
        [Route("api/Order/Order")]
        [HttpGet]
        public IHttpActionResult OrderList()
        {
            var list = from o in db.Order
                       join d in db.OrderDetail on o.OrderID equals d.OrderID
                       join m in db.Menu on d.MenuID equals m.MenuID
                       join u in db.User on o.UserID equals u.UserID
                       where o.Isvaild == true
                       select new
                       {
                           o.ExpressNumber,
                           o.OrderState,
                           m.image,
                           m.Price,
                           o.OrderMoney,
                           d.OrderID,
                           d.Quantity,
                           m.MenuName,
                           d.TotalMoney
                       };
            var list1 = from q in list
                        group q by new
                        {
                            q.OrderID
                        }
                            into g
                            select new
                            {
                                g.Key,
                                g
                            };

            return Ok(list1);
        }
        [Route("api/Order/StatusDfk")]
        [HttpGet]
        public IHttpActionResult StatusDfk()
        {
            var list = from o in db.Order
                       join d in db.OrderDetail on o.OrderID equals d.OrderID
                       join m in db.Menu on d.MenuID equals m.MenuID
                       join u in db.User on o.UserID equals u.UserID
                       where o.Isvaild == true && o.OrderState == "待付款"
                       select new
                       {
                           o.ExpressNumber,
                           o.OrderState,
                           m.image,
                           m.Price,
                           o.OrderMoney,
                           d.OrderID,
                           d.Quantity,
                           m.MenuName,
                           d.TotalMoney
                       };
            var list1 = from q in list
                        group q by new
                        {
                            q.OrderID
                        }
                            into g
                            select new
                            {
                                g.Key,
                                g
                            };
            return Ok(list1);
        }
        [Route("api/Order/StatusDsh")]
        [HttpGet]
        public IHttpActionResult StatusDsh()
        {
            var list = from o in db.Order
                       join d in db.OrderDetail on o.OrderID equals d.OrderID
                       join m in db.Menu on d.MenuID equals m.MenuID
                       join u in db.User on o.UserID equals u.UserID
                       where o.Isvaild == true && o.OrderState == "卖家已发货"
                       select new
                       {
                           o.ExpressNumber,
                           o.OrderState,
                           m.image,
                           m.Price,
                           o.OrderMoney,
                           d.OrderID,
                           d.Quantity,
                           m.MenuName,
                           d.TotalMoney
                       }; ;
            var list1 = from q in list
                        group q by new
                        {
                            q.OrderID
                        }
                            into g
                            select new
                            {
                                g.Key,
                                g
                            };
            return Ok(list1);
        }
        [Route("api/Order/StatusYsh")]
        [HttpGet]
        public IHttpActionResult StatusYsh()
        {
            var list = from o in db.Order
                       join d in db.OrderDetail on o.OrderID equals d.OrderID
                       join m in db.Menu on d.MenuID equals m.MenuID
                       join u in db.User on o.UserID equals u.UserID
                       where o.Isvaild == true && o.OrderState == "交易成功"
                       select new
                       {
                           o.ExpressNumber,
                           o.OrderState,
                           m.image,
                           m.Price,
                           o.OrderMoney,
                           d.OrderID,
                           d.Quantity,
                           m.MenuName,
                           d.TotalMoney
                       };
            var list1 = from q in list
                        group q by new
                        {
                            q.OrderID
                        }
                            into g
                            select new
                            {
                                g.Key,
                                g
                            };
            return Ok(list1);
        }
        [Route("api/Order/StatusSuccess")]
        [HttpGet]
        public IHttpActionResult StatusSuccess()
        {
            var list = from o in db.Order
                       join d in db.OrderDetail on o.OrderID equals d.OrderID
                       join m in db.Menu on d.MenuID equals m.MenuID
                       join u in db.User on o.UserID equals u.UserID
                       where o.Isvaild == true && o.OrderState == "交易成功"
                       select new
                       {
                           o.ExpressNumber,
                           o.OrderState,
                           m.image,
                           m.Price,
                           o.OrderMoney,
                           d.OrderID,
                           d.Quantity,
                           m.MenuName,
                           d.TotalMoney
                       };
            var list1 = from q in list
                        group q by new
                        {
                            q.OrderID
                        }
                            into g
                            select new
                            {
                                g.Key,
                                g
                            };
            return Ok(list1);
        }

        [Route("api/Order/CancelOrder")]
        [HttpGet]
        public IHttpActionResult CancelOrder(int OrderID)
        {
            var list = db.Order.Where(x => x.OrderID == OrderID).FirstOrDefault();
            list.OrderState = "交易取消";
            db.SaveChanges();
            return Ok();
        }
        [Route("api/Order/DeleteOrder")]
        [HttpGet]
        public IHttpActionResult DeleteOrder(int OrderID)
        {
            var list = db.Order.Where(x => x.OrderID == OrderID).FirstOrDefault();
            list.Isvaild = false;
            db.SaveChanges();
            return Ok();
        }
        [Route("api/Order/Confrim")]
        [HttpGet]
        public IHttpActionResult Confrim(int OrderID)
        {
            var list = db.Order.Where(x => x.OrderID == OrderID).FirstOrDefault();
            list.OrderState = "交易成功";
            db.SaveChanges();
            return Ok();
        }
        [Route("api/Order/Search")]
        [HttpGet]
        public IHttpActionResult Search(string OrderNumber)
        {
            var list = from o in db.Order
                       join d in db.OrderDetail on o.OrderID equals d.OrderID
                       join m in db.Menu on d.MenuID equals m.MenuID
                       join u in db.User on o.UserID equals u.UserID
                       where o.ExpressNumber.Contains(OrderNumber) && o.Isvaild == true
                       select new
                       {
                           o.ExpressNumber,
                           o.OrderState,
                           m.image,
                           m.Price,
                           o.OrderMoney,
                           d.OrderID,
                           d.Quantity,
                           m.MenuName,
                           d.TotalMoney
                       };
            var list1 = from q in list
                        group q by new
                        {
                            q.OrderID
                        }
                            into g
                            select new
                            {
                                g.Key,
                                g
                            };
            return Ok(list1);
        }
        [Route("api/Order/Pay")]
        [HttpGet]
        public IHttpActionResult Pay(int OrderID)
        {
            var list = from o in db.Order 
                       join od in db.OrderDetail on o.OrderID equals od.OrderID 
                       join a in db.ShipAddress on o.UserID equals a.UserID
                       join m in db.Menu on od.MenuID equals m.MenuID
                       where o.OrderID==OrderID&&a.Default==true
                       select new 
                       {
                           a.Consignee,
                           a.Address,
                           a.Ctel,
                           m.MenuID,
                           m.MenuName,
                           m.image,
                           m.Price,
                           od.Quantity,
                           m.Shipping,
                           o.OrderID
                       };
            return Ok(list);
        }
        [Route("api/Order/GoPay")]
        [HttpGet]
        public IHttpActionResult GoPay(string data)
        {
            string[] da = data.Split(',');
            int orderid=Convert.ToInt32(da[2]);
            var list = db.Order.Where(o => o.OrderID == orderid).FirstOrDefault();
            list.Note = da[1];
            list.OrderState = "交易成功";
            db.SaveChanges();
            return Ok(list);
        }
    }
}
