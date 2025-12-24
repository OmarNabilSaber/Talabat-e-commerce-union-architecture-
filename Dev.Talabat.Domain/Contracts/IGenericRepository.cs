using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.Talabat.Domain.Contracts
{
    public interface IGenericRepository<TEntity, TKey> 
        where TEntity : BaseAuditableEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public Task<IEnumerable<TEntity>> GetAllAsync(bool withTracking = false);
        public Task<TEntity?> GetAsync(TKey id);
        public Task AddAsync(TEntity entity);
        public void Update(TEntity entity);
        public void Delete(TEntity entity);
    }
}
