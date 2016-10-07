using System;
using System.Linq;
using System.Web.Mvc;
using CMS.DataAccess.Core.Domain;
using CMS.DataAccess.Core.Linqkit;
using CMS.DataAccess.Core.Repositories;
using CMS.DataAccess.Models;
using CMS.DataAccess.Persistence;
using CMS.DataAccess.Persistence.Repositories;
using MvcConnerstore.Collections;

namespace CMS.Dashboard.Controllers
{
    [RoutePrefix("Dashboard")]
    public class ProductCategoryController : Controller
    {
        private readonly IProductCategoryRepository _productCategoryRepository = new ProductCategoryRepository(new WorkContext());

        [Route("ProductCategory/Gets")]
        public ActionResult Gets()
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

        [Route("ProductCategory/Index")]
        public ActionResult Index(string pageIndex)
        {
            ViewBag.ProductCategory = "active";

            return View();
        }

        [HttpGet, Route("ProductCategory/Create")]
        public ActionResult Create()
        {
            ViewBag.ProductCategory = "active";

            using (var uow = new UnitOfWork(new WorkContext()))
            {
                var predicate = PredicateBuilder.Create<ProductCategory>(s => s.Order > 0);

                var maxOrderNum = uow.ProductCategory.Find(predicate).OrderByDescending(s => s.Order).Select(s => s.Order).FirstOrDefault();
                ViewBag.OrderNum = maxOrderNum + 1;
                return View();
            }
        }

        [HttpPost, Route("ProductCategory/Create")]
        public ActionResult Create(ProductCategoryRequest model)
        {
            if (ModelState.IsValid)
            {
                var productCategory = (ProductCategory)model;
                productCategory.CreatedDate = DateTime.UtcNow;

                using (var uow = new UnitOfWork(new WorkContext()))
                {
                    uow.ProductCategory.Add(productCategory);
                    uow.Complete();
                    return View();
                }
            }
            return View();
        }

        [HttpGet, Route("ProductCategory/Edit/{id}")]
        public ActionResult Edit(int id)
        {
            ViewBag.ProductCategory = "active";

            using (var uow = new UnitOfWork(new WorkContext()))
            {
                var category = uow.ProductCategory.Get(id);

                return View(category);
            }
        }

        [HttpPost, Route("ProductCategory/Edit")]
        public ActionResult Edit(ProductCategoryRequest model)
        {
            if (ModelState.IsValid)
            {
                using (var uow = new UnitOfWork(new WorkContext()))
                {
                    var category = uow.ProductCategory.Get(model.Id);

                    _productCategoryRepository.ConvertToModel(ref category, model);

                    category.ModeifiedDate = DateTime.UtcNow;

                    uow.Complete();
                    return View();
                }
            }
            return View();
        }

        [HttpPost, Route("ProductCategory/Active")]
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
        
        [HttpPost, Route("ProductCategory/Delete")]
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

