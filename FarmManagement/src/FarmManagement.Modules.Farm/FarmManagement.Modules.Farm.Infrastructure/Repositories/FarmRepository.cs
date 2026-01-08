
using Microsoft.EntityFrameworkCore;
using FarmAggregate = FarmManagement.Modules.Farm.Domain.Aggregates.FarmAggregate.Farm;
using FarmManagement.Modules.Farm.Domain.Repositories;
using FarmManagement.SharedKernel.Domain;
using FarmManagement.Modules.Farm.Infrastructure.Persistence;

namespace FarmManagement.Modules.Farm.Infrastructure.Repositories;

internal sealed class FarmRepository : IFarmRepository
{
    private readonly FarmDbContext _context;

    public FarmRepository(FarmDbContext context)
    {
        _context = context;
    }

    public async Task<FarmAggregate?> GetByIdAsync(FarmId id)
    {
        return await _context.Farms
            .Include(f => f.Fields)
            .FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task AddAsync(FarmAggregate farm)
    {
        await _context.Farms.AddAsync(farm);
    }
}
// using Microsoft.EntityFrameworkCore;
// using FarmManagement.Modules.Farm.Domain.Aggregates.FarmAggregate;
// using FarmManagement.Modules.Farm.Domain.Repositories;
// using FarmManagement.SharedKernel.Domain;
// using FarmManagement.Modules.Farm.Infrastructure.Persistence;

// namespace FarmManagement.Modules.Farm.Infrastructure.Repositories;

// internal sealed class FarmRepository : IFarmRepository
// {
//     private readonly FarmDbContext _context;

//     public FarmRepository(FarmDbContext context)
//     {
//         _context = context;
//     }

//     public async Task<Farm?> GetByIdAsync(FarmId id)
//     {
//         return await _context.Farms
//             .Include(f => f.Fields)
//             .FirstOrDefaultAsync(f => f.Id == id);
//     }

//     public async Task AddAsync(Farm farm)
//     {
//         await _context.Farms.AddAsync(farm);
//     }
// }
