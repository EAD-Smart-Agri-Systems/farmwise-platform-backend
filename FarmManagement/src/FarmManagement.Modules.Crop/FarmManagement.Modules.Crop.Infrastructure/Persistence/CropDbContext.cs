using Microsoft.EntityFrameworkCore;
using FarmManagement.Infrastructure.Shared.Persistence.EF;
using FarmManagement.Modules.Crop.Domain.Aggregates;

namespace FarmManagement.Modules.Crop.Infrastructure.Persistence;

public class CropDbContext : BaseDbContext
{
    public DbSet<CropCycle> CropCycles => Set<CropCycle>();

    public CropDbContext(DbContextOptions<CropDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CropDbContext).Assembly);
    }
}
// using Microsoft.EntityFrameworkCore;
// using FarmManagement.Modules.Crop.Domain.Aggregates;

// namespace FarmManagement.Modules.Crop.Infrastructure.Persistence;

// public class CropDbContext : DbContext
// {
//     public DbSet<CropCycle> CropCycles => Set<CropCycle>();

//     public CropDbContext(DbContextOptions<CropDbContext> options)
//         : base(options)
//     {
//     }

//     protected override void OnModelCreating(ModelBuilder modelBuilder)
//     {
//         modelBuilder.ApplyConfigurationsFromAssembly(typeof(CropDbContext).Assembly);
//     }
// }
