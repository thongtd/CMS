using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CMS.DataAccess.Core.Domain;
using CMS.DataAccess.Core.Extension;
using CMS.DataAccess.Models;
using MvcConnerstore.Collections;

namespace CMS.DataAccess.Core.Repositories
{
    public interface IProductCategoryRepository : IBaseRepository<ProductCategory>
    {
        ProductCategory ConvertToModel(ref ProductCategory productCategory, ProductCategoryRequest model);

        IPagedList<ProductCategory> Paging(int pageIndex, int pageSize, out int totalRecord, Expression<Func<ProductCategory, bool>> predicate);

        IEnumerable<ProductCategory> GetByTop(int top, Expression<Func<ProductCategory, bool>> predicate);

        IEnumerable<SelectedList> BlogCategoryTree();
    }
}
