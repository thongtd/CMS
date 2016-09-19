using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Framework.Core.Domain;
using Framework.Core.Repositories;

namespace Framework.Persistence.Repositories
{
    public class GalleryCategoryRepository : BaseRepository<GalleryCategory>, IGalleryCategoryRepository
    {
        public GalleryCategoryRepository(WorkContext context) : base(context)
        {

        }

        public IEnumerable<GalleryCategory> Paging(int pageIndex, int pageSize, out int totalRecord, Expression<Func<GalleryCategory, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GalleryCategory> GetByTop(int top, Expression<Func<GalleryCategory, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
