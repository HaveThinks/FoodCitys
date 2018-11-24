using FoodCityApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FoodCityApi.Controllers
{
    public class MenuController : ApiController
    {
        FoodDBEntities db = new FoodDBEntities();
        [Route("api/Menu/Menu")]
        [HttpGet]
        public IHttpActionResult List()
        {
            var list = (from m in db.Menu
                        orderby m.Price descending
                        select m).Take(4);
            return Ok(list);
        }
        [Route("api/Menu/Detail")]
        [HttpGet]
        public IHttpActionResult Detail(int MenuID)
        {
            var list = db.Menu.Where(x => x.MenuID == MenuID).FirstOrDefault();
            return Ok(list);
        }
        [Route("api/Menu/Search")]
        [HttpGet]
        public IHttpActionResult Search()
        {
            var list = (from m in db.Menu orderby m.Sales descending 
                       select m).Take(4);
            return Ok(list);
        }
        [Route("api/Menu/Searchs")]
        [HttpGet]
        public IHttpActionResult Search(string MenuName)
        {
            var list = (from m in db.Menu
                        orderby m.Sales descending
                        where m.MenuName.Contains(MenuName)
                        select m).Take(4);
            return Ok(list);
        }
        [Route("api/Menu/More")]
        [HttpGet]
        public IHttpActionResult moreMenu()
        {
            var list = from m in db.Menu select m;
            return Ok(list);
        }
        [Route("api/Menu/SortSales")]
        [HttpGet]
        public IHttpActionResult SortSales()
        {
            var  list=from m in db.Menu orderby m.Sales descending select m;
            return Ok(list);
        }
        [Route("api/Menu/SortPrice")]
        [HttpGet]
        public IHttpActionResult SortPrice()
        {
            var list = from m in db.Menu orderby m.Price descending select m;
            return Ok(list);
        }
        [Route("api/Menu/Paging")]
        [HttpGet]
        public IHttpActionResult Paging(int pageindex,int pagesize)
        {
            pageindex = (db.Menu.Count() + pagesize - 1) / pagesize;
            var list = (from m in db.Menu orderby m.Sales select m).Skip((pageindex - 1) * pagesize).Take(pagesize).ToList() ;
            pageindex++;
            return Ok(list);
        }
        [Route("api/Menu/CreateOrder")]
        [HttpGet]
        public IHttpActionResult GreateOrder(int MenuID,int quantity)
        {
            if (System.Web.HttpContext.Current.Application["userid"]!=null)
            {
                ShopCar s = new ShopCar();
                s.UserID = Convert.ToInt32(System.Web.HttpContext.Current.Application["userid"]); ;
                s.MenuID =MenuID;
                s.Quantity = quantity;
                s.CreateTime = DateTime.Now;
                s.IsVaild = true;
                db.ShopCar.Add(s);
                db.SaveChanges();
                return Ok(s.ShopCarID);
            }
            else
            {
                return Ok();
            }
           
        }
    }
}
