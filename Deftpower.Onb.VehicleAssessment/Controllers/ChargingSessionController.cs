using Deftpower.Onb.VehicleAssessment.Models;
using Deftpower.Onb.VehicleAssessment.Repositories;
using Deftpower.Onb.VehicleAssessment.Requests;
using Deftpower.Onb.VehicleAssessment.Services;
using Microsoft.AspNetCore.Mvc;

namespace Deftpower.Onb.VehicleAssessment.Controllers;
[ApiController]
public class ChargingSessionController(
    IChargingSessionRepository chargingSessionRepository,
    ISessionInspector sessionInspector) : ControllerBase
{
    // PUT /api/sessions
    // Upsert by SessionId: first call creates, subsequent calls update.
    [HttpPut("api/sessions")]
    public async Task<IActionResult> UpsertChargingSession([FromBody] ChargingSessionRequest? request)
    {
        if (request is null)
            return BadRequest(new { message = "Request body is required." });

        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        if (request.EndTime.HasValue && request.EndTime.Value < request.StartTime)
            return BadRequest(new { message = "End time cannot be earlier than start time." });

        var session = new ChargingSession
        {
            SessionId = request.SessionId.Trim(),
            UserId = request.UserId.Trim(),
            ChargePointId = request.ChargePointId.Trim(),
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            EnergyKwh = request.EnergyKwh
        };

        try
        {
            await sessionInspector.Start(session);
        }
        catch (InvalidOperationException ex)
        {
            return Problem(statusCode: StatusCodes.Status400BadRequest, detail: ex.Message);        }

        var result = await chargingSessionRepository.UpsertAsync(session);

        if (result.WasCreated)
        {
            return Created(
                $"/api/users/{result.Session.UserId}/sessions",
                result.Session);
        }

        return Ok(result.Session);
    }

    // GET /api/users/{userId}/sessions
    [HttpGet("api/users/{userId}/sessions")]
    public async Task<IActionResult> GetSessionsByUser(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
            return BadRequest(new { message = "User id is required." });

        var sessions = await chargingSessionRepository.GetByUserIdAsync(userId.Trim());
        return Ok(sessions);
    }
}
