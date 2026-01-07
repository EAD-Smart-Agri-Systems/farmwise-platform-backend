using Crop.Domain.Aggregates;
using Crop.Domain.ValueObjects;

namespace Crop.Application.Interfaces;

public interface ICropCycleRepository
{
    Task AddAsync(CropCycle cropCycle);
    Task<CropCycle?> GetByIdAsync(CropCycleId id);
    Task SaveChangesAsync();
}
