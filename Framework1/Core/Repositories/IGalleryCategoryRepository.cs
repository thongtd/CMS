using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Framework.Core.Domain;

namespace Framework.Core.Repositories
{
    public interface IGalleryCategoryRepository : IBaseRepository<GalleryCategory>
    {
        IEnumerable<GalleryCategory> Paging(int pageIndex, int pageSize, out int totalRecord, Expression<Func<GalleryCategory, bool>> predicate);

        IEnumerable<GalleryCategory> GetByTop(int top, Expression<Func<GalleryCategory, bool>> predicate);
    }
}
