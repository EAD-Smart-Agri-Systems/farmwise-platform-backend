using MediatR;
using FarmManagement.Modules.Crop.Domain.Aggregates;
using FarmManagement.Modules.Crop.Domain.ValueObjects;
using FarmManagement.Modules.Crop.Domain.Enums;
using FarmManagement.Modules.Crop.Application.Commands.StartCropCycle;
using FarmManagement.Modules.Crop.Application.Interfaces;
using FarmManagement.SharedKernel.Domain;

public sealed class StartCropCycleHandler : IRequestHandler<StartCropCycleCommand, Guid>
{
    private readonly ICropCycleRepository _repository;

    public StartCropCycleHandler(ICropCycleRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(StartCropCycleCommand request, CancellationToken cancellationToken)
    {
        // Pass raw Guids directly to CropCycle.Start
        var cropCycle = CropCycle.Start(
            request.FarmId,
            request.FieldId,
            request.CropType,
            request.PlantingDate
        );

        await _repository.AddAsync(cropCycle);
        await _repository.SaveChangesAsync();

        return cropCycle.Id.Value; // return the Guid
    }
}