using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CMS.DataAccess.Core.Domain;
using CMS.DataAccess.Models;

namespace CMS.DataAccess.Core.Repositories
{
    public interface ITagRepository : IBaseRepository<Tag>
    {
        IEnumerable<Tag> Paging(int pageIndex, int pageSize, out int totalRecord, Expression<Func<Tag, bool>> predicate);

        IEnumerable<Tag> GetByTop(int top, Expression<Func<Tag, bool>> predicate);

        IEnumerable<Tag> GetTagByBlogId(int blogId);

        TagHtmlResponse GetTagsForObject(Guid objectValue, string objectName, string objectProperty);
    }
}
