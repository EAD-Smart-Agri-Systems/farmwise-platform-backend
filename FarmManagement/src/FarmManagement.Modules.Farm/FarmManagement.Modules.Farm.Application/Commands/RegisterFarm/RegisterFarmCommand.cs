using MediatR;

namespace FarmManagement.Modules.Farm.Application.Commands.RegisterFarm;

public sealed record RegisterFarmCommand(
    string Name,
    decimal Latitude,
    decimal Longitude
) : IRequest<Guid>;

// namespace FarmManagement.Modules.Farm.Application.Commands.RegisterFarm;

// public sealed record RegisterFarmCommand(
//     string Name,
//     string Country,
//     string Region
// );
