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
        
        Task<IEnumerable<Tag>> GetTagsByObjectIdentityId(Guid identityId, string objectName);

        Task AddTagToObject(string[] arrTags, string objectName, Guid objectIdentityId, bool isEdit);

        Task RemoveOldTags(string objectName, Guid objectIdentityId);
    }
}
