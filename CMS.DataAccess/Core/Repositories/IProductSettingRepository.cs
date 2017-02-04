using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CMS.DataAccess.Core.Domain;
using CMS.DataAccess.Models;

namespace CMS.DataAccess.Core.Repositories
{
    public interface IProductSettingRepository : IBaseRepository<ProductSetting>
    {
        void ConvertToModel(ref Product product, ProductCategoryRequest model);

        IEnumerable<ProductSettingResponse> Paging(int pageIndex, int pageSize, out int totalRecord, Expression<Func<ProductSetting, bool>> predicate);
    }
}
