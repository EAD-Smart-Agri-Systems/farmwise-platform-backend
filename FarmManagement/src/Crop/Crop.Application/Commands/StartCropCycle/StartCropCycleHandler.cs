using Crop.Application.Interfaces;
using Crop.Domain.Aggregates;
using Crop.Domain.ValueObjects;

namespace Crop.Application.Commands.StartCropCycle;

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
