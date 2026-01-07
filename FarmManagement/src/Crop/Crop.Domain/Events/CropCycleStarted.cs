using System;
using Crop.Domain.ValueObjects;

namespace Crop.Domain.Events;

public sealed class CropCycleStarted
{
    public CropCycleId CropCycleId { get; }
    public FarmId FarmId { get; }
    public FieldId FieldId { get; }
    public CropType CropType { get; }
    public DateTime StartDate { get; }
    public DateTime OccurredOn { get; }

    private CropCycleStarted(
        CropCycleId cropCycleId,
        FarmId farmId,
        FieldId fieldId,
        CropType cropType,
        DateTime startDate)
    {
        CropCycleId = cropCycleId;
        FarmId = farmId;
        FieldId = fieldId;
        CropType = cropType;
        StartDate = startDate;
        OccurredOn = DateTime.UtcNow;
    }

    public static CropCycleStarted Create(
        CropCycleId cropCycleId,
        FarmId farmId,
        FieldId fieldId,
        CropType cropType,
        DateTime startDate)
    {
        return new CropCycleStarted(
            cropCycleId,
            farmId,
            fieldId,
            cropType,
            startDate);
    }
}
