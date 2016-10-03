using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CMS.DataAccess.Core.Domain;
using CMS.DataAccess.Core.Repositories;
using MvcConnerstore.Collections;

namespace CMS.DataAccess.Persistence.Repositories
{
    public class BlogCategoryRepository : BaseRepository<BlogCategory>, IBlogCategoryRepository
    {
        public BlogCategoryRepository(WorkContext context) : base(context)
        {
        }

        public IPagedList<BlogCategory> Paging(int pageIndex, int pageSize, out int totalRecord, Expression<Func<BlogCategory, bool>> predicate)
        {
            using (var unitOfWork = new UnitOfWork(new WorkContext()))
            {
                var records = unitOfWork.BlogCategory.Find(predicate).ToList();

                if (records.Any())
                {
                    totalRecord = records.Count();
                    records = records.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                    return new PagedList<BlogCategory>(records, totalRecord);
                }
            }

            totalRecord = 0;
            return new PagedList<BlogCategory>(null, 0);
        }

        public IEnumerable<BlogCategory> GetByTop(int top, Expression<Func<BlogCategory, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}