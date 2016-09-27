using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Framework.Core.Domain;

namespace Framework.Core.Repositories
{
    public interface IVideoCategoryRepository : IBaseRepository<VideoCategory>
    {
        IEnumerable<VideoCategory> Paging(int pageIndex, int pageSize, out int totalRecord, Expression<Func<VideoCategory, bool>> predicate);

        IEnumerable<VideoCategory> GetByTop(int top, Expression<Func<VideoCategory, bool>> predicate);
    }
}
