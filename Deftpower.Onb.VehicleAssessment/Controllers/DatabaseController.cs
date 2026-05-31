using Deftpower.Onb.VehicleAssessment.Repositories;
using Deftpower.Onb.VehicleAssessment.DbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Deftpower.Onb.VehicleAssessment.Controllers;

[ApiController]
[Route("api/database")]
public class DatabaseController(IDatabaseRepository databaseRepository) : ControllerBase
{
    [HttpGet("check")]
    public async Task<IActionResult> CheckConnection()
    {
        var canConnect = await databaseRepository.CheckConnectionAsync();
        return Ok(new { connected = canConnect });
    }

    [HttpGet("tables")]
    public async Task<IActionResult> GetTables()
    {
        var tables = await databaseRepository.GetTablesAsync();
        return Ok(new { tables });
    }
}