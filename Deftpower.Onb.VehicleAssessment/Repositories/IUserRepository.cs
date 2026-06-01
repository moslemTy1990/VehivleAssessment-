using Deftpower.Onb.VehicleAssessment.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Deftpower.Onb.VehicleAssessment.Repositories
{
    public interface IUserRepository
    {
        Task<User> AddUserAsync(User user);
        Task<User?> GetUserByIdAsync(string id);
        Task<bool> EmailExistsAsync(string email);
        Task<bool> UserExistsAsync(string id);
        Task<bool> IsUserForbiddenAsync(string userId);

        Task<ForbiddenUser> AddForbiddenUserAsync(string userId, string reason);
        Task<List<ForbiddenUser>> GetAllForbiddenUsersAsync();
        
        Task<List<User>> GetAllUsersAsync();
    }
}