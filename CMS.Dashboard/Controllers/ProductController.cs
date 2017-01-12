using System.Threading.Tasks;
using System.Web.Mvc;
using CMS.Dashboard.Filters;
using CMS.DataAccess.Core.Extension;
using CMS.DataAccess.Core.Repositories;
using CMS.DataAccess.Models;
using CMS.DataAccess.Persistence;
using CMS.DataAccess.Persistence.Repositories;
using MvcConnerstore.Collections;

namespace CMS.Dashboard.Controllers
{
    [RoutePrefix("admin")]
    [DashboardActionFilter(IndexPageTile = "Danh sách sản phẩm", EditPageTile = "Sửa thông tin sản phẩm", CreatePageTile = "Thêm mới sản phẩm")]
    public class ProductController : Controller
    {
        private readonly IProductRepository productRepository = new ProductRepository(new WorkContext());
        private readonly ITagRepository tagRepository = new TagRepository(new WorkContext());
        
        [Route("product/gets")]
        public async Task<ActionResult> Gets()
        {
            using (var uow = new UnitOfWork(new WorkContext()))
            {
                int total;

                var task = Task.Run(() => uow.Product.Paging(PagedExtention.TryGetPageIndex("1"), int.MaxValue, out total, null));

                return Json(await task, JsonRequestBehavior.AllowGet);
            }
        }

        [Route("product")]
        public ActionResult Index(string pageIndex)
        {
            ViewBag.Product = "active";

            return View();
        }

        [Route("product/create")]
        public ActionResult Create()
        {
            ViewBag.Product = "active";

            return View();
        }

        [HttpPost, ValidateInput(false), Route("product/create")]
        public ActionResult Create(ProductRequest model, FormCollection frmCollect)
        {
            if (!ModelState.IsValid)
                return View(model);

            using (var uow = new UnitOfWork(new WorkContext()))
            {
                uow.Product.Add(model);

                return RedirectToAction("Index");
            }
        }

        [Route("product/edit/{id}")]
        public ActionResult Edit(int id)
        {
            ViewBag.Product = "active";

            using (var uow = new UnitOfWork(new WorkContext()))
            {
                var product = uow.Product.Get(id);

                var lstTags = tagRepository.GetTagsForObject(product.IdentityCode, Constants.ObjectName.Blog, Constants.ObjectName.BlogIdenityCode);
                ViewBag.ActiveTags = lstTags.HtmlTag;
                ViewBag.HiddenTags = lstTags.TagValue;

                return View(product);
            }
        }

        [HttpPost, ValidateInput(false), Route("product/edit")]
        public ActionResult Edit(ProductRequest model, FormCollection frmCollect)
        {
            if (ModelState.IsValid)
            {
                var tags = frmCollect["hidden-tags"];

                productRepository.Update(model, tags);
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost, Route("product/active")]
        public ActionResult Active(int id)
        {
            using (var uow = new UnitOfWork(new WorkContext()))
            {
                var product = uow.Product.Get(id);

                product.IsActive = !product.IsActive;
                uow.Complete();
                return Json(new
                {
                    Status = true,
                    Message = string.Empty
                });
            }
        }

        [HttpPost, Route("product/delete")]
        public ActionResult Delete(int id)
        {
            using (var uow = new UnitOfWork(new WorkContext()))
            {
                var product = uow.Product.Get(id);

                uow.Product.Remove(product);
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