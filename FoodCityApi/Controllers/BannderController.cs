using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FoodCityApi.Models;

namespace FoodCityApi.Controllers
{   
    public class BannderController : ApiController
    {
        FoodDBEntities db = new FoodDBEntities();
        [Route("api/Bannder/Bannder")]
        [HttpGet]
        public IHttpActionResult BannderIndex()
        {
            var banner = from b in db.Bannder where b.BannerType == 1 && b.Isdefault == true select b;
            return Ok(banner);
        }
        [Route("api/Bannder/Bannders")]
        [HttpGet]
        public IHttpActionResult BannderSpeak()
        {
            var banner = from b in db.Bannder where b.BannerType == 2 && b.Isdefault == true select b;
            return Ok(banner);
        }
    }
}
