
using System.ComponentModel.DataAnnotations;

namespace Deftpower.Onb.VehicleAssessment.Requests;

public class AddUserRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Name { get; set; } = string.Empty;
}
