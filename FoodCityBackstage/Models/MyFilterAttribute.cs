using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodCityBackstage.Models
{
    public class MyFilterAttribute: ActionFilterAttribute
    {
        
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            HttpContextBase http = filterContext.HttpContext;
            if (http.Session["name"] == null)
            {
                http.Response.Redirect("/Login/Login");
            }
        }
    }
}