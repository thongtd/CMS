using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Framework.Core.Domain;
using Framework.Core.Repositories;

namespace Framework.Persistence.Repositories
{
    public class BlogRepository : BaseRepository<Blog>, IBlogRepository
    {
        public BlogRepository(WorkContext context) : base(context)
        {
        }

        public IEnumerable<Blog> Paging(int pageIndex, int pageSize, out int totalRecord, Expression<Func<Blog, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Blog> GetByTop(int top, Expression<Func<Blog, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Blog> GetTagByBlogId(int blogId)
        {
            throw new NotImplementedException();
        }
    }
}