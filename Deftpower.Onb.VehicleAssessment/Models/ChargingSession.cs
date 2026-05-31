namespace Deftpower.Onb.VehicleAssessment.Models;

public record ChargingSession
{
    public required string SessionId { get; set; }

    public required string UserId { get; set; }

    public required string ChargePointId { get; set; }

    public required DateTime StartTime { get; set; }

    public DateTime? EndTime { get; set; } // Nullable to allow ongoing sessions

    public double EnergyKwh { get; set; } // in kWh
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
