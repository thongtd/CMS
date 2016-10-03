using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CMS.DataAccess.Core.Domain;
using CMS.DataAccess.Core.Repositories;

namespace CMS.DataAccess.Persistence.Repositories
{
    public class VideoCategoryRepository : BaseRepository<VideoCategory>, IVideoCategoryRepository
    {
        public VideoCategoryRepository(WorkContext context) : base(context)
        {

        }

        public IEnumerable<VideoCategory> Paging(int pageIndex, int pageSize, out int totalRecord, Expression<Func<VideoCategory, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VideoCategory> GetByTop(int top, Expression<Func<VideoCategory, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
