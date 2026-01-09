using FarmManagement.Modules.Crop.Domain.Aggregates;
using FarmManagement.Modules.Crop.Domain.ValueObjects;
using FarmManagement.Modules.Crop.Application.Interfaces;
using FarmManagement.Modules.Crop.Infrastructure.Persistence;
using FarmManagement.Infrastructure.Shared.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace FarmManagement.Modules.Crop.Infrastructure.Persistence.Repositories
{
    public class CropCycleRepository : ICropCycleRepository
    {
        private readonly CropDbContext _context;
        
        public CropCycleRepository(CropDbContext context)
        {
            _context = context;
        }
        
        public async Task<CropCycle?> GetByIdAsync(CropCycleId id)
        {
            return await _context.CropCycles
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        
        public async Task AddAsync(CropCycle cropCycle)
        {
            await _context.CropCycles.AddAsync(cropCycle);
        }
        
        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesWithOutboxAsync(cancellationToken);
        }
    }
}

// // FarmManagement.Modules.Crop/Crop.Infrastructure/Persistence/Repositories/CropCycleRepository.cs
// using FarmManagement.Modules.Crop.Domain.Aggregates;
// using FarmManagement.Modules.Crop.Domain.ValueObjects;
// using FarmManagement.Modules.Crop.Application.Interfaces;
// using System.Collections.Concurrent;

// namespace FarmManagement.Modules.Crop.Infrastructure.Persistence.Repositories
// {
//     public class CropCycleRepository : ICropCycleRepository
//     {
//         // Option A: Use Guid as key (if cropCycle.Id is Guid)
//         private static readonly ConcurrentDictionary<Guid, CropCycle> _store = new();
        
//         public Task<CropCycle?> GetByIdAsync(CropCycleId id)
//         {
//             // Convert CropCycleId to Guid for lookup
//             _store.TryGetValue(id.Value, out var cropCycle);
//             return Task.FromResult(cropCycle);
//         }
        
//         public Task AddAsync(CropCycle cropCycle)
//         {
//             // If cropCycle.Id is Guid, use it directly
//             // If cropCycle.Id is CropCycleId, use cropCycle.Id.Value
//             _store.TryAdd(cropCycle.Id.Value, cropCycle); // or just cropCycle.Id if it's Guid
//             return Task.CompletedTask;
//         }
        
//         public Task SaveChangesAsync()
//         {
//             return Task.CompletedTask;
//         }
//     }
// }