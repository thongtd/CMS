using Autofac;
using CMS.DataAccess.Core.Domain;
using CMS.DataAccess.Core.Repositories;
using CMS.DataAccess.Persistence;
using CMS.DataAccess.Persistence.Repositories;
using MvcConnerstore.Cache.Repositories;

namespace CMS.Dashboard.Code
{
    public class CacheManager
    {
        private readonly IComponentContext componentContext;
        private static readonly ISiteSettingRepository siteSettingRepository = new SiteSettingRepository(new WorkContext());

        public CacheManager(IComponentContext componentContext)
        {
            this.componentContext = componentContext;
        }


        public void BuildToCache()
        {
            var siteSettingCache = componentContext.Resolve<ICacheRepository<SiteSetting>>();

            var siteSettings = siteSettingRepository.FindAll();

            foreach (var setting in siteSettings)
            {
                siteSettingCache.Add(setting.Group + "." + setting.Key, setting.Value);
            }
        }
    }
}