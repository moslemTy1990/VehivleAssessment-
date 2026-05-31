namespace Deftpower.Onb.VehicleAssessment.Repositories;

public interface IDatabaseRepository
{
    Task<bool> CheckConnectionAsync();
    Task<List<string>> GetTablesAsync();
}