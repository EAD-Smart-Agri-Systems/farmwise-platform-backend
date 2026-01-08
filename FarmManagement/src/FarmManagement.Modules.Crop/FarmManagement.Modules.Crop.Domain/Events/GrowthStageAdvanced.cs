// using System;
// using FarmManagement.Modules.Crop.Domain.Enums;
// using FarmManagement.Modules.Crop.Domain.ValueObjects;

// namespace FarmManagement.Modules.Crop.Domain.Events;

// public sealed class GrowthStageAdvanced
// {
//     public CropCycleId CropCycleId { get; }
//     public GrowthStage FromStage { get; }
//     public GrowthStage ToStage { get; }
//     public DateTime OccurredOn { get; }

//     private GrowthStageAdvanced(
//         CropCycleId cropCycleId,
//         GrowthStage fromStage,
//         GrowthStage toStage)
//     {
//         CropCycleId = cropCycleId;
//         FromStage = fromStage;
//         ToStage = toStage;
//         OccurredOn = DateTime.UtcNow;
//     }

//     public static GrowthStageAdvanced Create(
//         CropCycleId cropCycleId,
//         GrowthStage fromStage,
//         GrowthStage toStage)
//     {
//         return new GrowthStageAdvanced(
//             cropCycleId,
//             fromStage,
//             toStage);
//     }
// }
using System;
using FarmManagement.Modules.Crop.Domain.ValueObjects;
using FarmManagement.Modules.Crop.Domain.Enums;
using FarmManagement.SharedKernel.Domain;

namespace FarmManagement.Modules.Crop.Domain.Events;

public sealed record GrowthStageAdvanced(
    CropCycleId CropCycleId,
    GrowthStage FromStage,
    GrowthStage ToStage) : DomainEvent
{
    public DateTime OccurredOn { get; init; } = DateTime.UtcNow;

    // Factory method
    public static GrowthStageAdvanced Create(
        CropCycleId cropCycleId,
        GrowthStage fromStage,
        GrowthStage toStage)
        => new(cropCycleId, fromStage, toStage);
}
