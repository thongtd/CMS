using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS.Dashboard.Code.Models;
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
    [RoutePrefix("admin")]
    public class BlogController : Controller
    {
        private const string IndexPageTile = "Danh sách tin tức";
        private const string EditPageTile = "Sửa thông tin bài viết";
        private const string CreatePageTile = "Thêm mới bài viết";

        private readonly IBlogRepository blogRepository = new BlogRepository(new WorkContext());
        private readonly ITagRepository tagRepository = new TagRepository(new WorkContext());

        private readonly IList<Breadcurmb> breadcurmbs = new List<Breadcurmb>();

        public BlogController()
        {
            breadcurmbs.Add(new Breadcurmb
            {
                ActionLink = "/",
                Lable = "Home"
            });

            breadcurmbs.Add(new Breadcurmb
            {
                ActionLink = "#",
                Lable = "Dashboard"
            });
        }

        [Route("blog/get")]
        public ActionResult Get()
        {
            using (var uow = new UnitOfWork(new WorkContext()))
            {
                var total = 0;
                var blogs = uow.Blog.Paging(PagedExtention.TryGetPageIndex("1"), int.MaxValue, out total, null);

                return Json(blogs, JsonRequestBehavior.AllowGet);
            }
        }

        [Route("blog")]
        public ActionResult Index(string pageIndex)
        {
            breadcurmbs.Add(new Breadcurmb
            {
                ActionLink = Url.Action("Index"),
                Lable = IndexPageTile
            });

            ViewBag.Breadcurmbs = breadcurmbs;
            ViewBag.Title = IndexPageTile;
            ViewBag.News = "active";

            return View();
        }

        [Route("blog/create")]
        public ActionResult Create()
        {
            breadcurmbs.Add(new Breadcurmb
            {
                ActionLink = Url.Action("Index"),
                Lable = IndexPageTile
            });

            breadcurmbs.Add(new Breadcurmb
            {
                ActionLink = Url.Action("Create"),
                Lable = CreatePageTile
            });

            ViewBag.Breadcurmbs = breadcurmbs;
            ViewBag.Title = CreatePageTile;
            ViewBag.News = "active";

            return View();
        }

        [HttpPost, ValidateInput(false), Route("blog/create")]
        public ActionResult Create(BlogRequest model, FormCollection frmCollect)
        {
            if (ModelState.IsValid)
            {
                var tags = frmCollect["hidden-tags"];

                blogRepository.Add(model, tags);
                return RedirectToAction("Index");
            }
            return View();
        }

        [Route("blog/edit/{id}")]
        public ActionResult Edit(int id)
        {
            breadcurmbs.Add(new Breadcurmb
            {
                ActionLink = Url.Action("Index"),
                Lable = IndexPageTile
            });

            breadcurmbs.Add(new Breadcurmb
            {
                ActionLink = Url.Action("Edit"),
                Lable = EditPageTile
            });

            ViewBag.Breadcurmbs = breadcurmbs;
            ViewBag.Title = EditPageTile;
            ViewBag.News = "active";

            using (var uow = new UnitOfWork(new WorkContext()))
            {
                var blog = uow.Blog.Get(id);

                var lstTags = tagRepository.GetTagsForObject(blog.IdentityCode, Constants.ObjectName.Blog, Constants.ObjectName.BlogIdenityCode);
                ViewBag.ActiveTags = lstTags.HtmlTag;
                ViewBag.HiddenTags = lstTags.TagValue;

                return View(blog);
            }
        }

        [HttpPost, ValidateInput(false), Route("blog/edit")]
        public ActionResult Edit(BlogRequest model, FormCollection frmCollect)
        {
            if (ModelState.IsValid)
            {
                var tags = frmCollect["hidden-tags"];

                blogRepository.Update(model, tags);
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost, Route("blog/active")]
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

        [HttpPost, Route("blog/delete")]
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