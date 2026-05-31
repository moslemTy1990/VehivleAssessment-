using Deftpower.Onb.VehicleAssessment.DbContext;
using Deftpower.Onb.VehicleAssessment.Models;
using Microsoft.EntityFrameworkCore;

namespace Deftpower.Onb.VehicleAssessment.Repositories;

public class ChargingSessionRepository(AppDbContext db) : IChargingSessionRepository
{
    public async Task<ChargingSession> UpsertAsync(ChargingSession session)
    {
        var now = DateTime.UtcNow;

        var existingSession = await db.ChargingSessions
            .FirstOrDefaultAsync(existing => existing.SessionId == session.SessionId);

        if (existingSession is null)
        {
            session.CreatedAt = now;
            session.UpdatedAt = now;

            await db.ChargingSessions.AddAsync(session);
            await db.SaveChangesAsync();

            return session;
        }

        existingSession.UserId = session.UserId;
        existingSession.ChargePointId = session.ChargePointId;
        existingSession.StartTime = session.StartTime;
        existingSession.EndTime = session.EndTime;
        existingSession.EnergyKwh = session.EnergyKwh;
        existingSession.UpdatedAt = now;

        await db.SaveChangesAsync();

        return existingSession;
    }

    public async Task<List<ChargingSession>> GetByUserIdAsync(string userId)
    {
        return await db.ChargingSessions
            .Where(session => session.UserId == userId)
            .OrderByDescending(session => session.StartTime)
            .ToListAsync();
    }
}