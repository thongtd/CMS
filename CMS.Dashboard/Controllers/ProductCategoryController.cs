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
    [DashboardActionFilter(IndexPageTile = "Danh sách nhóm sản phẩm", EditPageTile = "Sửa thông tin nhóm sản phẩm", CreatePageTile = "Thêm mới nhóm sản phẩm")]
    public abstract class ProductCategoryController : Controller
    {
        private readonly IProductCategoryRepository productCategoryRepository = new ProductCategoryRepository(new WorkContext());
        
        [Route("product-category/get")]
        public ActionResult Get()
        {
            using (var uow = new UnitOfWork(new WorkContext()))
            {
                var total = 0;
                var productCategory = uow.ProductCategory.Paging(PagedExtention.TryGetPageIndex("1"), int.MaxValue, out total, null);

                if (productCategory.Any())
                {
                    foreach (var category in productCategory)
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

                return Json(productCategory, JsonRequestBehavior.AllowGet);
            }
        }

        [Route("product-category")]
        public virtual ActionResult Index(string pageIndex)
        {
            ViewBag.Product = "active";

            return View();
        }

        [HttpGet, Route("product-category/create")]
        public ActionResult Create()
        {
            ViewBag.Product = "active";

            using (var uow = new UnitOfWork(new WorkContext()))
            {
                var predicate = PredicateBuilder.Create<ProductCategory>(s => s.Order > 0);

                var maxOrderNum = uow.ProductCategory.Find(predicate).OrderByDescending(s => s.Order).Select(s => s.Order).FirstOrDefault();
                ViewBag.OrderNum = maxOrderNum + 1;
                return View();
            }
        }

        [HttpPost, Route("product-category/create")]
        public ActionResult Create(ProductCategoryRequest model)
        {
            if (!ModelState.IsValid) return View();

            var productCategory = (ProductCategory)model;
            productCategory.CreatedDate = DateTime.UtcNow;

            using (var uow = new UnitOfWork(new WorkContext()))
            {
                uow.ProductCategory.Add(productCategory);
                uow.Complete();
                return View();
            }
        }

        [HttpGet, Route("product-category/edit/{id}")]
        public ActionResult Edit(int id)
        {
            ViewBag.Product = "active";

            using (var uow = new UnitOfWork(new WorkContext()))
            {
                var category = uow.ProductCategory.Get(id);

                return View(category);
            }
        }

        [HttpPost, Route("product-category/edit")]
        public ActionResult Edit(ProductCategoryRequest model)
        {
            if (!ModelState.IsValid) return View();

            using (var uow = new UnitOfWork(new WorkContext()))
            {
                var category = uow.ProductCategory.Get(model.Id);

                productCategoryRepository.ConvertToModel(ref category, model);

                category.ModeifiedDate = DateTime.UtcNow;

                uow.Complete();
                return View();
            }
        }

        [HttpPost, Route("product-category/active")]
        public ActionResult Active(int id)
        {
            using (var uow = new UnitOfWork(new WorkContext()))
            {
                var category = uow.ProductCategory.Get(id);

                category.IsActive = !category.IsActive;
                uow.Complete();
                return Json(new
                {
                    Status = true,
                    Message = string.Empty
                });
            }
        }
        
        [HttpPost, Route("product-category/delete")]
        public ActionResult Delete(int id)
        {
            using (var uow = new UnitOfWork(new WorkContext()))
            {
                var category = uow.ProductCategory.Get(id);

                uow.ProductCategory.Remove(category);
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

