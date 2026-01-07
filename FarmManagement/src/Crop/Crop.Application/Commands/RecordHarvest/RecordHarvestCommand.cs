using System;

namespace Crop.Application.Commands.RecordHarvest;

public sealed record RecordHarvestCommand(
    Guid CropCycleId,
    double Quantity,
    string Unit,
    DateTime HarvestDate
);
