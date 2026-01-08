using MediatR;
using FarmManagement.Modules.Crop.Application.Interfaces;
using FarmManagement.Modules.Crop.Domain.Enums;
using FarmManagement.Modules.Crop.Domain.ValueObjects;

namespace FarmManagement.Modules.Crop.Application.Commands.AdvanceGrowthStage;

public sealed class AdvanceGrowthStageHandler : IRequestHandler<AdvanceGrowthStageCommand>
{
    private readonly ICropCycleRepository _repository;

    public AdvanceGrowthStageHandler(ICropCycleRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(AdvanceGrowthStageCommand request, CancellationToken cancellationToken)
    {
        var cropCycle = await _repository.GetByIdAsync(
            CropCycleId.From(request.CropCycleId)
        );

        if (cropCycle is null)
            throw new InvalidOperationException("Crop cycle not found.");

        var newStage = Enum.Parse<GrowthStage>(
            request.NewStage,
            ignoreCase: true
        );

        cropCycle.AdvanceStage(newStage);

        await _repository.SaveChangesAsync();
    }
}
