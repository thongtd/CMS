using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using CMS.Dashboard.Models;
using CMS.DataAccess.Core.Domain;
using CMS.DataAccess.Core.Linqkit;
using CMS.DataAccess.Core.Repositories;
using CMS.DataAccess.Persistence;
using CMS.DataAccess.Persistence.Repositories;
using MvcConnerstore.Collections;

namespace CMS.Dashboard.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository _productRepository = new ProductRepository(new WorkContext());

        public ActionResult Index()
        {
            ViewBag.Title = "Trang chủ";

            var predicate = PredicateBuilder.Create<Product>(s => s.IsActive);
            var products = _productRepository.GetByTop(6, predicate);

            return View(products);
        }
        
        [Route("danh-sach-san-pham/{page}")]
        public ActionResult ProductCategory(string page)
        {
            int totalRows = 0;
            ViewBag.Title = "Danh sách sản phẩm";

            var predicate = PredicateBuilder.Create<Product>(s => s.IsActive);

            var products =  _productRepository.Paging(PagedExtention.TryGetPageIndex(page), 9, out totalRows, predicate);

            return View(products);
        }

        [Route("sap-pham/{slug}")]
        public ActionResult ProductDetail(string slug)
        {
            var product = _productRepository.GetBySlug(slug);

            ViewBag.Title = product.Name;

            var predicate = PredicateBuilder.Create<Product>(s => s.IsActive && s.ProductCategoryId == product.ProductCategoryId);
            var relatedProducts = _productRepository.GetByTop(3, predicate).ToList();

            var productDetailResponse = new ProductDetailModel
            {
                ProductResponse = product,
                RelatedProducts = relatedProducts
            };
            return View(productDetailResponse);
        }

        [Route("tin-tuc/{slug}")]
        public ActionResult BlogCategory()
        {
            return View();
        }
    }
}