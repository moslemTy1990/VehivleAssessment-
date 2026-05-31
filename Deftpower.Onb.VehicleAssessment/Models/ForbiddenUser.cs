namespace Deftpower.Onb.VehicleAssessment.Models;

public class ForbiddenUser
{
    public int Id { get; set; }

    public string UserId { get; set; }   // 👈 FK column

    public string Reason { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public User User { get; set; } = null!;
}