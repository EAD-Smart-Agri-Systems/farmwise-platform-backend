using System;

namespace FarmManagement.Modules.Crop.Application.Commands.AdvanceGrowthStage;

public sealed record AdvanceGrowthStageCommand(
    Guid CropCycleId,
    string NewStage
);
