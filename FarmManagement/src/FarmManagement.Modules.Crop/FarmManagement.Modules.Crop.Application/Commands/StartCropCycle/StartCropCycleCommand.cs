using MediatR;
using FarmManagement.Modules.Crop.Domain.Enums;
using FarmManagement.Modules.Crop.Domain.ValueObjects;
using System;
using FarmManagement.SharedKernel.Domain;

namespace FarmManagement.Modules.Crop.Application.Commands.StartCropCycle
{
    public sealed class StartCropCycleCommand : IRequest<Guid>
    {
        public Guid FarmId { get; init; }
        public Guid FieldId { get; init; }
        public CropType CropType { get; init; }
        public DateTime PlantingDate { get; init; }

        public StartCropCycleCommand(Guid farmId, Guid fieldId, CropType cropType, DateTime plantingDate)
        {
            FarmId = farmId;
            FieldId = fieldId;
            CropType = cropType;
            PlantingDate = plantingDate;
        }
    }
}
