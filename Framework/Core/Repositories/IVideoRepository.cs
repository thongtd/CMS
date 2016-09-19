using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Framework.Core.Domain;

namespace Framework.Core.Repositories
{
    public interface IVideoRepository : IBaseRepository<Video>
    {
        IEnumerable<Video> Paging(int pageIndex, int pageSize, out int totalRecord, Expression<Func<Video, bool>> predicate);

        IEnumerable<Video> GetByTop(int top, Expression<Func<Video, bool>> predicate);
    }
}
