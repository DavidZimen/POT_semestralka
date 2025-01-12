using Domain.Entity.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Security.Service;

namespace Persistence.Interceptors;

/// <summary>
/// Interceptor before saving changes to database for <b>AuditableEntity</b>.
/// Sets dates based on current state of the entity.
/// </summary>
internal sealed class AuditableEntityInterceptor : SaveChangesInterceptor
{
    private readonly IAuthService _authService;

    public AuditableEntityInterceptor(IAuthService authService)
    {
        _authService = authService;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateAuditableEntities(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        UpdateAuditableEntities(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateAuditableEntities(DbContext? dbContext)
    {
        if (dbContext is null)
            return;

        var now = DateTime.UtcNow;
        var entities = dbContext.ChangeTracker.Entries<IAuditableEntity>().ToList();

        foreach (var entry in entities)
        {
            switch (entry.State)
            {
                case EntityState.Added: 
                    SetCurrentPropertyValue(entry, nameof(IAuditableEntity.CreatedDate), now);
                    break;
                case EntityState.Modified:
                    SetCurrentPropertyValue(entry, nameof(IAuditableEntity.ModifiedDate), now);
                    break;
                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    SetCurrentPropertyValue(entry, nameof(IAuditableEntity.DeletedDate), now);
                    break;
            }
            entry.Entity.LastModifiedBy = _authService.GetCurrentUserId() ?? string.Empty;
        }

        static void SetCurrentPropertyValue(EntityEntry entry, string propertyName, DateTime utcNow) 
            => entry.Property(propertyName).CurrentValue = utcNow;
    }
}