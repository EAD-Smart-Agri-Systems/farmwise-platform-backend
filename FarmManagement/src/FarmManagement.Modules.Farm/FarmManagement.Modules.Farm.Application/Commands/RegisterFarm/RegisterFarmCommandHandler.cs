using MediatR;
using FarmManagement.Modules.Farm.Domain.Factories;
using FarmManagement.Modules.Farm.Domain.Repositories;
using FarmManagement.Modules.Farm.Domain.ValueObjects;

namespace FarmManagement.Modules.Farm.Application.Commands.RegisterFarm;

public sealed class RegisterFarmCommandHandler : IRequestHandler<RegisterFarmCommand, Guid>
{
    private readonly IFarmRepository _farmRepository;

    public RegisterFarmCommandHandler(IFarmRepository farmRepository)
    {
        _farmRepository = farmRepository;
    }

    public async Task<Guid> Handle(RegisterFarmCommand request, CancellationToken cancellationToken)
    {
        var name = new FarmName(request.Name);
        var location = new Location(request.Latitude, request.Longitude);

        var farm = FarmFactory.Create(name, location);

        await _farmRepository.AddAsync(farm);
        await _farmRepository.SaveChangesAsync(cancellationToken);

        return farm.Id.Value;
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
