using Framework.Core.Repositories;
using System;

namespace Framework.Core
{
    public interface IUnitOfWork : IDependency, IDisposable
    {
        IStudentRepository Student { get; }

        int Complete();
    }
}
