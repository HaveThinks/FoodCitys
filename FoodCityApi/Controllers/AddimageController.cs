using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using System.Web;

namespace FoodCityApi.Controllers
{
    public class AddimageController : ApiController
    {
        [Route("api/Add/image")]
        [HttpPost]
        public IHttpActionResult Addimage()
        {

            
            HttpRequest request = HttpContext.Current.Request;
            HttpFileCollection fileCollection = request.Files;
            if (fileCollection != null)
            {

                var mapPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Content/images/");
                string ext = Path.GetExtension(fileCollection[0].FileName);
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
                        if (fileCollection[0].ContentLength < 1024000)
                        {
                            fileName = Guid.NewGuid().ToString().Substring(0, 8) + ext;
                            fileCollection[0].SaveAs(mapPath + "\\" + fileName);
                        }
                        break;
                }
                return Ok(fileName);
            }
            else
            {
                return Ok("");
            }



        }
    }
}
