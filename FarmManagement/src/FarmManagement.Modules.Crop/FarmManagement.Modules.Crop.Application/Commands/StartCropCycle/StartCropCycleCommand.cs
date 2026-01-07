using System;

namespace FarmManagement.Modules.Crop.Application.Commands.StartCropCycle;

public sealed record StartCropCycleCommand(
    Guid FarmId,
    Guid FieldId,
    int CropCode,
    string CropName,
    string TypicalStages,
    int DurationDays,
    DateTime PlantingDate
);
