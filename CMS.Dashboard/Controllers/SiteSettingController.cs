using System;
using System.Linq;
using System.Web.Mvc;
using CMS.Dashboard.Filters;
using CMS.Dashboard.Models;
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
    [DashboardActionFilter(IndexPageTile = "Danh sách nhóm bài viết", EditPageTile = "Sửa thông tin nhóm bài viết", CreatePageTile = "Thêm mới nhóm bài viết")]
    public class SiteSettingController : Controller
    {
        private readonly ISiteSettingRepository siteSettingRepository = new SiteSettingRepository(new WorkContext());

        [Route("site-setting/get")]
        public ActionResult Get(KendoGridFilterModel filter)
        {
            using (var uow = new UnitOfWork(new WorkContext()))
            {
                int total;
                var siteSettings = uow.SiteSetting.Paging(PagedExtention.TryGetPageIndex(filter.page.ToString()), filter.pageSize, out total, null);

                if (!siteSettings.Any()) return Json(siteSettings, JsonRequestBehavior.AllowGet);

                return Json(siteSettings, JsonRequestBehavior.AllowGet);
            }
        }

        [Route("site-setting")]
        public ActionResult Index(string pageIndex)
        {
            ViewBag.News = "active";

            return View();
        }

        [HttpGet, Route("site-setting/create")]
        public ActionResult Create()
        {
            ViewBag.News = "active";

            return View();
        }

        [HttpPost, Route("site-setting/create")]
        public ActionResult Create(SiteSettingRequest model)
        {
            if (!ModelState.IsValid) return View();
            
            using (var uow = new UnitOfWork(new WorkContext()))
            {
                uow.SiteSetting.Add(model);
                uow.Complete();
                return View();
            }
        }

        [HttpGet, Route("site-setting/edit/{id}")]
        public ActionResult Edit(int id)
        {
            ViewBag.News = "active";

            return View(siteSettingRepository.Get(id));
        }

        [HttpPost, Route("site-setting/edit")]
        public ActionResult Edit(SiteSettingRequest model)
        {
            if (!ModelState.IsValid) return View();

            using (var uow = new UnitOfWork(new WorkContext()))
            {
                var siteSetting = uow.SiteSetting.Get(model.Id);

                siteSetting.Value = model.Value;
                siteSetting.Group = model.Group;

                uow.Complete();
                return View();
            }
        }
        
        [HttpPost, Route("site-setting/delete")]
        public ActionResult Delete(int id)
        {
            using (var uow = new UnitOfWork(new WorkContext()))
            {
                var siteSetting = uow.SiteSetting.Get(id);

                uow.SiteSetting.Remove(siteSetting);
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