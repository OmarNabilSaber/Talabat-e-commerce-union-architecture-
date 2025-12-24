using Dev.Talabat.Application.abstruction;
using Dev.Talabat.Domain.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.Talabat.Infrastructure.persistence.Data.Interceptors
{
    internal class CustomSaveChangesInterceptor : SaveChangesInterceptor
    {
        private ILoggedInUserService _loggedInUserService { get; }

        public CustomSaveChangesInterceptor(ILoggedInUserService loggedInUserService)
        {
            _loggedInUserService = loggedInUserService;
        }
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateEntities(eventData.Context);
            return base.SavingChanges(eventData, result);
        }
        public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            UpdateEntities(eventData.Context);
            return base.SavedChangesAsync(eventData, result, cancellationToken);
        }
         
        private void UpdateEntities(DbContext? dbContext)
        {
            if (dbContext is null)
                return;
            foreach (var entry in dbContext.ChangeTracker.Entries<BaseAuditableEntity<int>>()
                .Where(e => e.State is EntityState.Added or EntityState.Modified))
            {
                if (entry.Entity is BaseAuditableEntity<int> auditableEntity)
                {
                    var now = DateTime.UtcNow;
                    var user = _loggedInUserService.UserId; 
                    if (entry.State == EntityState.Added)
                    {
                        auditableEntity.CreatedOn = now;
                        auditableEntity.CreatedBy = user;
                    }
                    auditableEntity.LastModifiedOn = now;
                    auditableEntity.LastModifiedBy = user;
                }
            } 
            
        }
    }
}
