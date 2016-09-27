using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Framework.Core.Domain;
using Framework.Core.Repositories;

namespace Framework.Persistence.Repositories
{
    public class GalleryRepository : BaseRepository<Gallery>, IGalleryRepository
    {
        public GalleryRepository(WorkContext context) : base(context)
        {

        }

        public IEnumerable<Gallery> Paging(int pageIndex, int pageSize, out int totalRecord, Expression<Func<Gallery, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Gallery> GetByTop(int top, Expression<Func<Gallery, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
