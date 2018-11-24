using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FoodCityApi.Models;
using System.Web;
using System.IO;
using System.Text;

namespace FoodCityApi.Controllers
{
    public class CodeController : ApiController
    {
        FoodDBEntities db = new FoodDBEntities();
        [Route("api/Code/Useryanzheng")]
        [HttpGet]
        public IHttpActionResult Useryanzheng(string tel)
        {
            var list = db.User.Where(s => s.Tel == tel).FirstOrDefault();
            if (list.Tel != null)
            {
                string code = Code.getRandom(); 
                string data = Code.Sendyan(tel, code);
                if (data != null)
                {
                    HttpContext.Current.Application["userid"] = list.UserID.ToString();
                    return Ok(code);
                }
                return Ok(data);
            }
            else
            {
                return Ok("none");
            }

        }
       
    }
}
