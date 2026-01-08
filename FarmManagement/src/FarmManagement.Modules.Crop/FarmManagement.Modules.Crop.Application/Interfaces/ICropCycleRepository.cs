// Crop.Application/Interfaces/ICropCycleRepository.cs
using FarmManagement.Modules.Crop.Domain.Aggregates;
using FarmManagement.Modules.Crop.Domain.ValueObjects;

namespace FarmManagement.Modules.Crop.Application.Interfaces
{
    public interface ICropCycleRepository
    {
        Task AddAsync(CropCycle cropCycle);
        Task<CropCycle?> GetByIdAsync(CropCycleId id);
        Task SaveChangesAsync();
    }
}