using DistributedSystem.Shared.Core.Abstractions;
using DistributedSystem.Shared.Core.Entities;
using DistributedSystem.Shared.Infrastructure.Ef.Repository.Conventers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DistributedSystem.Shared.Infrastructure.Ef.Repository;
public abstract class AppDbContext : DbContext
{
    private readonly IMediator? _mediator;
    private readonly IClock _clock;

    protected AppDbContext(DbContextOptions options, IMediator? mediator, IClock clock)
        : base(options)
    {
        _mediator = mediator;
        _clock = clock;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ApplyConfigurations(modelBuilder);
        modelBuilder.RegisterEnumConverters();
    }

    protected abstract void ApplyConfigurations(ModelBuilder modelBuilder);

    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
        // Register any conventions here such as EF type conversions
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        UpdateEntityDates();
        int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        // dispatch events only if save was successful
        var entitiesWithEvents = ChangeTracker.Entries<BaseEntity>()
            .Select(e => e.Entity)
            .Where(e => e.Events.Any())
            .ToArray();

        foreach (var entity in entitiesWithEvents)
        {
            var events = entity.Events.ToArray();
            entity.Events.Clear();
            if (_mediator is null)
            {
                continue;
            }
            foreach (var domainEvent in events)
            {
                await _mediator.Publish(domainEvent, cancellationToken).ConfigureAwait(false);
            }
        }

        return result;
    }

    public override int SaveChanges()
    {
        return SaveChangesAsync().GetAwaiter().GetResult();
    }

    protected abstract Assembly GetEntityConfigurationAssembly();

    private void UpdateEntityDates()
    {
        foreach (var entry in ChangeTracker.Entries<IHasCreated>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property(nameof(IHasCreated.Created)).CurrentValue = _clock.CurrentDateTime;
            }
        }

        foreach (var entry in ChangeTracker.Entries<IHasUpdated>())
        {
            if (entry.State == EntityState.Modified)
            {
                entry.Property(nameof(IHasUpdated.Updated)).CurrentValue = _clock.CurrentDateTime;
            }
        }
    }
}
