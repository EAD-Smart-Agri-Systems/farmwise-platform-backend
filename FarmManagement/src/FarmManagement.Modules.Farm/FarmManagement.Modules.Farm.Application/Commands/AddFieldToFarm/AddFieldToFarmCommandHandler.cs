using FarmManagement.Modules.Farm.Domain.Aggregates.FarmAggregate;
using FarmManagement.Modules.Farm.Domain.Repositories;
using FarmManagement.Modules.Farm.Domain.ValueObjects;

namespace FarmManagement.Modules.Farm.Application.Commands.AddFieldToFarm;

public sealed class AddFieldToFarmCommandHandler
{
    private readonly IFarmRepository _farmRepository;

    public AddFieldToFarmCommandHandler(IFarmRepository farmRepository)
    {
        _farmRepository = farmRepository;
    }

    public async Task Handle(AddFieldToFarmCommand command)
    {
        var farmId = FarmId.From(command.FarmId);

        var farm = await _farmRepository.GetByIdAsync(farmId);

        if (farm is null)
            throw new InvalidOperationException("Farm not found.");

        var field = new Field(FieldId.New(), command.FieldName);

        farm.AddField(field);
    }
}


// using FarmManagement.Modules.Farm.Domain.Aggregates.FarmAggregate;
// using FarmManagement.Modules.Farm.Domain.Repositories;
// using FarmManagement.Modules.Farm.Domain.ValueObjects;

// namespace FarmManagement.Modules.Farm.Application.Commands.AddFieldToFarm;

// public sealed class AddFieldToFarmCommandHandler
// {
//     private readonly IFarmRepository _farmRepository;

//     public AddFieldToFarmCommandHandler(IFarmRepository farmRepository)
//     {
//         _farmRepository = farmRepository;
//     }

//     public async Task Handle(AddFieldToFarmCommand command)
//     {
//         //  Correct way to construct FarmId
//         // var farmId = FarmId.From(command.FarmId);
//         var farmId = FarmManagement.Modules.Farm.Domain.ValueObjects.FarmId.From(command.FarmId);


//         var farm = await _farmRepository.GetByIdAsync(farmId);

//         if (farm is null)
//             throw new InvalidOperationException("Farm not found.");

//         var field = new Field(FieldId.New(), command.FieldName);

//         farm.AddField(field);

//         // persistence is usually handled by UnitOfWork / repository tracking
//     }
// }


// using FarmManagement.Modules.Farm.Domain.Aggregates.FarmAggregate;
// using FarmManagement.Modules.Farm.Domain.Repositories;
// using FarmManagement.Modules.Farm.Domain.ValueObjects;

// namespace FarmManagement.Modules.Farm.Application.Commands.AddFieldToFarm;

// public sealed class AddFieldToFarmCommandHandler
// {
//     private readonly IFarmRepository _farmRepository;

//     public AddFieldToFarmCommandHandler(IFarmRepository farmRepository)
//     {
//         _farmRepository = farmRepository;
//     }

//     public async Task Handle(AddFieldToFarmCommand command)
//     {
//         var farmId = new FarmId(command.FarmId);
//         var farm = await _farmRepository.GetByIdAsync(farmId);

//         if (farm is null)
//             throw new InvalidOperationException("Farm not found");

//         var field = new Field(FieldId.New(), command.FieldName);
//         farm.AddField(field);
//     }
// }
