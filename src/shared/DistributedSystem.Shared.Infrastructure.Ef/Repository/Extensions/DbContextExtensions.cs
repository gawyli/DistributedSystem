using DistributedSystem.Shared.Core.Abstractions;
using DistributedSystem.Shared.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DistributedSystem.Shared.Infrastructure.Ef.Repository;
public static class DbContextExtensions
{
    /*
     * Due to Entity Framework Core's current limitations with inheritance,
     * we can't directly use DbSet<T> when T is a superclass and the entity instance is a subclass.
     * Thus, we use reflection to dynamically obtain the DbSet for the runtime type of the entity.
     * TODO: This workaround is in place until Microsoft (re)introduces a way to get a DbSet based on a specified type.
     */

    public static dynamic GetDbSetForEntity<T>(this DbContext context, T entity)
        where T : BaseEntity, IAggregateRoot
    {
        if (entity.GetType() == typeof(T))
        {
            // Try using compile-time type.
            return context.Set<T>();
        }

        // Try using runtime type.
        return context.GetDbSetForType(entity.GetType());
    }

    public static dynamic GetDbSetForType(this DbContext context, Type entityType)
    {
        // Obtains the "Set" method of the DbContext
        var method = typeof(DbContext).GetMethods().Single(p =>
            p is { Name: nameof(DbContext.Set), ContainsGenericParameters: true }
            && !p.GetParameters().Any());

        // Makes the method a generic of type 'entityType'
        method = method.MakeGenericMethod(entityType);

        // Invokes to obtain DbSet<entityType>
        return method.Invoke(context, null)!;
    }
}
