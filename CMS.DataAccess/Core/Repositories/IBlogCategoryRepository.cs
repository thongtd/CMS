using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CMS.DataAccess.Core.Domain;

namespace CMS.DataAccess.Core.Repositories
{
    public interface IBlogCategoryRepository : IBaseRepository<BlogCategory>
    {
        IEnumerable<BlogCategory> Paging(int pageIndex, int pageSize, out int totalRecord, Expression<Func<BlogCategory,bool>> predicate);

        IEnumerable<BlogCategory> GetByTop(int top, Expression<Func<BlogCategory, bool>> predicate);
    }
}
