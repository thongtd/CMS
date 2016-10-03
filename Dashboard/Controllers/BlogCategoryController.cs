using CMS.DataAccess.Core.Repositories;
using CMS.DataAccess.Persistence.Repositories;
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

        public ActionResult Index(string pageIndex)
        {
            ViewBag.News = "active";

            int total = 0;
            var blogCategorys = _blogCategoryRepository.Paging(PagedExtention.TryGetPageIndex(pageIndex), PagedExtention.PageSize, out total, null);

            //ViewBag.Paging = businessReturnObject.PagingHtml;
            return View(blogCategorys);
        }
    }
}
