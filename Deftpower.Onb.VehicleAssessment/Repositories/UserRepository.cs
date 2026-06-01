using Deftpower.Onb.VehicleAssessment.DbContext;
using Deftpower.Onb.VehicleAssessment.Models;
using Microsoft.EntityFrameworkCore;

namespace Deftpower.Onb.VehicleAssessment.Repositories
{
    public class UserRepository(AppDbContext db) : IUserRepository
    {
        public async Task<User> AddUserAsync(User user)
        {
            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();
            return user;
        }

        public async Task<User?> GetUserByIdAsync(string id)
        {
            return await db.Users
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await db.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<bool> UserExistsAsync(string id)
        {
            return await db.Users.AnyAsync(u => u.Id == id);
        }

        public async Task<ForbiddenUser> AddForbiddenUserAsync(string userId, string reason)
        {
            var forbidden = new ForbiddenUser
            {
                UserId = userId,
                Reason = reason,
                CreatedAt = DateTime.UtcNow
            };

            await db.ForbiddenUsers.AddAsync(forbidden);
            await db.SaveChangesAsync();

            return forbidden;
        }
        
        public async Task<List<ForbiddenUser>> GetAllForbiddenUsersAsync()
        {
            return await db.ForbiddenUsers
                .Include(f => f.User)
                .ToListAsync();
        }
        
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await db.Users
                .ToListAsync();
        }
        
        public async Task<bool> IsUserForbiddenAsync(string userId)
        {
            return await db.ForbiddenUsers.AnyAsync(x => x.UserId == userId);
        }
    }
}