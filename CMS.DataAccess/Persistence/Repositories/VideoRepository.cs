﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CMS.DataAccess.Core.Domain;
using CMS.DataAccess.Core.Repositories;

namespace CMS.DataAccess.Persistence.Repositories
{
    public class VideoRepository : BaseRepository<Video>, IVideoRepository
    {
        public VideoRepository(WorkContext context) : base(context)
        {

        }

        public IEnumerable<Video> Paging(int pageIndex, int pageSize, out int totalRecord, Expression<Func<Video, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Video> GetByTop(int top, Expression<Func<Video, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
