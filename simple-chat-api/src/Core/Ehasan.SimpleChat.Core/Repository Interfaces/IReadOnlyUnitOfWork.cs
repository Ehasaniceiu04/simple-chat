using System;
using System.Collections.Generic;
using System.Text;

namespace Ehasan.SimpleChat.Core.Repository_Interfaces
{
    public interface IReadOnlyUnitOfWork : IDisposable
    {
        IRepository<TEntity> Repository<TEntity>() where TEntity : class;
    }
}
