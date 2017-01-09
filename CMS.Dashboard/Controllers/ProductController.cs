using System.Collections.Generic;
using System.Web.Mvc;
using CMS.Dashboard.Code.Models;
using CMS.DataAccess.Core.Extension;
using CMS.DataAccess.Core.Repositories;
using CMS.DataAccess.Models;
using CMS.DataAccess.Persistence;
using CMS.DataAccess.Persistence.Repositories;
using MvcConnerstore.Collections;

namespace CMS.Dashboard.Controllers
{
    [RoutePrefix("admin")]
    public class ProductController : Controller
    {
        private const string IndexPageTile = "Danh sách nhóm sản phẩm";
        private const string EditPageTile = "Sửa thông tin nhóm sản phẩm";
        private const string CreatePageTile = "Thêm mới nhóm sản phẩm";

        private readonly IProductRepository productRepository = new ProductRepository(new WorkContext());
        private readonly ITagRepository tagRepository = new TagRepository(new WorkContext());

        private readonly IList<Breadcurmb> breadcurmbs = new List<Breadcurmb>();

        public ProductController()
        {
            breadcurmbs.Add(new Breadcurmb
            {
                ActionLink = "/",
                Lable = "Home"
            });

            breadcurmbs.Add(new Breadcurmb
            {
                ActionLink = "#",
                Lable = "Dashboard"
            });
        }

        [Route("product/gets")]
        public ActionResult Gets()
        {
            using (var uow = new UnitOfWork(new WorkContext()))
            {
                var total = 0;
                var product = uow.Product.Paging(PagedExtention.TryGetPageIndex("1"), int.MaxValue, out total, null);

                return Json(product, JsonRequestBehavior.AllowGet);
            }
        }

        [Route("product")]
        public ActionResult Index(string pageIndex)
        {
            breadcurmbs.Add(new Breadcurmb
            {
                ActionLink = Url.Action("Index"),
                Lable = IndexPageTile
            });

            ViewBag.Breadcurmbs = breadcurmbs;
            ViewBag.Title = IndexPageTile;
            ViewBag.Product = "active";

            return View();
        }

        [Route("product/create")]
        public ActionResult Create()
        {
            breadcurmbs.Add(new Breadcurmb
            {
                ActionLink = Url.Action("Index"),
                Lable = IndexPageTile
            });

            breadcurmbs.Add(new Breadcurmb
            {
                ActionLink = "#",
                Lable = CreatePageTile
            });

            ViewBag.Breadcurmbs = breadcurmbs;
            ViewBag.Title = CreatePageTile;
            ViewBag.Product = "active";

            return View();
        }

        [HttpPost, ValidateInput(false), Route("product/create")]
        public ActionResult Create(ProductRequest model, FormCollection frmCollect)
        {
            if (ModelState.IsValid)
                return View();

            using (var uow = new UnitOfWork(new WorkContext()))
            {
                uow.Product.Add(model);

                return RedirectToAction("Index");
            };
        }

        [Route("product/edit/{id}")]
        public ActionResult Edit(int id)
        {
            breadcurmbs.Add(new Breadcurmb
            {
                ActionLink = Url.Action("Index"),
                Lable = IndexPageTile
            });

            breadcurmbs.Add(new Breadcurmb
            {
                ActionLink = "#",
                Lable = EditPageTile
            });

            ViewBag.Breadcurmbs = breadcurmbs;
            ViewBag.Title = EditPageTile;
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