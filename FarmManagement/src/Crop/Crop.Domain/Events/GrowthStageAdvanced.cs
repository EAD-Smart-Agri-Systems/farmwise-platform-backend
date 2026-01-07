using System;
using Crop.Domain.Enums;
using Crop.Domain.ValueObjects;

namespace Crop.Domain.Events;

public sealed class GrowthStageAdvanced
{
    public CropCycleId CropCycleId { get; }
    public GrowthStage FromStage { get; }
    public GrowthStage ToStage { get; }
    public DateTime OccurredOn { get; }

    private GrowthStageAdvanced(
        CropCycleId cropCycleId,
        GrowthStage fromStage,
        GrowthStage toStage)
    {
        CropCycleId = cropCycleId;
        FromStage = fromStage;
        ToStage = toStage;
        OccurredOn = DateTime.UtcNow;
    }

    public static GrowthStageAdvanced Create(
        CropCycleId cropCycleId,
        GrowthStage fromStage,
        GrowthStage toStage)
    {
        return new GrowthStageAdvanced(
            cropCycleId,
            fromStage,
            toStage);
    }
}
