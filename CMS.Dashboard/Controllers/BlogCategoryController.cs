using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CMS.Dashboard.Code.Models;
using CMS.DataAccess.Core.Domain;
using CMS.DataAccess.Core.Linqkit;
using CMS.DataAccess.Core.Repositories;
using CMS.DataAccess.Models;
using CMS.DataAccess.Persistence;
using CMS.DataAccess.Persistence.Repositories;
using MvcConnerstore.Collections;

namespace CMS.Dashboard.Controllers
{
    [RoutePrefix("admin")]
    public class BlogCategoryController : Controller
    {
        private const string IndexPageTile = "Danh sách nhóm tin";

        private readonly IBlogCategoryRepository blogCategoryRepository = new BlogCategoryRepository(new WorkContext());

        private readonly IList<Breadcurmb> breadcurmbs = new List<Breadcurmb>();

        public BlogCategoryController()
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

        [Route("blog-category/get")]
        public ActionResult Get()
        {
            using (var uow = new UnitOfWork(new WorkContext()))
            {
                var total = 0;
                var blogCategorys = uow.BlogCategory.Paging(PagedExtention.TryGetPageIndex("1"), int.MaxValue, out total, null);

                if (blogCategorys.Any())
                {
                    foreach (var category in blogCategorys)
                    {
                        if (category.Level.Length > 5)
                        {
                            var space = string.Empty;
                            while (space.Length < category.Level.Length - 5)
                            {
                                space += "_";
                            }
                            category.Name = space + category.Name;
                        }
                    }
                }

                return Json(blogCategorys, JsonRequestBehavior.AllowGet);
            }
        }

        [Route("blog-category")]
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

        [HttpGet, Route("blog-category/create")]
        public ActionResult Create()
        {
            ViewBag.News = "active";

            using (var uow = new UnitOfWork(new WorkContext()))
            {
                var predicate = PredicateBuilder.Create<BlogCategory>(s => s.Order > 0);

                var maxOrderNum = uow.BlogCategory.Find(predicate).OrderByDescending(s => s.Order).Select(s => s.Order).FirstOrDefault();
                ViewBag.OrderNum = maxOrderNum + 1;
                return View();
            }
        }

        [HttpPost, Route("blog-category/create")]
        public ActionResult Create(BlogCategoryRequest model)
        {
            if (ModelState.IsValid)
            {
                var blogCategory = (BlogCategory)model;
                blogCategory.CreatedDate = DateTime.UtcNow;

                using (var uow = new UnitOfWork(new WorkContext()))
                {
                    uow.BlogCategory.Add(blogCategory);
                    uow.Complete();
                    return View();
                }
            }
            return View();
        }

        [HttpGet, Route("blog-category/edit/{id}")]
        public ActionResult Edit(int id)
        {
            ViewBag.News = "active";

            using (var uow = new UnitOfWork(new WorkContext()))
            {
                var category = uow.BlogCategory.Get(id);

                return View(category);
            }
        }

        [HttpPost, Route("blog-category/edit")]
        public ActionResult Edit(BlogCategoryRequest model)
        {
            if (ModelState.IsValid)
            {
                using (var uow = new UnitOfWork(new WorkContext()))
                {
                    var category = uow.BlogCategory.Get(model.Id);

                    blogCategoryRepository.ConvertToModel(ref category, model);

                    category.ModeifiedDate = DateTime.UtcNow;

                    uow.Complete();
                    return View();
                }
            }
            return View();
        }

        [HttpPost, Route("blog-category/active")]
        public ActionResult Active(int id)
        {
            using (var uow = new UnitOfWork(new WorkContext()))
            {
                var category = uow.BlogCategory.Get(id);

                category.IsActive = !category.IsActive;
                uow.Complete();
                return Json(new
                {
                    Status = true,
                    Message = string.Empty
                });
            }
        }
        
        [HttpPost, Route("blog-category/delete")]
        public ActionResult Delete(int id)
        {
            using (var uow = new UnitOfWork(new WorkContext()))
            {
                var category = uow.BlogCategory.Get(id);

                uow.BlogCategory.Remove(category);
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

