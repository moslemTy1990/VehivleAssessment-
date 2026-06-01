using Deftpower.Onb.VehicleAssessment.Models;
using Deftpower.Onb.VehicleAssessment.Repositories;

namespace Deftpower.Onb.VehicleAssessment.Services;

public class SessionInspector(ILogger<SessionInspector> logger, IUserRepository userRepository) : ISessionInspector
{
    public async Task Start(ChargingSession? session)
    {
        if (session is null)
            throw new ArgumentNullException(nameof(session), "Charging session cannot be null.");

        logger.LogInformation("Validating charging session {SessionId}.", session.SessionId);

        if (!CheckSession(session))
            throw new ArgumentException("Charging session is invalid.", nameof(session));

        var isForbidden = await userRepository.IsUserForbiddenAsync(session.UserId);
        
        if (isForbidden)
        {
            logger.LogWarning("User {UserId} is forbidden from charging.", session.UserId);
            throw new InvalidOperationException($"User {session.UserId} is forbidden from starting charging sessions.");
        }
    }

    private static bool CheckSession(ChargingSession s) =>
        !string.IsNullOrWhiteSpace(s.UserId) &&
        !string.IsNullOrWhiteSpace(s.SessionId) &&
        s.StartTime != default;
}