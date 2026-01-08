using MediatR;
using FarmManagement.Modules.Crop.Application.DTOs;
using FarmManagement.Modules.Crop.Application.Interfaces;
using FarmManagement.Modules.Crop.Domain.ValueObjects; // for CropCycleId

namespace FarmManagement.Modules.Crop.Application.Queries.GetCropCycleDetails;

public sealed class GetCropCycleDetailsHandler
    : IRequestHandler<GetCropCycleDetailsQuery, CropCycleDto>
{
    private readonly ICropCycleRepository _repository;

    public GetCropCycleDetailsHandler(ICropCycleRepository repository)
    {
        _repository = repository;
    }

    public async Task<CropCycleDto> Handle(
        GetCropCycleDetailsQuery request,
        CancellationToken cancellationToken)
    {
        // Convert Guid to CropCycleId value object
        var cropCycleId = CropCycleId.From(request.CropCycleId);

        var cropCycle = await _repository.GetByIdAsync(cropCycleId);

        if (cropCycle is null)
            throw new KeyNotFoundException(
                $"Crop cycle {request.CropCycleId} not found"
            );

        var harvest = cropCycle.YieldRecord;

        return new CropCycleDto(
            Id: cropCycle.Id.Value,
            FarmId: cropCycle.FarmId.Value,
            FieldId: cropCycle.FieldId.Value,
            CropType: cropCycle.CropType.Name,
            GrowthStage: cropCycle.CurrentStage.ToString(),
            Status: cropCycle.Status.ToString(),
            StartDate: cropCycle.PlantingDate,
            ExpectedHarvestDate: cropCycle.ExpectedHarvestDate,
            HarvestedQuantity: harvest?.Quantity,
            HarvestUnit: harvest?.Unit.ToString()
        );
    }
}
