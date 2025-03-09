using ChickTrack.Base.Domain.Entities;
using ChickTrack.Base.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ChickTrack.Base.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<BaseUser> _userManager;
        private readonly SignInManager<BaseUser> _signInManager;
        private readonly IJwtRepository _jwtRepository;

        public UserRepository(UserManager<BaseUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<BaseUser> GetUserByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<BaseUser> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<IdentityResult> CreateUserAsync(BaseUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<IdentityResult> UpdateUserAsync(BaseUser user)
        {
            return await _userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> DeleteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return await _userManager.DeleteAsync(user);
        }

        public async Task<string> LogInAsync(BaseUserLogin baseUser)
        {
            Result<string> result = new(false);

            var response = await _signInManager.PasswordSignInAsync(baseUser.Email, baseUser.Password, false, false);
            if (!response.Succeeded)
            {
                result.SetError("Error", $"wrong {baseUser.Email} or {baseUser.Password}");
                return ("Incorrect Email or Password");
            }

            return await _jwtRepository.GenerateJwtToken(baseUser);
        }
    }
}
