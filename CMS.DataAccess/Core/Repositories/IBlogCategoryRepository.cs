﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CMS.DataAccess.Core.Domain;
using CMS.DataAccess.Core.Extension;
using CMS.DataAccess.Models;
using MvcConnerstore.Collections;

namespace CMS.DataAccess.Core.Repositories
{
    public interface IBlogCategoryRepository : IBaseRepository<BlogCategory>
    {
        BlogCategory ConvertToModel(ref BlogCategory blogCategory, BlogCategoryRequest model);

        IPagedList<BlogCategory> Paging(int pageIndex, int pageSize, out int totalRecord, Expression<Func<BlogCategory,bool>> predicate);

        IEnumerable<BlogCategory> GetByTop(int top, Expression<Func<BlogCategory, bool>> predicate);

        IEnumerable<SelectedList> BlogCategoryTree();
    }
}
