using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using FarmManagement.Modules.Farm.Application.Commands.RegisterFarm;
using FarmManagement.Modules.Farm.Application.Commands.AddFieldToFarm;

namespace FarmManagement.Modules.Farm.Api.Controllers;

[ApiController]
[Route("api/farms")]
[Authorize] // All endpoints require authentication
public sealed class FarmsController : ControllerBase
{
    private readonly IMediator _mediator;

    public FarmsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // POST /api/farms
    [HttpPost]
    [Authorize(Policy = "FarmerOrAdmin")]
    public async Task<IActionResult> RegisterFarm(
        [FromBody] RegisterFarmRequest request,
        CancellationToken cancellationToken)
    {
        var command = new RegisterFarmCommand(
            request.Name,
            request.Latitude,
            request.Longitude);

        var farmId = await _mediator.Send(command, cancellationToken);

        return CreatedAtAction(
            nameof(GetFarm),
            new { id = farmId },
            new { id = farmId });
    }

    // POST /api/farms/{id}/fields
    [HttpPost("{id:guid}/fields")]
    [Authorize(Policy = "FarmerOrAdmin")]
    public async Task<IActionResult> AddField(
        Guid id,
        [FromBody] AddFieldRequest request,
        CancellationToken cancellationToken)
    {
        var command = new AddFieldToFarmCommand(id, request.Name);
        await _mediator.Send(command, cancellationToken);

        return NoContent();
    }

    // Placeholder for GetFarm - implement if needed
    [HttpGet("{id:guid}")]
    [Authorize(Policy = "FarmerOrAdmin")]
    public async Task<IActionResult> GetFarm(Guid id)
    {
        // Implement query handler if needed
        return Ok();
    }
}
