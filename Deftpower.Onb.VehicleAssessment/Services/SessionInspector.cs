using Deftpower.Onb.VehicleAssessment.Models;
using Deftpower.Onb.VehicleAssessment.Repositories;

namespace Deftpower.Onb.VehicleAssessment.Services;

public class SessionInspector(ILogger<SessionInspector> logger, IUserRepository userRepository) : ISessionInspector
{
    public async Task Start(ChargingSession? x)
    {
        if (x == null)
        {
            throw new ArgumentNullException(nameof(x), "Charging session cannot be null.");
        }

        logger.LogInformation($"Verifying charging session {x.SessionId} for validation.");

        var forbiddenUsers = await userRepository.GetAllForbiddenUsersAsync();
        var forbiddenUserIds = forbiddenUsers.Select(s => s.UserId).ToList();

        if (forbiddenUserIds.Contains(x.UserId))
        {
            logger.LogWarning($"User {x.UserId} is not allowed to start a charging session.");
        }
        
        if (!CheckSession(x))
        {
            logger.LogError($"Charging session {x.SessionId} is not valid.");        }
    }

    private bool CheckSession(ChargingSession? s) =>
         s != null && !string.IsNullOrEmpty(s.UserId) && !string.IsNullOrEmpty(s.SessionId) && s.StartTime != new DateTime();
}