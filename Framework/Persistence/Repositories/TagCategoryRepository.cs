﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Framework.Core.Domain;
using Framework.Core.Repositories;

namespace Framework.Persistence.Repositories
{
    public class TagCategoryRepository : BaseRepository<TagCategory>, ITagCategoryRepository
    {
        public TagCategoryRepository(WorkContext context) : base(context)
        {

        }

        public IEnumerable<Tag> Paging(int pageIndex, int pageSize, out int totalRecord, Expression<Func<Tag, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tag> GetByTop(int top, Expression<Func<Tag, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}