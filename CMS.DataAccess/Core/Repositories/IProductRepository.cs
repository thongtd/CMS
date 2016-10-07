using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CMS.DataAccess.Core.Domain;
using CMS.DataAccess.Models;

namespace CMS.DataAccess.Core.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        void ConvertToModel(ref Product blog, ProductRequest model);

        IEnumerable<ProductResponse> Paging(int pageIndex, int pageSize, out int totalRecord, Expression<Func<Product, bool>> predicate);

        IEnumerable<ProductResponse> GetByTop(int top, Expression<Func<Product, bool>> predicate);

        IEnumerable<ProductResponse> GetTagByBlogId(int blogId);

        void Add(ProductRequest model, string tags);

        void Update(ProductRequest model, string tags);
    }
}
