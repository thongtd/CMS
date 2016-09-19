using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Framework.Core.Domain;
using Framework.Core.Repositories;

namespace Framework.Persistence.Repositories
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
