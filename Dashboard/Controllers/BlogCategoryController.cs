using System.Linq;
using CMS.DataAccess.Core.Domain;
using CMS.DataAccess.Core.Linqkit;
using CMS.DataAccess.Core.Repositories;
using CMS.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using MvcConnerstore.Collections;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

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

        public IActionResult Create()
        {
            ViewBag.News = "active";
            var predicate = PredicateBuilder.Create<BlogCategory>(s => s.Order > 0);

            var maxOrderNum = _blogCategoryRepository.Find(predicate).OrderByDescending(s=>s.Order).Select(s=>s.Order).FirstOrDefault();
            ViewBag.OrderNum = int.Parse(maxOrderNum.ToString()) + 1;
            return View();
        }

        [HttpPost]
        public IActionResult Create(BlogCategoryRequest model)
        {
            if (ModelState.IsValid)
            {
                var blogCategory = (BlogCategory) model;
                
            }

            return View("Index");
        }
    }
}
