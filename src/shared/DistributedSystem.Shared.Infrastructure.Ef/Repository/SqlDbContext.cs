using DistributedSystem.Shared.Core.Abstractions;
using DistributedSystem.Shared.Infrastructure.Ef.Entities;
using DistributedSystem.Shared.Utility.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedSystem.Shared.Infrastructure.Ef.Repository;
public abstract class SqlDbContext : AppDbContext
{
    public SqlDbContext(DbContextOptions options, IMediator? mediator, IClock clock)
    : base(options, mediator, clock)
    {
    }

    protected override void ApplyConfigurations(ModelBuilder modelBuilder)
    {
        // All SQL entities have EF configs that inherit from BaseEntityConfiguration<>.
        modelBuilder.ApplyConfigurationsFromAssembly(
            GetEntityConfigurationAssembly(),
            t => t.InheritsFromGenericParent(typeof(BaseEntityConfiguration<>)));
    }
}
