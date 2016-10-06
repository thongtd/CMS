using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS.DataAccess.Core.Domain;
using CMS.DataAccess.Core.Linqkit;
using CMS.DataAccess.Core.Repositories;
using CMS.DataAccess.Models;
using CMS.DataAccess.Persistence;
using CMS.DataAccess.Persistence.Repositories;
using MvcConnerstore.Collections;

namespace CMS.Dashboard.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogRepository _blogRepository = new BlogRepository(new WorkContext());

        public ActionResult Gets()
        {
            using (var uow = new UnitOfWork(new WorkContext()))
            {
                var total = 0;
                var blogs = uow.Blog.Paging(PagedExtention.TryGetPageIndex("1"), int.MaxValue, out total, null);
                
                return Json(blogs, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Index(string pageIndex)
        {
            ViewBag.News = "active";

            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(BlogRequest model)
        {
            if (ModelState.IsValid)
            {
                var blog = (Blog)model;
                blog.CreatedDate = DateTime.UtcNow;

                using (var uow = new UnitOfWork(new WorkContext()))
                {
                    uow.Blog.Add(blog);
                    uow.Complete();
                    return View("Index");
                }
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            using (var uow = new UnitOfWork(new WorkContext()))
            {
                var blog = uow.Blog.Get(id);

                return View(blog);
            }
        }

        [HttpPost]
        public ActionResult Edit(BlogRequest model)
        {
            if (ModelState.IsValid)
            {
                using (var uow = new UnitOfWork(new WorkContext()))
                {
                    var blog = uow.Blog.Get(model.Id);

                    _blogRepository.ConvertToModel(ref blog, model);

                    blog.ModeifiedDate = DateTime.UtcNow;

                    uow.Complete();
                    return View();
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult Active(int id)
        {
            using (var uow = new UnitOfWork(new WorkContext()))
            {
                var blog = uow.Blog.Get(id);

                blog.IsActive = !blog.IsActive;
                uow.Complete();
                return Json(new
                {
                    Status = true,
                    Message = string.Empty
                });
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (var uow = new UnitOfWork(new WorkContext()))
            {
                var blog = uow.Blog.Get(id);

                uow.Blog.Remove(blog);
                uow.Complete();
                return Json(new
                {
                    Status = true,
                    Message = string.Empty
                });
            }
        }
    }
}