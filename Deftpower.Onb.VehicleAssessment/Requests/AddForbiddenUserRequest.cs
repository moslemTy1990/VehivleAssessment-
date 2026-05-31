using System.ComponentModel.DataAnnotations;

namespace Deftpower.Onb.VehicleAssessment.Requests;

public class AddForbiddenUserRequest
{
    [Required]
    public string Reason { get; set; } = string.Empty;
}