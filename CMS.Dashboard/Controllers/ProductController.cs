using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using CMS.Dashboard.Filters;
using CMS.Dashboard.Models;
using CMS.Dashboard.ViewModels;
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

        [HttpPost, Route("product/gets")]
        public async Task<ActionResult> Gets(KendoGridFilterModel filter)
        {
            int totalRecord = 0;

            var task = await Task.Run(() => productRepository.Paging(PagedExtention.TryGetPageIndex(filter.page.ToString()), filter.pageSize, out totalRecord, null));

            var response = task.Select(s => new
            {
                s.Name,
                s.Price,
                s.CreatedDate,
                s.Id,
                ProductCategoryName = s.ProductCategory.Name,
                s.NumberOfProduct,
                s.SellingOfProduct,
                s.IsActive
            });

            return Json(new
            {
                data = response,
                total = totalRecord
            });
        }

        [Route("product")]
        public ActionResult Index()
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
        public async Task<ActionResult> Create(ProductRequest model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await productRepository.Add(model);

            return RedirectToAction("Index");
        }

        [Route("product/edit/{id}")]
        public async Task<ActionResult> Edit(int id)
        {
            ViewBag.Product = "active";

            using (var uow = new UnitOfWork(new WorkContext()))
            {
                var product = uow.Product.Get(id);
                var tags = await tagRepository.GetTagsByObjectIdentityId(product.IdentityCode, Constants.ObjectName.Product);

                var editProductModel = new EditProductModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Slug = product.Slug,
                    ProductCategoryId = product.ProductCategoryId,
                    Thumbnail = product.Thumbnail,
                    Images = product.Images,
                    CreatedDate = product.CreatedDate,
                    ModeifiedDate = product.ModeifiedDate,
                    SubContent = product.SubContent,
                    BodyContent = product.BodyContent,
                    Target = product.Target,
                    View = product.View,
                    IdentityCode = product.IdentityCode,
                    PinToTop = product.PinToTop,
                    Title = product.Title,
                    Description = product.Description,
                    Keyword = product.Keyword,
                    CultureCode = product.CultureCode,
                    Price = product.Price,
                    Discount = product.Discount,
                    DiscountIsPercent = product.DiscountIsPercent,
                    IsActive = product.IsActive,
                    Tags = tags
                };

                return View(editProductModel);
            }
        }

        [HttpPost, ValidateInput(false), Route("product/edit")]
        public async Task<ActionResult> Edit(ProductRequest model)
        {
            if (ModelState.IsValid)
            {
                await productRepository.Update(model);
                return RedirectToAction("Index");
            }
            return View(model);
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