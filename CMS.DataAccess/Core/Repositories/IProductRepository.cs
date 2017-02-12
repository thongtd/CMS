using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CMS.DataAccess.Core.Domain;
using CMS.DataAccess.Models;

namespace CMS.DataAccess.Core.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        void ConvertToModel(ref Product product, ProductRequest model);

        IEnumerable<ProductResponse> Paging(int pageIndex, int pageSize, out int totalRecord, Expression<Func<Product, bool>> predicate);

        IEnumerable<ProductResponse> GetByTop(int top, Expression<Func<Product, bool>> predicate);

        ProductResponse GetTagByProductId(int id);

        ProductResponse GetBySlug(string slug);

        Task Add(ProductRequest model);

        Task Update(ProductRequest model);
    }
}
