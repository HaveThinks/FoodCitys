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
    public class ShopCarController : ApiController
    {
        FoodDBEntities db = new FoodDBEntities();
        /// <summary>
        /// 购物车列表
        /// </summary>
        /// <returns></returns>
        [Route("api/ShopCar/ShopList")]
        [HttpGet]
        public IHttpActionResult ShopList()
        {
            int UserID = Convert.ToInt32(System.Web.HttpContext.Current.Application["userid"]);
            var list = from s in db.ShopCar
                       join m in db.Menu on s.MenuID equals m.MenuID
                       where s.IsVaild == true && s.UserID == UserID
                       select new
                       {
                           s.Quantity,
                           m.MenuName,
                           m.Price,
                           m.MenuID,
                           m.image,
                           m.Store,
                           s.ShopCarID
                       };
            return Ok(list);
        }
        [Route("api/ShopCar/AddQuantity")]
        [HttpGet]
        public IHttpActionResult AddQuantity(int quantity, int shopcarid)
        {
            var list = db.ShopCar.Where(s => s.ShopCarID == shopcarid).First();
            list.Quantity = quantity;
            db.SaveChanges();
            return Ok();
        }
        [Route("api/ShopCar/DeletePro")]
        [HttpGet]
        public IHttpActionResult DeletePro(int shopcarid)
        {
            var list = db.ShopCar.Where(s => s.ShopCarID == shopcarid).FirstOrDefault();
            list.IsVaild = false;
            db.SaveChanges();
            return Ok();
        }
        [Route("api/ShopCar/Pay")]
        [HttpGet]
        public IHttpActionResult Pay(string carid)
        {
            string shopcarid = carid.Substring(0, carid.Length - 1);
            string[] id = shopcarid.Split(',');

            var list = from s in db.ShopCar
                       join a in db.ShipAddress on s.UserID equals a.UserID
                       join m in db.Menu on s.MenuID equals m.MenuID
                       where id.Contains(s.ShopCarID.ToString()) && a.Default == true && s.IsVaild == true && a.IsValid == true
                       select new
                       {
                           a.Consignee,
                           a.Address,
                           a.Ctel,
                           m.MenuID,
                           m.MenuName,
                           m.image,
                           m.Price,
                           s.ShopCarID,
                           s.Quantity,
                           m.Shipping
                       };
            return Ok(list);
        }
        [Route("api/ShopCar/CreateOrder")]
        [HttpGet]
        public IHttpActionResult CreateOrder(string data)
        {
            Order o = new Order();
            string d = data.Substring(0, data.Length - 1);
            string[] da = d.Split(',');
            
            o.UserID = Convert.ToInt32(System.Web.HttpContext.Current.Application["userid"]);
            o.OrderState = "待付款";
            o.OrderMoney = Convert.ToInt32(da[0]);
            o.CreateTime = DateTime.Now;  //创建订单的时间
            string aa = DateTime.Now.ToShortDateString();
            string dd = aa.Split('/')[0] + aa.Split('/')[1] + aa.Split('/')[2];   //取当前时间
            o.ExpressNumber = dd + new Random().Next(11111, 99999);  //订单编号
            o.AddressInfo = (db.ShipAddress.Where(s => s.Default == true).FirstOrDefault()).Address;
            o.Isvaild = true;
            o.Note = da[1].ToString();
            db.Order.Add(o);
            db.SaveChanges();
            OrderDetail od = new OrderDetail();
            for (int i = 2; i < da.Length ; i++)
            {
                string carid = da[i];
                 var list = (from s in db.ShopCar
                       join m in db.Menu on s.MenuID equals m.MenuID
                       where  s.IsVaild == true&&s.ShopCarID.ToString()==carid
                       select new
                       {
                           s.Quantity,
                           s.MenuID,
                           m.Price,
                          
                       }).FirstOrDefault();
                od.OrderID = o.OrderID;
                od.MenuID = Convert.ToInt32(da[i]);
                od.Quantity=list.Quantity;
                od.TotalMoney = list.Quantity * list.Price;
                db.OrderDetail.Add(od);
                db.SaveChanges();
            }
            return Ok(o);
        }
        [Route("api/ShopCar/ConfirmPay")]
        [HttpGet]
        public IHttpActionResult ConfirmPay(int orderid)
        {
            var list = db.Order.Where(o => o.OrderID == orderid).FirstOrDefault();
            list.OrderState = "交易成功";
            db.SaveChanges();
            return Ok(list.OrderID);
        }
        [Route("api/ShopCar/OrderData")]
        [HttpGet]
        public IHttpActionResult OrderData(int orderid)
        {
            var list = from o in db.Order 
                       join d in db.OrderDetail on o.OrderID equals d.OrderID 
                       join s in db.ShipAddress on o.UserID equals s.UserID
                       join m in db.Menu on d.MenuID equals m.MenuID 
                       where o.OrderID == orderid &&s.Default==true&&s.IsValid==true&&o.Isvaild==true
                       select new 
                       { 
                           s.Address,
                           s.Consignee,
                           s.Ctel,
                           m.MenuID,
                           m.MenuName,
                           m.image,
                           m.Price,
                           d.Quantity,
                           d.TotalMoney,
                           m.Shipping,
                           o.ExpressNumber,
                           o.CreateTime
                       };
            return Ok(list);
        }
    }
}
