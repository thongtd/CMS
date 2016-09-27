using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Framework.Core.Domain;

namespace Framework.Core.Repositories
{
    public interface ITagRepository : IBaseRepository<Tag>
    {
        IEnumerable<Tag> Paging(int pageIndex, int pageSize, out int totalRecord, Expression<Func<Tag, bool>> predicate);

        IEnumerable<Tag> GetByTop(int top, Expression<Func<Tag, bool>> predicate);

        IEnumerable<Tag> GetTagByBlogId(int blogId);
    }
}
