using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Services.Description;
using CMS.DataAccess.Core.Domain;
using CMS.DataAccess.Core.Linqkit;
using CMS.DataAccess.Models;
using CMS.DataAccess.Persistence;
using MvcConnerstore.Collections;

namespace CMS.Dashboard.Controllers
{
    public class BlogCategoryController : Controller
    {
        public ActionResult Gets()
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

        public ActionResult Index(string pageIndex)
        {
            ViewBag.News = "active";

            return View();
        }

        public ActionResult Create()
        {
            using (var uow = new UnitOfWork(new WorkContext()))
            {
                var predicate = PredicateBuilder.Create<BlogCategory>(s => s.Order > 0);

                var maxOrderNum = uow.BlogCategory.Find(predicate).OrderByDescending(s => s.Order).Select(s => s.Order).FirstOrDefault();
                ViewBag.OrderNum = maxOrderNum + 1;
                return View();
            }
        }

        [HttpPost]
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

        public ActionResult Edit(int id)
        {
            using (var uow = new UnitOfWork(new WorkContext()))
            {
                var category = uow.BlogCategory.Get(id);

                return View(category);
            }
        }

        [HttpPost]
        public ActionResult Edit(BlogCategoryRequest model)
        {
            if (ModelState.IsValid)
            {
                using (var uow = new UnitOfWork(new WorkContext()))
                {
                    var category = uow.BlogCategory.Get(model.Id);

                    category = (BlogCategory)model;
                    category.ModeifiedDate = DateTime.UtcNow;

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

        [HttpPost]
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
