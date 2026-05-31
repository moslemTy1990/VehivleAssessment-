using System.Text.Json.Serialization;
using Deftpower.Onb.VehicleAssessment.Repositories;
using Deftpower.Onb.VehicleAssessment.Services;

namespace Deftpower.Onb.VehicleAssessment.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddSessionInspectorServices(this IServiceCollection services)
    { 
        services.AddScoped<ISessionInspector, SessionInspector>();
        services.AddScoped<IDatabaseRepository, DatabaseRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IChargingSessionRepository, ChargingSessionRepository>();

        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler =
                    ReferenceHandler.IgnoreCycles;
            });
        return services;
    }
}