using FarmManagement.Modules.Crop.Application.Interfaces;
using FarmManagement.Modules.Crop.Domain.Enums;
using FarmManagement.Modules.Crop.Domain.ValueObjects;

namespace FarmManagement.Modules.Crop.Application.Commands.RecordHarvest;

public sealed class RecordHarvestHandler
{
    private readonly ICropCycleRepository _repository;

    public RecordHarvestHandler(ICropCycleRepository repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(RecordHarvestCommand command)
    {
        var cropCycle = await _repository.GetByIdAsync(
            CropCycleId.From(command.CropCycleId)
        );

        if (cropCycle is null)
            throw new InvalidOperationException("Crop cycle not found.");

        var unit = Enum.Parse<YieldUnit>(
            command.Unit,
            ignoreCase: true
        );

        var yieldRecord = YieldRecord.Create(
            (float)command.Quantity,
            unit,
            command.HarvestDate
        );

        cropCycle.RecordHarvest(yieldRecord);

        await _repository.SaveChangesAsync();
    }
}
