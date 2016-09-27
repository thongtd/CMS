using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Framework.Core.Domain;

namespace Framework.Core.Repositories
{
    public interface IGalleryRepository : IBaseRepository<Gallery>
    {
        IEnumerable<Gallery> Paging(int pageIndex, int pageSize, out int totalRecord, Expression<Func<Gallery, bool>> predicate);

        IEnumerable<Gallery> GetByTop(int top, Expression<Func<Gallery, bool>> predicate);
    }
}
