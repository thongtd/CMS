using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS.DataAccess.Core.Domain;
using CMS.DataAccess.Core.Extension;
using CMS.DataAccess.Core.Linqkit;
using CMS.DataAccess.Core.Repositories;
using CMS.DataAccess.Models;
using CMS.DataAccess.Persistence;
using CMS.DataAccess.Persistence.Repositories;
using MvcConnerstore.Collections;

namespace CMS.Dashboard.Controllers
{
    [RoutePrefix("Dashboard")]
    public class BlogController : Controller
    {
        private readonly IBlogRepository _blogRepository = new BlogRepository(new WorkContext());

        private readonly ITagRepository _tagRepository = new TagRepository(new WorkContext());

        [Route("Blog/Gets")]
        public ActionResult Gets()
        {
            using (var uow = new UnitOfWork(new WorkContext()))
            {
                var total = 0;
                var blogs = uow.Blog.Paging(PagedExtention.TryGetPageIndex("1"), int.MaxValue, out total, null);

                return Json(blogs, JsonRequestBehavior.AllowGet);
            }
        }

        [Route("Blog/Index")]
        public ActionResult Index(string pageIndex)
        {
            ViewBag.News = "active";

            return View();
        }

        [Route("Blog/Create")]
        public ActionResult Create()
        {
            ViewBag.News = "active";

            return View();
        }

        [HttpPost, ValidateInput(false), Route("Blog/Create")]
        public ActionResult Create(BlogRequest model, FormCollection frmCollect)
        {
            if (ModelState.IsValid)
            {
                var tags = frmCollect["hidden-tags"];

                _blogRepository.Add(model, tags);
                return RedirectToAction("Index");
            }
            return View();
        }

        [Route("Blog/Edit/{id}")]
        public ActionResult Edit(int id)
        {
            ViewBag.News = "active";

            using (var uow = new UnitOfWork(new WorkContext()))
            {
                var blog = uow.Blog.Get(id);

                var lstTags = _tagRepository.GetTagsForObject(blog.IdentityCode, Constants.ObjectName.Blog, Constants.ObjectName.BlogIdenityCode);
                ViewBag.ActiveTags = lstTags.HtmlTag;
                ViewBag.HiddenTags = lstTags.TagValue;

                return View(blog);
            }
        }

        [HttpPost, ValidateInput(false), Route("Blog/Edit")]
        public ActionResult Edit(BlogRequest model, FormCollection frmCollect)
        {
            if (ModelState.IsValid)
            {
                var tags = frmCollect["hidden-tags"];

                _blogRepository.Update(model, tags);
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost, Route("Blog/Active")]
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

        [HttpPost, Route("Blog/Delete")]
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