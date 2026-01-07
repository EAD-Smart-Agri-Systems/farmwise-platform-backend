using FarmManagement.Modules.Crop.Application.Interfaces;
using FarmManagement.Modules.Crop.Domain.Aggregates;
using FarmManagement.Modules.Crop.Domain.ValueObjects;

namespace FarmManagement.Modules.Crop.Application.Commands.StartCropCycle;

public sealed class StartCropCycleHandler
{
    private readonly ICropCycleRepository _repository;

    public StartCropCycleHandler(ICropCycleRepository repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(StartCropCycleCommand command)
    {
        var cropType = CropType.Create(
            command.CropCode,
            command.CropName,
            command.TypicalStages,
            command.DurationDays
        );

        var cropCycle = CropCycle.Start(
            FarmId.From(command.FarmId),
            FieldId.From(command.FieldId),
            cropType,
            command.PlantingDate
        );

        await _repository.AddAsync(cropCycle);
        await _repository.SaveChangesAsync();
    }
}
