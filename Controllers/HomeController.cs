using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBlog.Models;


namespace MyBlog.Controllers
{

    public class HomeController : Controller
    {
        MyBlogContext db=new MyBlogContext();
        public ActionResult Index()
        {
            var article = db.Articles.ToList();
            return View(article.OrderByDescending(x=>x.Date));
        }
      
        public ActionResult Category(int category_Id)
        {
            var query = from article in db.Articles
                where article.Categories.Any(c => c.CategoryId == category_Id)
                select article;
            var articles = query.ToList();
            return View(articles.OrderByDescending(x => x.Date));
        }

        public ActionResult Tag(int tag_Id)
        {
            var query = from article in db.Articles
                where article.Tags.Any(c => c.Tag_Id == tag_Id)
                select article;
            var articles = query.ToList();
            return View(articles.OrderByDescending(x => x.Date));

        }

        public ActionResult SinglePost(int Id)
        {
            var article = db.Articles.FirstOrDefault(x => x.ArticleId == Id);
           
            return View(article);
        }

        public ActionResult CategoryWidget()
        {
            var categories = db.Categories.ToList();
            return View(categories);
        }

        

        public ActionResult TagWidget()
        {
            var tags = db.Tags.ToList();
            return View(tags);
        }

        public ActionResult AddComment(string Name,string Email,string Surname, string Message, int ArticleId)
        {
            Comment com = new Comment
            {
                ArticleId = ArticleId,
                Date = DateTime.Now,
                Name = Name,
                Surname = Surname,
                Content_ = Message
            };

            db.Comments.Add(com);
            db.SaveChanges();

            return RedirectToAction("SinglePost","Home",new {id=ArticleId});
        }

        public ActionResult Search(string text)
        {
            var article = db.Articles.Where(c=>c.Title.Contains(text));
           
            return View(article);
        }

        public ActionResult Fill_Controller()
        {
            var categories = db.Categories.ToList();
            return View(categories);
        }

        public ActionResult Archive()
        {
            var article = db.Articles.ToList();
            return View(article.OrderByDescending(x => x.Date));
        }

      

    }
}