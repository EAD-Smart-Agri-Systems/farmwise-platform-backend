using Microsoft.AspNetCore.Mvc;
using FarmManagement.Modules.Farm.Application.Commands.RegisterFarm;
using FarmManagement.Modules.Farm.Application.Commands.AddFieldToFarm;

namespace FarmManagement.Modules.Farm.Api.Controllers;

[ApiController]
[Route("api/farms")]
public sealed class FarmsController : ControllerBase
{
    private readonly RegisterFarmCommandHandler _registerFarm;
    private readonly AddFieldToFarmCommandHandler _addField;

    public FarmsController(
        RegisterFarmCommandHandler registerFarm,
        AddFieldToFarmCommandHandler addField)
    {
        _registerFarm = registerFarm;
        _addField = addField;
    }

    // POST /api/farms
    [HttpPost]
    public async Task<IActionResult> RegisterFarm(
        [FromBody] RegisterFarmRequest request)
    {
        await _registerFarm.Handle(new RegisterFarmCommand(
            request.Name,
            request.Latitude,
            request.Longitude));

        return Created(string.Empty, null);
    }

    // POST /api/farms/{id}/fields
    [HttpPost("{id:guid}/fields")]
    public async Task<IActionResult> AddField(
        Guid id,
        [FromBody] AddFieldRequest request)
    {
        await _addField.Handle(new AddFieldToFarmCommand(
            id,
            request.Name));

        return NoContent();
    }
}
