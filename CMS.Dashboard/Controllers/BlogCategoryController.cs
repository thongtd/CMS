using System;
using System.Linq;
using System.Web.Mvc;
using CMS.Dashboard.Filters;
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
    [DashboardActionFilter(IndexPageTile = "Danh sách nhóm bài viết", EditPageTile = "Sửa thông tin nhóm bài viết", CreatePageTile = "Thêm mới nhóm bài viết")]
    public class BlogCategoryController : Controller
    {
        private readonly IBlogCategoryRepository blogCategoryRepository = new BlogCategoryRepository(new WorkContext());
        
        [Route("blog-category/get")]
        public ActionResult Get()
        {
            using (var uow = new UnitOfWork(new WorkContext()))
            {
                int total;
                var blogCategorys = uow.BlogCategory.Paging(PagedExtention.TryGetPageIndex("1"), int.MaxValue, out total, null);

                if (!blogCategorys.Any()) return Json(blogCategorys, JsonRequestBehavior.AllowGet);

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

                return Json(blogCategorys, JsonRequestBehavior.AllowGet);
            }
        }

        [Route("blog-category")]
        public ActionResult Index(string pageIndex)
        {
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
            if (!ModelState.IsValid) return View();

            var blogCategory = (BlogCategory)model;
            blogCategory.CreatedDate = DateTime.UtcNow;

            using (var uow = new UnitOfWork(new WorkContext()))
            {
                uow.BlogCategory.Add(blogCategory);
                uow.Complete();
                return View();
            }
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
            if (!ModelState.IsValid) return View();

            using (var uow = new UnitOfWork(new WorkContext()))
            {
                var category = uow.BlogCategory.Get(model.Id);

                blogCategoryRepository.ConvertToModel(ref category, model);

                category.ModeifiedDate = DateTime.UtcNow;

                uow.Complete();
                return View();
            }
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

