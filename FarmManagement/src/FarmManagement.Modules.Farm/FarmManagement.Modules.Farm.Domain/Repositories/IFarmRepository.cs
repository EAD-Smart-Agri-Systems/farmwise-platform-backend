using FarmManagement.Modules.Farm.Domain.Aggregates.FarmAggregate;
using FarmManagement.SharedKernel.Domain;

namespace FarmManagement.Modules.Farm.Domain.Repositories;

public interface IFarmRepository
{
    Task<Aggregates.FarmAggregate.Farm?> GetByIdAsync(FarmId id);
    Task AddAsync(Aggregates.FarmAggregate.Farm farm);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}

// using FarmManagement.Modules.Farm.Domain.Aggregates.FarmAggregate;
// using FarmManagement.Modules.Farm.Domain.ValueObjects;
// using FarmManagement.SharedKernel.Domain;

// namespace FarmManagement.Modules.Farm.Domain.Repositories;

// public interface IFarmRepository
// {
//     Task<Aggregates.FarmAggregate.Farm?> GetByIdAsync(FarmId id);
//     Task AddAsync(Aggregates.FarmAggregate.Farm farm);
// }

// using FarmManagement.Modules.Farm.Domain.Aggregates.FarmAggregate;
// using FarmManagement.Modules.Farm.Domain.ValueObjects;

// namespace FarmManagement.Modules.Farm.Domain.Repositories;

// public interface IFarmRepository
// {
//     Task<Farm?> GetByIdAsync(FarmId id);
//     Task AddAsync(Farm farm);
// }
