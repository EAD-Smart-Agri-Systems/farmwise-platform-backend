using System;

namespace FarmManagement.Modules.Crop.Application.Commands.RecordHarvest;

public sealed record RecordHarvestCommand(
    Guid CropCycleId,
    double Quantity,
    string Unit,
    DateTime HarvestDate
);
