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
    public class ForumController : Controller
    {
        FoodDBEntities5 dc = new FoodDBEntities5();
        // GET: Forum
        /// <summary>
        ///增加编辑模态框
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Form(int? id)
        {
            if (id == null)
            {
                ViewBag.edit = 0;
                return View();
            }
            else
            {
                ViewBag.edit = 1;
                var list = dc.PostType.Where(c => c.PostTypeID == id);
                ViewBag.cate = list;
                return View();
            }
        }
        /// <summary>
        /// 分类列表
        /// </summary>
        /// <param name="Content"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public ActionResult ForumCoty(string Content, int? s)
        {

            if (s == 1)
            {
                var list = dc.PostType.Where(C => C.PostTypeName.Contains(Content));
                return View(list);
            }
            else
            {
                var list = dc.PostType;
                return View(list);
            }
        }
        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult Deleteclass(int ID)
        {
            var lisr = dc.PostType.Where(c => c.PostTypeID == ID).FirstOrDefault();
            dc.PostType.Remove(lisr);
            dc.SaveChanges();
            return Content("删除成功!");

        }
        /// <summary>
        /// 添加分类
        /// </summary>
        /// <param name="cc"></param>
        /// <returns></returns>
        public ActionResult Addclass(PostType cc)
        {
            dc.PostType.Add(cc);
            dc.SaveChanges();
            return Content("添加成功！");
        }
        /// <summary>
        /// 修改分类
        /// </summary>
        /// <param name="cc"></param>
        /// <returns></returns>
        public ActionResult Edclass(PostType cc)
        {
            var list = dc.PostType.Where(c => c.PostTypeID == cc.PostTypeID).FirstOrDefault();
            list.PostTypeName = cc.PostTypeName;
            dc.SaveChanges();
            return Content("修改成功！");
        }




        /// <summary>
        /// 帖子
        /// </summary>
        /// <param name="Content"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public ActionResult Post(string Content, int? s)
        {
            var list = from a in dc.Post
                       join b in dc.User on a.UserID equals b.UserID
                       join c in dc.PostType on a.PostTypeID equals c.PostTypeID
                       select new
                       {
                           a.PostID,
                           b.UserName,
                           c.PostTypeName,
                           a.Content,
                           a.BrowseVolume,
                           a.ReleaseTime,
                           a.PostName,
                           a.PostTitle
                       };
            if (s == 1)
            {
                var menus = list.Where(C => C.PostTitle.Contains(Content));
                List<dynamic> oneList = new List<dynamic>();
                foreach (var item in menus.ToList())
                {
                    dynamic dyObj = new ExpandoObject();//需要引用命名空间 using System.Dynamic;
                    dyObj.PostID = item.PostID;
                    dyObj.UserName = item.UserName;
                    dyObj.PostTypeName = item.PostTypeName;
                    dyObj.PostTitle = item.PostTitle;
                    dyObj.Content = item.Content;
                    dyObj.ReleaseTime = item.ReleaseTime;
                    dyObj.BrowseVolume = item.BrowseVolume;                   
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
                    dyObj.PostID = item.PostID;
                    dyObj.UserName = item.UserName;
                    dyObj.PostTypeName = item.PostTypeName;
                    dyObj.PostTitle = item.PostTitle;
                    dyObj.Content = item.Content;
                    dyObj.ReleaseTime = item.ReleaseTime;
                    dyObj.BrowseVolume = item.BrowseVolume;
                    oneList.Add(dyObj);
                }
                ViewBag.data = oneList;
                return View();
              
            }

        }
        /// <summary>
        /// 帖子详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            var list = from a in dc.Post
                       join b in dc.User on a.UserID equals b.UserID
                       join c in dc.PostType on a.PostTypeID equals c.PostTypeID
                       where a.PostID==id
                       select new
                       {
                           a.PostID,
                           b.UserName,
                           c.PostTypeName,
                           a.Content,
                           a.BrowseVolume,
                           a.ReleaseTime,
                           a.PostName,
                           a.PostTitle
                       };
            List<dynamic> oneList = new List<dynamic>();
            foreach (var item in list.ToList())
            {
                dynamic dyObj = new ExpandoObject();//需要引用命名空间 using System.Dynamic;
                dyObj.PostID = item.PostID;
                dyObj.UserName = item.UserName;
                dyObj.PostTypeName = item.PostTypeName;
                dyObj.PostTitle = item.PostTitle;
                dyObj.Content = item.Content;
                dyObj.ReleaseTime = item.ReleaseTime;
                dyObj.BrowseVolume = item.BrowseVolume;
                oneList.Add(dyObj);
            }
            ViewBag.data = oneList;
            ViewBag.posttype = dc.PostType;
            return View();

        }
        /// <summary>
        /// 删除帖子
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult Deletepost(int ID)
        {
            var lisr = dc.Post.Where(c => c.PostID == ID).FirstOrDefault();
            dc.Post.Remove(lisr);
            dc.SaveChanges();
            return Content("删除成功!");

        }       
        /// <summary>
        /// 修改帖子
        /// </summary>
        /// <param name="cc"></param>
        /// <returns></returns>
        public ActionResult EdPost(Post cc)
        {
            var list = dc.Post.Where(c => c.PostID == cc.PostID).FirstOrDefault();
            list.PostTypeID = cc.PostTypeID;
            list.PostTitle = cc.PostTitle;
            list.Content = cc.Content;
            
            dc.SaveChanges();
            return Content("修改成功！");
        }





        /// <summary>
        /// 评论
        /// </summary>
        /// <param name="Content"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public ActionResult PostEvaluation(string name,string title, int? s)
        {

            var list = from a in dc.PostEvaluation
                       join b in dc.User on a.UserID equals b.UserID
                       join c in dc.Post on a.PostID equals c.PostID
                       select new
                       {
                           a.PostEvaluationID,
                           b.UserName,
                           c.PostTitle,
                           a.Content,                         
                           a.ReleaseTime,
                          
                       };
            if (s == 1)
            {
                var menus = list.Where(C => C.UserName.Contains(name)&&C.PostTitle.Contains(title));
                List<dynamic> oneList = new List<dynamic>();
                foreach (var item in menus.ToList())
                {
                    dynamic dyObj = new ExpandoObject();//需要引用命名空间 using System.Dynamic;
                    dyObj.PostEvaluationID = item.PostEvaluationID;
                    dyObj.UserName = item.UserName;                  
                    dyObj.PostTitle = item.PostTitle;
                    dyObj.Content = item.Content;
                    dyObj.ReleaseTime = item.ReleaseTime;                 
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
                    dyObj.PostEvaluationID = item.PostEvaluationID;
                    dyObj.UserName = item.UserName;
                    dyObj.PostTitle = item.PostTitle;
                    dyObj.Content = item.Content;
                    dyObj.ReleaseTime = item.ReleaseTime;
                    oneList.Add(dyObj);
                }
                ViewBag.data = oneList;
                return View();

            }
          
        }
        /// <summary>
        /// 删除评论
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult DelEvl(int ID)
        {
            var lisr = dc.PostEvaluation.Where(c => c.PostEvaluationID == ID).FirstOrDefault();
            dc.PostEvaluation.Remove(lisr);
            dc.SaveChanges();
            return Content("删除成功!");

        }
        /// <summary>
        /// 查看评论
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Evadetail(int id)
        {
            var list = (from a in dc.PostEvaluation
                       join b in dc.User on a.UserID equals b.UserID
                       where a.PostEvaluationID==id
                       select new {
                           a.Content,
                           a.ReleaseTime,
                           b.UserName
                       }).FirstOrDefault();
            return Json(list);

        }
    }
}