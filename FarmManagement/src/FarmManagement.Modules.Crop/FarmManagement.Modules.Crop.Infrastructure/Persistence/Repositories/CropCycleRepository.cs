// FarmManagement.Modules.Crop/Crop.Infrastructure/Persistence/Repositories/CropCycleRepository.cs
using FarmManagement.Modules.Crop.Domain.Aggregates;
using FarmManagement.Modules.Crop.Domain.ValueObjects;
using FarmManagement.Modules.Crop.Application.Interfaces;
using System.Collections.Concurrent;

namespace FarmManagement.Modules.Crop.Infrastructure.Persistence.Repositories
{
    public class CropCycleRepository : ICropCycleRepository
    {
        // Option A: Use Guid as key (if cropCycle.Id is Guid)
        private static readonly ConcurrentDictionary<Guid, CropCycle> _store = new();
        
        public Task<CropCycle?> GetByIdAsync(CropCycleId id)
        {
            // Convert CropCycleId to Guid for lookup
            _store.TryGetValue(id.Value, out var cropCycle);
            return Task.FromResult(cropCycle);
        }
        
        public Task AddAsync(CropCycle cropCycle)
        {
            // If cropCycle.Id is Guid, use it directly
            // If cropCycle.Id is CropCycleId, use cropCycle.Id.Value
            _store.TryAdd(cropCycle.Id.Value, cropCycle); // or just cropCycle.Id if it's Guid
            return Task.CompletedTask;
        }
        
        public Task SaveChangesAsync()
        {
            return Task.CompletedTask;
        }
    }
}