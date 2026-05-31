using Deftpower.Onb.VehicleAssessment.Models;
using Deftpower.Onb.VehicleAssessment.Repositories;
using Deftpower.Onb.VehicleAssessment.Requests;
using Deftpower.Onb.VehicleAssessment.Services;
using Microsoft.AspNetCore.Mvc;

namespace Deftpower.Onb.VehicleAssessment.Controllers;

[ApiController]
[Route("api/sessions")]
public class ChargingSessionController(ISessionInspector sessionInspector, IChargingSessionRepository chargingSessionRepository) : ControllerBase
{
    [HttpPut]
    public async Task<IActionResult> CreateOrUpdateSession(ChargingSession chargingSession)
    {
        await sessionInspector.Start(chargingSession);

        return Ok();
    }

    [Route("/api/users/{userId}/sessions")]
    [HttpGet]
    public IActionResult GetSessionsByUser(string userId)
    {
        return Ok();
    }
    
    
    
    /////
    ///
    ///
    [HttpPost("charging-sessions")]
    public async Task<IActionResult> UpsertChargingSession([FromBody] ChargingSessionRequest? request)
    {
        if (request is null)
        {
            return BadRequest(new { message = "Request body is required." });
        }

        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        if (request.EndTime.HasValue && request.EndTime.Value < request.StartTime)
        {
            return BadRequest(new { message = "End time cannot be earlier than start time." });
        }

        var session = new ChargingSession
        {
            SessionId = request.SessionId.Trim(),
            UserId = request.UserId.Trim(),
            ChargePointId = request.ChargePointId.Trim(),
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            EnergyKwh = request.EnergyKwh
        };

        var savedSession = await chargingSessionRepository.UpsertAsync(session);

        return Ok(savedSession);
    }

    [HttpGet("users/{userId}/charging-sessions")]
    public async Task<IActionResult> GetChargingSessionsForUser(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            return BadRequest(new { message = "User id is required." });
        }

        var sessions = await chargingSessionRepository.GetByUserIdAsync(userId.Trim());

        return Ok(sessions);
    }
}
