using FarmManagement.Modules.Crop.Api.Contracts;
using FarmManagement.Modules.Crop.Application.Commands.AdvanceGrowthStage;
using FarmManagement.Modules.Crop.Application.Commands.RecordHarvest;
using FarmManagement.Modules.Crop.Application.Commands.StartCropCycle;
using FarmManagement.Modules.Crop.Application.Queries.GetCropCycleDetails;
using FarmManagement.Modules.Crop.Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FarmManagement.Modules.Crop.Api.Controllers;

[ApiController]
[Route("api/crop-cycles")]
public class CropCyclesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CropCyclesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> StartCropCycle(
        [FromBody] StartCropCycleRequest request,
        CancellationToken cancellationToken)
    {
        var cropType = CropType.Create(
            request.CropCode,
            request.CropName,
            request.TypicalStages,
            request.DurationDays
        );

        var command = new StartCropCycleCommand(
            request.FarmId,
            request.FieldId,
            cropType,
            request.PlantingDate
        );

        var cropCycleId = await _mediator.Send(command, cancellationToken);

        return CreatedAtAction(
            nameof(GetCropCycleDetails),
            new { id = cropCycleId },
            new { id = cropCycleId });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCropCycleDetails(
        Guid id,
        CancellationToken cancellationToken)
    {
        var query = new GetCropCycleDetailsQuery(id);
        var result = await _mediator.Send(query, cancellationToken);

        return Ok(result);
    }

    [HttpPost("{id}/advance-stage")]
    public async Task<IActionResult> AdvanceGrowthStage(
        Guid id,
        [FromBody] AdvanceGrowthStageRequest request,
        CancellationToken cancellationToken)
    {
        var command = new AdvanceGrowthStageCommand(
            id,
            request.NewStage
        );

        await _mediator.Send(command, cancellationToken);

        return NoContent();
    }

    [HttpPost("{id}/harvest")]
    public async Task<IActionResult> RecordHarvest(
        Guid id,
        [FromBody] RecordHarvestRequest request,
        CancellationToken cancellationToken)
    {
        var command = new RecordHarvestCommand(
            id,
            request.Quantity,
            request.Unit,
            request.HarvestDate
        );

        await _mediator.Send(command, cancellationToken);

        return NoContent();
    }
}