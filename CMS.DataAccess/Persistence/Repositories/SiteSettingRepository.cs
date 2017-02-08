using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using CMS.DataAccess.Core.Domain;
using CMS.DataAccess.Core.Repositories;
using MvcConnerstore.Collections;

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

        public IEnumerable<SiteSetting> Paging(int pageIndex, int pageSize, out int totalRecord, Expression<Func<SiteSetting, bool>> predicate)
        {
            using (var unitOfWork = new UnitOfWork(new WorkContext()))
            {
                var records = unitOfWork.SiteSetting.Find(predicate).ToList();

                if (records.Any())
                {
                    totalRecord = records.Count();
                    records = records.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                    return new PagedList<SiteSetting>(records, totalRecord);
                }
            }

            totalRecord = 0;
            return new PagedList<SiteSetting>(null, 0);
        }
    }
}
