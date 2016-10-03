using CMS.DataAccess.Core.Repositories;
using System;

namespace CMS.DataAccess.Core
{
    public interface IUnitOfWork : IDependency, IDisposable
    {
        int Complete();
    }
}
