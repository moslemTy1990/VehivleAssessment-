using Deftpower.Onb.VehicleAssessment.Models;

namespace Deftpower.Onb.VehicleAssessment.Services;

public interface ISessionInspector
{
    Task Start(ChargingSession? s);
}