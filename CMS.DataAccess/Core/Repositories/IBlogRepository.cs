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

        IEnumerable<BlogResponse> Paging(int pageIndex, int pageSize, out int totalRecord, Expression<Func<Blog, bool>> predicate);

        IEnumerable<BlogResponse> GetByTop(int top, Expression<Func<Blog, bool>> predicate);

        IEnumerable<BlogResponse> GetTagByBlogId(int blogId);

        void Add(BlogRequest model, string tags);

        void Update(BlogRequest model, string tags);
    }
}
