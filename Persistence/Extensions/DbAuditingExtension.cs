using System.Linq.Expressions;
using Domain.Entity.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Persistence.Extensions;

public static class DbAuditingExtension
{
    public static void ApplyDeletionQueryFilterToEntities(this ModelBuilder builder)
    {
        // Apply global query filter to all entities that inherit from AuditableEntity
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            if (!typeof(IAuditableEntity).IsAssignableFrom(entityType.ClrType)) 
                continue;
            
            // Get the 'DeletedDate' property
            var deletedDateProperty = entityType.FindProperty(nameof(IAuditableEntity.DeletedDate));
                
            // Apply the global filter where DeletedDate is null
            builder.Entity(entityType.ClrType).HasQueryFilter(CreateIsNotDeletedExpression(entityType.ClrType, deletedDateProperty!));
        }
    }
    
    // Helper method to create the query filter expression dynamically
    private static LambdaExpression CreateIsNotDeletedExpression(Type entityType, IMutableProperty deletedDateProperty)
    {
        // Create the lambda expression: entity => entity.DeletedDate == null
        var parameter = Expression.Parameter(entityType, "entity");
        var property = Expression.Property(parameter, deletedDateProperty.PropertyInfo!);
        var condition = Expression.Equal(property, Expression.Constant(null));

        return Expression.Lambda(condition, parameter);
    }
}