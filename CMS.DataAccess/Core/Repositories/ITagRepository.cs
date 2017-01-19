using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CMS.DataAccess.Core.Domain;

namespace CMS.DataAccess.Core.Repositories
{
    public interface ITagRepository : IBaseRepository<Tag>
    {
        IEnumerable<Tag> Paging(int pageIndex, int pageSize, out int totalRecord, Expression<Func<Tag, bool>> predicate);

        IEnumerable<Tag> GetByTop(int top, Expression<Func<Tag, bool>> predicate);

        IEnumerable<Tag> GetTagByBlogId(int blogId);

        Task<IEnumerable<Tag>> GetTagsOfObject(Guid objectValue, string objectName, string objectProperty);

        Task AddTagToObject(string[] arrTags, string objectName, string objectProperty, Guid objectIdentityId, bool isEdit);
    }
}
