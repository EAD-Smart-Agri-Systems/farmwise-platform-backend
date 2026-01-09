using FarmManagement.Modules.Advisory.Domain.Aggregates;
using FarmManagement.Infrastructure.Shared.Persistence.EF;
using Microsoft.EntityFrameworkCore;

namespace FarmManagement.Modules.Advisory.Infrastructure.Persistence;

public class AdvisoryDbContext : BaseDbContext
{
    public DbSet<AdvisoryReport> AdvisoryReports => Set<AdvisoryReport>();

    public AdvisoryDbContext(DbContextOptions<AdvisoryDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AdvisoryDbContext).Assembly);
    }
}

// using FarmManagement.Modules.Advisory.Domain.Aggregates;
// using Microsoft.EntityFrameworkCore;

// namespace FarmManagement.Modules.Advisory.Infrastructure.Persistence;

// public class AdvisoryDbContext : DbContext
// {
//     public DbSet<AdvisoryReport> AdvisoryReports => Set<AdvisoryReport>();

//     public AdvisoryDbContext(DbContextOptions<AdvisoryDbContext> options)
//         : base(options)
//     {
//     }

//     protected override void OnModelCreating(ModelBuilder modelBuilder)
//     {
//         modelBuilder.ApplyConfigurationsFromAssembly(typeof(AdvisoryDbContext).Assembly);
//     }
// }
