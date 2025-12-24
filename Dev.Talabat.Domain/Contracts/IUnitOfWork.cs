using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.Talabat.Domain.Contracts
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
            where TEntity : BaseAuditableEntity<TKey>
            where TKey : IEquatable<TKey>; 

        Task<int> CompleteAsync();
    }
}
