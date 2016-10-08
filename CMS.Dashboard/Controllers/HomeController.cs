using System.Threading.Tasks;
using System.Web.Mvc;
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
            int totalRows = 0;
            var predicate = PredicateBuilder.Create<Product>(s => s.IsActive);
            var products = _productRepository.Paging(1, 6, out totalRows, predicate);

            return View(products);
        }
        
        [Route("danh-sach-san-pham/{page}")]
        public async Task<ActionResult> Product(string page)
        {
            int totalRows = 0;
            var predicate = PredicateBuilder.Create<Product>(s => s.IsActive);

            var products =  _productRepository.Paging(PagedExtention.TryGetPageIndex(page), 9, out totalRows, predicate);

            return View(products);
        }

        [Route("sap-pham/{slug}")]
        public async Task<ActionResult> ProductDetail(string slug)
        {
            var product = _productRepository.GetBySlug(slug);

            return View(product);
        }
    }
}