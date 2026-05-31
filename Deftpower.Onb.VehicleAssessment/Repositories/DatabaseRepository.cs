using Deftpower.Onb.VehicleAssessment.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Deftpower.Onb.VehicleAssessment.Repositories;

public class DatabaseRepository(AppDbContext db) : IDatabaseRepository
{
    public async Task<bool> CheckConnectionAsync()
    {
        return await db.Database.CanConnectAsync();
    }

    public async Task<List<string>> GetTablesAsync()
    {
        var connection = db.Database.GetDbConnection();
        await connection.OpenAsync();

        var tables = new List<string>();

        using var command = connection.CreateCommand();
        command.CommandText = @"
            SELECT table_name
            FROM information_schema.tables
            WHERE table_schema = DATABASE()
            ORDER BY table_name;
        ";

        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            tables.Add(reader.GetString(0));
        }

        return tables;
    }
} 
