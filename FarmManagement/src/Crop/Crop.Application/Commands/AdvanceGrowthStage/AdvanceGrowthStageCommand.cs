using System;

namespace Crop.Application.Commands.AdvanceGrowthStage;

public sealed record AdvanceGrowthStageCommand(
    Guid CropCycleId,
    string NewStage
);
