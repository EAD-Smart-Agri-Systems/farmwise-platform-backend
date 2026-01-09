using FarmManagement.Modules.Advisory.Api.Contracts;
using FarmManagement.Modules.Advisory.Application.Commands.GenerateAdvisoryReport;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FarmManagement.Modules.Advisory.Api.Controllers;

[ApiController]
[Route("api/advisory")]
[Authorize] // All endpoints require authentication
public class AdvisoryController(
    GenerateAdvisoryReportHandler handler) : ControllerBase
{
    private readonly GenerateAdvisoryReportHandler _handler = handler;

    [HttpPost("reports")]
    [Authorize(Policy = "FarmerOrAdmin")]
    public async Task<IActionResult> GenerateReport(
        [FromBody] GenerateAdvisoryReportRequest request)
    {
        var command = new GenerateAdvisoryReportCommand(
            request.FarmId,
            request.CropType,
            request.Temperature,
            request.Humidity,
            request.WeatherCondition,
            request.Recommendation
        );

        var reportId = await _handler.HandleAsync(command);

        return CreatedAtAction(nameof(GenerateReport), new { id = reportId }, reportId);
    }
}
