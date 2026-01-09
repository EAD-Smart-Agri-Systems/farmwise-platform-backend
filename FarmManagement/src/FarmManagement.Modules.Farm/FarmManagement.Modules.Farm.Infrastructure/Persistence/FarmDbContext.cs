using Microsoft.EntityFrameworkCore;
using FarmManagement.Infrastructure.Shared.Persistence.EF;
using FarmAggregate = FarmManagement.Modules.Farm.Domain.Aggregates.FarmAggregate.Farm;
using FieldAggregate = FarmManagement.Modules.Farm.Domain.Aggregates.FarmAggregate.Field;

namespace FarmManagement.Modules.Farm.Infrastructure.Persistence;

public sealed class FarmDbContext : BaseDbContext
{
    public DbSet<FarmAggregate> Farms => Set<FarmAggregate>();
    public DbSet<FieldAggregate> Fields => Set<FieldAggregate>();

    public FarmDbContext(DbContextOptions<FarmDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FarmDbContext).Assembly);
    }
}

// using Microsoft.EntityFrameworkCore;
// using FarmManagement.Modules.Farm.Domain.Aggregates.FarmAggregate;

// namespace FarmManagement.Modules.Farm.Infrastructure.Persistence;

// public sealed class FarmDbContext : DbContext
// {
//     public DbSet<Farm> Farms => Set<Farm>();
//     public DbSet<Field> Fields => Set<Field>();

//     public FarmDbContext(DbContextOptions<FarmDbContext> options)
//         : base(options) { }

//     protected override void OnModelCreating(ModelBuilder modelBuilder)
//     {
//         modelBuilder.ApplyConfigurationsFromAssembly(typeof(FarmDbContext).Assembly);
//     }
// }
// using Microsoft.EntityFrameworkCore;
// using FarmAggregate = FarmManagement.Modules.Farm.Domain.Aggregates.FarmAggregate.Farm;
// using FieldAggregate = FarmManagement.Modules.Farm.Domain.Aggregates.FarmAggregate.Field;

// namespace FarmManagement.Modules.Farm.Infrastructure.Persistence;

// public sealed class FarmDbContext : DbContext
// {
//     public DbSet<FarmAggregate> Farms => Set<FarmAggregate>();
//     public DbSet<FieldAggregate> Fields => Set<FieldAggregate>();

//     public FarmDbContext(DbContextOptions<FarmDbContext> options)
//         : base(options) { }

//     protected override void OnModelCreating(ModelBuilder modelBuilder)
//     {
//         modelBuilder.ApplyConfigurationsFromAssembly(typeof(FarmDbContext).Assembly);
//     }
// }

// using Microsoft.EntityFrameworkCore;
// using FarmManagement.Modules.Farm.Domain.Aggregates.FarmAggregate;

// namespace FarmManagement.Modules.Farm.Infrastructure.Persistence;

// public sealed class FarmDbContext : DbContext
// {
//     public DbSet<Farm> Farms => Set<Farm>();
//     public DbSet<Field> Fields => Set<Field>();

//     public FarmDbContext(DbContextOptions<FarmDbContext> options)
//         : base(options) { }

//     protected override void OnModelCreating(ModelBuilder modelBuilder)
//     {
//         modelBuilder.ApplyConfigurationsFromAssembly(typeof(FarmDbContext).Assembly);
//     }
// }
