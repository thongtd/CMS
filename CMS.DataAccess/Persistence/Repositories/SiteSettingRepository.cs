using System.Data.Entity;
using CMS.DataAccess.Core.Domain;
using CMS.DataAccess.Core.Repositories;

namespace CMS.DataAccess.Persistence.Repositories
{
    public class SiteSettingRepository : BaseRepository<SiteSetting>, ISiteSettingRepository
    {
        public SiteSettingRepository(DbContext context) : base(context)
        {
        }

        public WorkContext WorkContext
        {
            get { return Context as WorkContext; }
        }
    }
}
