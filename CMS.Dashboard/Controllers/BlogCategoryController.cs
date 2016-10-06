using System;
using System.Linq;
using System.Web.Mvc;
using CMS.DataAccess.Core.Domain;
using CMS.DataAccess.Core.Linqkit;
using CMS.DataAccess.Models;
using CMS.DataAccess.Persistence;
using MvcConnerstore.Collections;

namespace CMS.Dashboard.Controllers
{
    public class BlogCategoryController : Controller
    {

        #region Json Function
        public ActionResult Gets()
        {
            using (var unitOfWork = new UnitOfWork(new WorkContext()))
            {
                int total = 0;
                var blogCategorys = unitOfWork.BlogCategory.Paging(PagedExtention.TryGetPageIndex("1"), int.MaxValue, out total, null);

                return Json(blogCategorys, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        public ActionResult Index(string pageIndex)
        {
            ViewBag.News = "active";

            return View();
        }

        public ActionResult Create()
        {
            using (var unitOfWork = new UnitOfWork(new WorkContext()))
            {
                var predicate = PredicateBuilder.Create<BlogCategory>(s => s.Order > 0);

                var maxOrderNum = unitOfWork.BlogCategory.Find(predicate).OrderByDescending(s => s.Order).Select(s => s.Order).FirstOrDefault();
                ViewBag.OrderNum = maxOrderNum + 1;
                return View();
            }
        }

        [HttpPost]
        public ActionResult Create(BlogCategoryRequest model)
        {
            if (ModelState.IsValid)
            {
                var blogCategory = (BlogCategory) model;
                blogCategory.CreatedDate = DateTime.UtcNow;

                using (var unitOfWork = new UnitOfWork(new WorkContext()))
                {
                    unitOfWork.BlogCategory.Add(blogCategory);
                    unitOfWork.Complete();
                    return View();
                }
            }
            return View();
        }
    }
}
