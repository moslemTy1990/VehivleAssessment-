using System.ComponentModel.DataAnnotations;

namespace Deftpower.Onb.VehicleAssessment.Requests;

public class ChargingSessionRequest
{
    [Required]
    [MinLength(1)] 
    public string SessionId { get; set; } = string.Empty;

    [Required]
    [MinLength(1)]
    public string UserId { get; set; } = string.Empty;

    [Required]
    [MinLength(1)]
    public string ChargePointId { get; set; } = string.Empty;

    [Required]
    public DateTime StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Energy consumed must be zero or greater.")]
    public double EnergyKwh { get; set; }
}