using System;
using System.Linq;
using CMS.DataAccess.Core.Domain;
using CMS.DataAccess.Core.Linqkit;
using CMS.DataAccess.Core.Repositories;
using CMS.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using MvcConnerstore.Collections;

namespace Dashboard.Controllers
{
    public class BlogCategoryController : Controller
    {
        private readonly IBlogCategoryRepository _blogCategoryRepository;

        public BlogCategoryController(IBlogCategoryRepository blogCategoryRepository)
        {
            _blogCategoryRepository = blogCategoryRepository;
        }

        public IActionResult Index(string pageIndex)
        {
            ViewBag.News = "active";

            int total = 0;
            var blogCategorys = _blogCategoryRepository.Paging(PagedExtention.TryGetPageIndex(pageIndex), PagedExtention.PageSize, out total, null);

            return View(blogCategorys);
        }

        [Route("/get")]
        public IActionResult Gets()
        {
            int total = 0;
            var blogCategorys = _blogCategoryRepository.Paging(PagedExtention.TryGetPageIndex("1"), PagedExtention.PageSize, out total, null);

            return Json(blogCategorys);
        }

        public IActionResult Create()
        {
            var predicate = PredicateBuilder.Create<BlogCategory>(s => s.Order > 0);

            var maxOrderNum = _blogCategoryRepository.Find(predicate).OrderByDescending(s=>s.Order).Select(s=>s.Order).FirstOrDefault();
            ViewBag.OrderNum = maxOrderNum + 1;
            return View();
        }

        [HttpPost]
        public IActionResult Create(BlogCategoryRequest model)
        {
            if (ModelState.IsValid)
            {
                var blogCategory = (BlogCategory) model;
                blogCategory.CreatedDate = DateTime.UtcNow;
            }

            return View("Index");
        }
    }
}
