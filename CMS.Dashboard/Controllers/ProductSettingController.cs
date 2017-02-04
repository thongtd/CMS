using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CMS.Dashboard.Filters;
using CMS.Dashboard.Models;
using CMS.Dashboard.ViewModels;
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
    [DashboardActionFilter(IndexPageTile = "Cấu hình sản phẩm", EditPageTile = "Sửa thông tin cấu hình", CreatePageTile = "Thêm mới cấu hình")]
    public class ProductSettingController : Controller
    {
        private readonly IProductSettingRepository productSettingRepository = new ProductSettingRepository(new WorkContext());

        [HttpPost, Route("product-setting/gets")]
        public async Task<ActionResult> Gets(KendoGridFilterModel filter)
        {
            int totalRecord = 0;

            var task = await Task.Run(() => productSettingRepository.Paging(PagedExtention.TryGetPageIndex(filter.page.ToString()), filter.pageSize, out totalRecord, null));

            var response = task.Select(s => new
            {
                s.Id,
                s.Name,
                s.Type
            });

            return Json(new
            {
                data = response,
                total = totalRecord
            });
        }

        [Route("product-setting")]
        public ActionResult Index()
        {
            ViewBag.SiteSetting = "active";

            return View();
        }

        [HttpGet, Route("product-setting/create")]
        public ActionResult Create()
        {
            ViewBag.SiteSetting = "active";

            return View();
        }

        [HttpPost, Route("product-setting/create")]
        public ActionResult Create(ProductSettingRequest model)
        {
            if (!ModelState.IsValid) return View();

            var productSetting = (ProductSetting)model;

            using (var uow = new UnitOfWork(new WorkContext()))
            {
                uow.ProductSetting.Add(productSetting);
                uow.Complete();
                return View();
            }
        }
    }
}