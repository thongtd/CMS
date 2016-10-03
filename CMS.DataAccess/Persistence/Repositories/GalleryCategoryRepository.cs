using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CMS.DataAccess.Core.Domain;
using CMS.DataAccess.Core.Repositories;

namespace CMS.DataAccess.Persistence.Repositories
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
