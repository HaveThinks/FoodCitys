using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodCityBackstage.Models;
using System.IO;

namespace FoodCityBackstage.Controllers
{
    [Models.MyFilter]
    public class AdminController : Controller
    {
        FoodDBEntities5 dc = new FoodDBEntities5();
        // GET: Admin
        /// <summary>
        /// 搜索管理员
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public ActionResult Index(string UserName, int? s)
        {

            if (s == 1)
            {
                var list = from a in dc.Admin where a.AdName.Contains(UserName) select a;
                ViewBag.power = Session["power"];
                return View(list);
                //return Json(list);
            }
            else
            {
                var list = dc.Admin.Where(c => c.State == "正常");
                ViewBag.power = Session["power"];
                return View(list);
            }



        }
        //Bander管理
        public ActionResult Bander(string BanderName, int? s)
        {

            var lis=from a in dc.Bannder join b in dc.BannderType on a.BannerType equals b.BannderTypeid select new {
                a.ID,
                a.BannderName,
                b.BannderTypename,
                a.Image,
                a.Isdefault,
               
            };
            List<Banders> bds = new List<Banders>();
            foreach (var item in lis)
            {
                Banders aa = new Banders();
                aa.ID = item.ID;
                aa.BannerTypeName = item.BannderTypename;
                aa.BannderName = item.BannderName;
                aa.Image = item.Image;
                bds.Add(aa);
            }
            if (s == 1)
            {
                var lists = bds.Where(c => c.BannderName.Contains(BanderName));

                return View(lists);
                //return Json(list);
            }
            else
            {               

                return View(bds);
            }



        }
        public ActionResult BanderType(string Content, int? s)
        {
            if (s == 1)
            {
                var list = dc.BannderType.Where(C => C.BannderTypename.Contains(Content));
                return View(list);
            }
            else
            {
                var list = dc.BannderType;
                return View(list);
            }

        }
        public ActionResult BanderTypeform(int? id)
        {

            if (id != null)
            {
                ViewBag.edit = 1;
                var list = dc.BannderType.Where(c => c.BannderTypeid == id);
                ViewBag.bander = list;
                return View();
            }
            else
            {
                ViewBag.edit = 0;
                return View();
            }
        }
        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult Deleteclass(int ID)
        {
            var lisr = dc.BannderType.Where(c => c.BannderTypeid == ID).FirstOrDefault();
            dc.BannderType.Remove(lisr);
            dc.SaveChanges();
            return Content("删除成功!");

        }
        /// <summary>
        /// 添加分类
        /// </summary>
        /// <param name="cc"></param>
        /// <returns></returns>
        public ActionResult AddMenuclass(BannderType cc)
        {
            dc.BannderType.Add(cc);
            dc.SaveChanges();
            return Content("添加成功！");
        }
        /// <summary>
        /// 修改分类
        /// </summary>
        /// <param name="cc"></param>
        /// <returns></returns>
        public ActionResult EdMenuclass(BannderType cc)
        {
            var list = dc.BannderType.Where(c => c.BannderTypeid == cc.BannderTypeid).FirstOrDefault();
            list.BannderTypename = cc.BannderTypename;
            dc.SaveChanges();
            return Content("修改成功！");
        }
        public ActionResult Banderform(int? id)
        {

            if (id != null)
            {
                ViewBag.edit = 1;
                var list = dc.Bannder.Where(c => c.ID == id);
                ViewBag.bander = list;
                ViewBag.banderType = dc.BannderType;
                return View();
            }
            else
            {
                ViewBag.edit = 0;
                ViewBag.banderType = dc.BannderType;
                return View();
            }
        }
        public ActionResult Addban(Bannder bannder)
        {
            //int aa =Convert.ToInt32(Request.Form["BannderType"]);
            //bannder.BannerType = aa;
            dc.Bannder.Add(bannder);
            dc.SaveChanges();
            return Content("添加成功！");
        }
        public ActionResult Editban(Bannder bannder)
        {
            var list = dc.Bannder.Where(c => c.ID == bannder.ID).FirstOrDefault();
            list.BannderName = bannder.BannderName;
            list.BannerType = bannder.BannerType;
            list.Image = bannder.Image;
            list.Isdefault = bannder.Isdefault;
            dc.SaveChanges();
            return Content("修改成功！");
        }
        public ActionResult Delbander(int id)
        {
            var list = dc.Bannder.Where(c => c.ID == id).FirstOrDefault();
            dc.Bannder.Remove(list);
            dc.SaveChanges();
            return Content("删除成功！");
        }

        /// <summary>
        /// 增加\编辑管理员
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Form(int? id)
        {
            if (id!=null)
            {
                ViewBag.edit = 1;
                var list = dc.Admin.Where(c => c.AdID == id&&c.State=="正常");
                ViewBag.admin = list;
                ViewBag.power = Session["power"];
                return View();
            }
            else
            {
                ViewBag.edit = 0;
                return View();
            }
           
        }
        /// <summary>
        /// 修改管理员
        /// </summary>
        /// <param name="admin"></param>
        /// <returns></returns>
        public ActionResult Addmin(Admin admin)
        {
            admin.CreatTime = DateTime.Now;
            admin.State = "正常";
            
            dc.Admin.Add(admin);
            dc.SaveChanges();
            return Content("ok");
        }
        public ActionResult Eddmin(Admin admin)
        {
            var list = dc.Admin.Where(c => c.AdID == admin.AdID);
            foreach (var item in list)
            {
                item.AdName = admin.AdName;
                item.Tel = admin.Tel;
                item.Email = admin.Email;
                item.Power = admin.Power;
                item.ImageUrl = admin.ImageUrl;
            }
            dc.SaveChanges();
            return Content("ok");
        }
        public ActionResult DeLAdmin(int aa)
        {
            var list = dc.Admin.Where(c=>c.AdID==aa);
            foreach (var item in list)
            {
                item.State = "已删除";
            }
          
            dc.SaveChanges();
            return Content("删除成功！");
        }
        public ActionResult AddPricBander()
        {

            if (Request.Files["FileUpload1"] != null)
            {
                string savepath = Server.MapPath("FoodCity/Content/images");
                //string savepath = Server.MapPath("~/Content/Backstage/assets/images/Bander");
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
        //存储图片
        public ActionResult AddPric()
        {
         
            if (Request.Files["FileUpload1"] != null)
            {

                string savepath = Server.MapPath("~/Content/Backstage/assets/images/users");
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
        //修改密码
        public ActionResult UpdatePwd(string old,string news)
        {
            int id = (int)Session["adid"];
            var list = dc.Admin.Where(c => c.AdID == id).FirstOrDefault();
            if (list.AdPwd!=old)
            {
                return Content("旧密码输入错误");
            }
            else
            {
                list.AdPwd = news;
                dc.SaveChanges();
                return Content("密码更改成功！");
            }
           
        }
    }
}