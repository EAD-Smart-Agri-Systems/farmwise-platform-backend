using FarmManagement.Modules.Farm.Domain.Factories;
using FarmManagement.Modules.Farm.Domain.Repositories;
using FarmManagement.Modules.Farm.Domain.ValueObjects;

namespace FarmManagement.Modules.Farm.Application.Commands.RegisterFarm;

public sealed class RegisterFarmCommandHandler
{
    private readonly IFarmRepository _farmRepository;

    public RegisterFarmCommandHandler(IFarmRepository farmRepository)
    {
        _farmRepository = farmRepository;
    }

    public async Task Handle(RegisterFarmCommand command, CancellationToken cancellationToken = default)
    {
        var name = new FarmName(command.Name);
        var location = new Location(command.Latitude, command.Longitude);

        var farm = FarmFactory.Create(name, location);

        await _farmRepository.AddAsync(farm);
        await _farmRepository.SaveChangesAsync(cancellationToken);
    }
}

// using FarmManagement.Modules.Farm.Domain.Factories;
// using FarmManagement.Modules.Farm.Domain.Repositories;
// using FarmManagement.Modules.Farm.Domain.ValueObjects;

// namespace FarmManagement.Modules.Farm.Application.Commands.RegisterFarm;

// public sealed class RegisterFarmCommandHandler
// {
//     private readonly IFarmRepository _farmRepository;

//     public RegisterFarmCommandHandler(IFarmRepository farmRepository)
//     {
//         _farmRepository = farmRepository;
//     }

//     public async Task Handle(RegisterFarmCommand command)
//     {
//         var farmName = new FarmName(command.Name);
//         var location = new Location(command.Country, command.Region);

//         var farm = FarmFactory.Create(farmName, location);

//         await _farmRepository.AddAsync(farm);
//     }
// }
