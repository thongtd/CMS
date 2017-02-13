using System.Web.Mvc;
using Autofac;
using CMS.DataAccess.Core.Extension;
using MvcConnerstore.Cache.Repositories;

namespace CMS.Dashboard.Areas.FlatShop.Controllers
{
    public class BaseController : Controller
    {
        private readonly IComponentContext componentContext;

        public BaseController(IComponentContext componentContext)
        {
            this.componentContext = componentContext;

            GetWebSeoSettings();
        }

        private void GetWebSeoSettings()
        {
            var cache = componentContext.Resolve<ICacheRepository<string>>();
            ViewBag.Title = cache.Gets(Constants.SiteSetting.WebSeo + "." + Constants.SiteSetting.Title);
            ViewBag.Description = cache.Gets(Constants.SiteSetting.WebSeo + "." + Constants.SiteSetting.Description);
            ViewBag.Keywords = cache.Gets(Constants.SiteSetting.WebSeo + "." + Constants.SiteSetting.Keywords);
        }
    }
}