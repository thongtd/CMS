using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CMS.DataAccess.Core.Domain;
using CMS.DataAccess.Models;

namespace CMS.DataAccess.Core.Repositories
{
    public interface IBlogRepository : IBaseRepository<Blog>
    {
        void ConvertToModel(ref Blog blog, BlogRequest model);

        IEnumerable<Blog> Paging(int pageIndex, int pageSize, out int totalRecord, Expression<Func<Blog, bool>> predicate);

        IEnumerable<Blog> GetByTop(int top, Expression<Func<Blog, bool>> predicate);

        IEnumerable<Blog> GetTagByBlogId(int blogId);
    }
}
