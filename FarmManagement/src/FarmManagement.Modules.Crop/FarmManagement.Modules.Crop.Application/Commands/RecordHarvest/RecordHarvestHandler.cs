using MediatR;
using FarmManagement.Modules.Crop.Application.Interfaces;
using FarmManagement.Modules.Crop.Domain.Enums;
using FarmManagement.Modules.Crop.Domain.ValueObjects;

namespace FarmManagement.Modules.Crop.Application.Commands.RecordHarvest;

public sealed class RecordHarvestHandler : IRequestHandler<RecordHarvestCommand>
{
    private readonly ICropCycleRepository _repository;

    public RecordHarvestHandler(ICropCycleRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(RecordHarvestCommand request, CancellationToken cancellationToken)
    {
        var cropCycle = await _repository.GetByIdAsync(
            CropCycleId.From(request.CropCycleId)
        );

        if (cropCycle is null)
            throw new InvalidOperationException("Crop cycle not found.");

        var unit = Enum.Parse<YieldUnit>(
            request.Unit,
            ignoreCase: true
        );

        var yieldRecord = YieldRecord.Create(
            (float)request.Quantity,
            unit,
            request.HarvestDate
        );

        cropCycle.RecordHarvest(yieldRecord);

        await _repository.SaveChangesAsync(cancellationToken);
    }
}
