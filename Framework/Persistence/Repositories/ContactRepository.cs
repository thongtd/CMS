using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Framework.Core.Domain;
using Framework.Core.Repositories;

namespace Framework.Persistence.Repositories
{
    public class ContactRepository : BaseRepository<Contact>, IContactRepository
    {
        public ContactRepository(WorkContext context) : base(context)
        {

        }

        public IEnumerable<Contact> Paging(int pageIndex, int pageSize, out int totalRecord, Expression<Func<Contact, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Contact> GetByTop(int top, Expression<Func<Contact, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
