using Deftpower.Onb.VehicleAssessment.Models;

namespace Deftpower.Onb.VehicleAssessment.Repositories;

public interface IChargingSessionRepository
{
    Task<(ChargingSession Session, bool WasCreated)> UpsertAsync(ChargingSession session);
    Task<List<ChargingSession>> GetByUserIdAsync(string userId);
}