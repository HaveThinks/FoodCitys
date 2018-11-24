using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.IO;
using FoodCityApi.Models;

namespace FoodCityApi.Controllers
{
    public class PostController : ApiController
    {
        FoodDBEntities db = new FoodDBEntities();

        [Route("api/Post/List")]
        [HttpGet]
        public IHttpActionResult List()
        {
            var list = from p in db.Post orderby p.ReleaseTime descending select p;
            return Ok(list);
        }
        [Route("api/Post/Type")]
        [HttpGet]
        public IHttpActionResult Type()
        {
            var list = db.PostType.ToList();
            return Ok(list);
        }
        [Route("api/Post/Detail")]
        [HttpGet]
        public IHttpActionResult Detail(int PostID)
        {
            var list = db.Post.Where(p => p.PostID == PostID).FirstOrDefault();
            list.BrowseVolume += 1;
            db.SaveChanges();
            return Ok(list);
        }
        [Route("api/Post/GetType")]
        [HttpGet]
        public IHttpActionResult TypePost(int PostTypeID)
        {
            var list = db.Post.Where(p => p.PostTypeID == PostTypeID).ToList();
            return Ok(list);
        }
        [Route("api/Post/Release")]
        [HttpGet]
        public IHttpActionResult Release(int PostTypeID,string PostName,string PostTitle,string Content)
        {
            Post p = new Post();
            p.PostTypeID = PostTypeID;
            p.PostName=PostName;
            p.PostTitle = PostTitle;
            p.Content = Content;
            p.ReleaseTime = DateTime.Now;
            p.BrowseVolume =0;
            db.Post.Add(p);
            db.SaveChanges();
            return Ok();
        }
    }
}
