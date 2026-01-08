// using System;
// using FarmManagement.Modules.Crop.Domain.ValueObjects;
// using FarmManagement.SharedKernel.Domain;
// using FarmManagement.Modules.Crop.Domain.Events;

// public sealed class CropCycleStarted
// {
//     public CropCycleId CropCycleId { get; }
//     public FarmId FarmId { get; }
//     public FieldId FieldId { get; }
//     public CropType CropType { get; }
//     public DateTime StartDate { get; }
//     public DateTime OccurredOn { get; }

//     private CropCycleStarted(
//         CropCycleId cropCycleId,
//         FarmId farmId,
//         FieldId fieldId,
//         CropType cropType,
//         DateTime startDate)
//     {
//         CropCycleId = cropCycleId;
//         FarmId = farmId;
//         FieldId = fieldId;
//         CropType = cropType;
//         StartDate = startDate;
//         OccurredOn = DateTime.UtcNow;
//     }

//     public static CropCycleStarted Create(
//         CropCycleId cropCycleId,
//         FarmId farmId,
//         FieldId fieldId,
//         CropType cropType,
//         DateTime startDate)
//     {
//         return new CropCycleStarted(
//             cropCycleId,
//             farmId,
//             fieldId,
//             cropType,
//             startDate);
//     }
// }
using System;
using FarmManagement.Modules.Crop.Domain.ValueObjects;
using FarmManagement.SharedKernel.Domain;

namespace FarmManagement.Modules.Crop.Domain.Events;

public sealed record CropCycleStarted(
    CropCycleId CropCycleId,
    FarmId FarmId,
    FieldId FieldId,
    CropType CropType,
    DateTime StartDate) : DomainEvent
{
    public DateTime OccurredOn { get; init; } = DateTime.UtcNow;

    // Factory method
    public static CropCycleStarted Create(
        CropCycleId cropCycleId,
        FarmId farmId,
        FieldId fieldId,
        CropType cropType,
        DateTime startDate)
        => new(cropCycleId, farmId, fieldId, cropType, startDate);
}
