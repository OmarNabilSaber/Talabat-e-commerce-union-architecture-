using Dev.Talabat.Domain.Common;
using Dev.Talabat.Domain.Contracts;
using Dev.Talabat.Infrastructure.persistence.Data;
using Dev.Talabat.Infrastructure.persistence.Repositories;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Dev.Talabat.Infrastructure.persistence.UnitOfWork
{
    internal class UnitOfWork : IUnitOfWork
    {
        private ConcurrentDictionary<string, object> _repositories;
        private readonly StoreContext _dbcontext;
        public UnitOfWork(StoreContext dbcontext)
        {
            _dbcontext = dbcontext;
            _repositories = new();
        }
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
            where TEntity : BaseAuditableEntity<TKey>
            where TKey : IEquatable<TKey>
        {
            return (IGenericRepository<TEntity, TKey>) _repositories.GetOrAdd(typeof(TEntity).Name, new GenericRepository<TEntity, TKey>(_dbcontext));
        }
        public async Task<int> CompleteAsync() =>  await _dbcontext.SaveChangesAsync();
        
        public async ValueTask DisposeAsync() => await _dbcontext.DisposeAsync();
    }
    
}
