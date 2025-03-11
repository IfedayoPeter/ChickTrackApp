using ChickTrack.Base.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace ChickTrack.Base.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<BaseUser> GetUserByIdAsync(string id);
        Task<BaseUser> GetUserByEmailAsync(string email);
        Task<IEnumerable<BaseUser>> GetAllUserAsync();
        Task<Result<dynamic>> GetAllUsersAsync(string search = null, string filter = null, int page = 1, int pageSize = 10, string select = null);
        Task<IdentityResult> CreateUserAsync(BaseUser user, string password);
        Task<IdentityResult> UpdateUserAsync(BaseUser user);
        Task<IdentityResult> DeleteUserAsync(string id);
        Task<string> LogInAsync(BaseUserLogin userLogin);
    }
}
