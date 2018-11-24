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
    public class UserController : ApiController
    {

        FoodDBEntities db = new FoodDBEntities();
        [Route("api/User/Login")]
        [HttpPost]
        public IHttpActionResult Login([FromBody] User us)
        {
            var list = db.User.Where(u => u.UserName == us.UserName && u.UserPwd == us.UserPwd).FirstOrDefault();
            HttpContext.Current.Application["userid"] = list.UserID.ToString();
            return Ok(list);
        }
        [Route("api/User/Register")]
        [HttpPost]
        public IHttpActionResult Register([FromBody] User us)
        {
            var list = db.User.Where(u => u.UserName == us.UserName).FirstOrDefault();
            if (list==null)
            {
                us.CreatTime = DateTime.Now;
                us.Grade = "普通用户";
                us.Vip = true;
                us.Score = 1000;
                db.User.Add(us);
                db.SaveChanges();
                return Ok(us.UserID);
            }
            else
            {
                return Ok();
            }         
        }
        [Route("api/User/Data")]
        [HttpGet]
        public IHttpActionResult UserData()
        {
            int UserID = Convert.ToInt32(System.Web.HttpContext.Current.Application["userid"]);
            var list = db.User.Where(u => u.UserID == UserID).FirstOrDefault();
            return Ok(list);


        }
        [Route("api/User/Address")]
        [HttpGet]
        public IHttpActionResult Address()
        {
            var list = db.ShipAddress.Where(s => s.IsValid == true).ToList();
            return Ok(list);
        }
        [Route("api/User/AddAddress")]
        [HttpGet]
        public IHttpActionResult AddAddress(string Address, string Consignee, string Ctel)
        {
            ShipAddress sa = new ShipAddress
            {
                UserID = Convert.ToInt32(System.Web.HttpContext.Current.Application["userid"]),
                Address = Address,
                Default = false,
                CreatTime = DateTime.Now,
                Consignee = Consignee,
                Ctel = Ctel
            };
            db.ShipAddress.Add(sa);
            db.SaveChanges();
            return Ok();
        }
        [Route("api/User/EditAddress")]
        [HttpGet]
        public IHttpActionResult EditAddress(int AddressID)
        {
            var list = db.ShipAddress.Where(s => s.ShipAddressID == AddressID).FirstOrDefault();
            return Ok(list);
        }
        [Route("api/User/DeleteAddress")]
        [HttpGet]
        public IHttpActionResult DeleteAddress(int AddressID)
        {
            var list = db.ShipAddress.Where(s => s.ShipAddressID == AddressID).FirstOrDefault();
            list.IsValid = false;
            db.SaveChanges();
            return Ok();
        }
        [Route("api/User/UpdateAddress")]
        [HttpGet]
        public IHttpActionResult SubAddress(int AddressID, string Address, string Consignee, string Ctel)
        {
            var list = db.ShipAddress.Where(s => s.ShipAddressID == AddressID).FirstOrDefault();
            list.Address = Address;
            list.Consignee = Consignee;
            list.Ctel = Ctel;
            db.SaveChanges();
            return Ok();
        }
        [Route("api/User/Default")]
        [HttpGet]
        public IHttpActionResult Default(int AddressID)
        {
            var list = db.ShipAddress.Where(s => s.Default == true).FirstOrDefault();
            list.Default = false;
            db.SaveChanges();
            var list1 = db.ShipAddress.Where(s => s.ShipAddressID == AddressID).FirstOrDefault();
            list1.Default = true;
            db.SaveChanges();
            return Ok();
        }
        [Route("api/User/Logout")]
        [HttpGet]
        public IHttpActionResult Logout()
        {
            HttpContext.Current.Application.Remove("userid");
            return Ok();
        }
    }
}
