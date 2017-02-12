using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using CMS.Dashboard.Code;
using CMS.DataAccess.Core.Repositories;
using CMS.DataAccess.Models;
using CMS.DataAccess.Persistence;
using CMS.DataAccess.Persistence.Repositories;
using MvcConnerstore.Cache.Persistance;
using MvcConnerstore.Cache.Repositories;

namespace CMS.Dashboard
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            builder.RegisterGeneric(typeof(MemoryCacheRepository<>))
                .As(typeof(ICacheRepository<>))
                .InstancePerDependency();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<CacheManager>().SingleInstance();

            var container = builder.Build();

            var cacheManager = container.Resolve<CacheManager>();
            cacheManager.BuildToCache();
        }
    }
}
