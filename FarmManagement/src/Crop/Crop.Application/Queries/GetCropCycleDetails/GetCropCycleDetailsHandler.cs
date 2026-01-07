using MediatR;
using Crop.Application.DTOs;
using Crop.Application.Interfaces;
using Crop.Domain.ValueObjects;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Crop.Application.Queries.GetCropCycleDetails
{
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
            var cropCycleId = CropCycleId.From(request.CropCycleId);
            var cropCycle = await _repository.GetByIdAsync(cropCycleId);

            if (cropCycle is null)
                throw new KeyNotFoundException($"Crop cycle {request.CropCycleId} not found");

            return new CropCycleDto(
                Id: cropCycle.Id.Value,
                FarmId: cropCycle.FarmId.Value,
                FieldId: cropCycle.FieldId.Value,
                CropType: cropCycle.CropType?.Name ?? string.Empty,
                GrowthStage: cropCycle.CurrentStage.ToString(),
                Status: cropCycle.Status.ToString(),
                StartDate: cropCycle.PlantingDate,
                ExpectedHarvestDate: cropCycle.ExpectedHarvestDate,
                HarvestedQuantity: cropCycle.YieldRecord?.Quantity,
                HarvestUnit: cropCycle.YieldRecord?.Unit.ToString()
            );
        }
    }
}