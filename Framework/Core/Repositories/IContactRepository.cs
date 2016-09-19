using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Framework.Core.Domain;

namespace Framework.Core.Repositories
{
    public interface IContactRepository : IBaseRepository<Contact>
    {
        IEnumerable<Contact> Paging(int pageIndex, int pageSize, out int totalRecord, Expression<Func<Contact, bool>> predicate);

        IEnumerable<Contact> GetByTop(int top, Expression<Func<Contact, bool>> predicate);
    }
}
