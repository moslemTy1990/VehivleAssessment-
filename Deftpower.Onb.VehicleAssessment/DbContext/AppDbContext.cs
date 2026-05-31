using Deftpower.Onb.VehicleAssessment.Models;
using Microsoft.EntityFrameworkCore;

namespace Deftpower.Onb.VehicleAssessment.DbContext;

public class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<User> Users => Set<User>();
    public DbSet<ForbiddenUser> ForbiddenUsers => Set<ForbiddenUser>();
    public DbSet<ChargingSession> ChargingSessions => Set<ChargingSession>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<ForbiddenUser>()
            .HasOne(f => f.User)
            .WithOne(u => u.ForbiddenUser)
            .HasForeignKey<ForbiddenUser>(f => f.UserId);

        modelBuilder.Entity<ForbiddenUser>()
            .HasIndex(f => f.UserId)
            .IsUnique();

        modelBuilder.Entity<ChargingSession>()
            .HasKey(session => session.SessionId);
        modelBuilder.Entity<ChargingSession>()
            .HasIndex(session => session.UserId);
    }
}