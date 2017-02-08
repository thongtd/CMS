using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CMS.DataAccess.Core.Domain;

namespace CMS.DataAccess.Core.Repositories
{
    public interface ISiteSettingRepository:IBaseRepository<SiteSetting>
    {
        IEnumerable<SiteSetting> Paging(int pageIndex, int pageSize, out int totalRecord, Expression<Func<SiteSetting, bool>> predicate);
    }
}
