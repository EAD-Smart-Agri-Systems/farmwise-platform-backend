using FarmManagement.Modules.Farm.Application.DTOs;

namespace FarmManagement.Modules.Farm.Application.Interfaces;

public interface IFarmReadRepository
{
    Task<FarmDto?> GetFarmByIdAsync(Guid farmId);
}

// namespace FarmManagement.Modules.Farm.Application.Interfaces;

// public interface IFarmReadRepository
// {
//     Task<FarmDto?> GetFarmByIdAsync(Guid farmId);
// }
