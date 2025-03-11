using AutoMapper;
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

        public UserRepository(UserManager<BaseUser> userManager,
            SignInManager<BaseUser> signInManager,
            IJwtRepository jwtRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtRepository = jwtRepository;
        }

        public async Task<Result<dynamic>> GetAllUsersAsync(
        string search = null,
        string filter = null,
        int page = 1,
        int pageSize = 10,
        string select = null)
        {
            Result<dynamic> result = new(false);

            try
            {
                var query = _userManager.Users.AsQueryable();

                // Apply search by email or username
                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(u => u.Email.Contains(search) || u.UserName.Contains(search));
                }

                // Apply filter (e.g., by role)
                if (!string.IsNullOrEmpty(filter))
                {
                    query = query.Where(u => u.NormalizedEmail.Contains(filter.ToUpper())); // Example filter
                }

                // Get total count before pagination
                int totalCount = query.Count();

                // Apply pagination
                var users = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

                // Manually map users to DTO
                var responseDTO = users.Select(u => new BaseUser
                {
                    FullName = u.FullName,
                    CreatedBy = u.CreatedBy,
                    LastModifiedBy = u.LastModifiedBy,
                    UserName = u.UserName,
                    NormalizedUserName = u.NormalizedUserName,
                    Email = u.Email,
                    NormalizedEmail = u.NormalizedEmail,
                    EmailConfirmed = u.EmailConfirmed,
                    PasswordHash = u.PasswordHash,
                    PhoneNumber = u.PhoneNumber,
                    PhoneNumberConfirmed = u.PhoneNumberConfirmed,
                    TwoFactorEnabled = u.TwoFactorEnabled,
                    LockoutEnd = u.LockoutEnd,
                    LockoutEnabled = u.LockoutEnabled,
                    AccessFailedCount = u.AccessFailedCount
                }).ToList();

                // Selecting specific properties
                if (!string.IsNullOrEmpty(select))
                {
                    var selectedProperties = select.Split(',', StringSplitOptions.TrimEntries);
                    var selectedData = responseDTO.Select(s => BaseServiceHelper.SelectProperties(s, selectedProperties)).ToList();

                    result.SetSuccess(new { TotalCount = totalCount, Data = selectedData }, "Retrieved Successfully.");
                    return result;
                }

                result.SetSuccess(new { TotalCount = totalCount, Data = responseDTO }, "Retrieved Successfully.");
            }
            catch (Exception ex)
            {
                result.SetError(ex.Message, "Error while retrieving users.");
                return result;
            }

            return result;
        }



        public async Task<BaseUser> GetUserByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<BaseUser> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<IEnumerable<BaseUser>> GetAllUserAsync()
        {
            return await Task.FromResult(_userManager.Users.ToList());
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

            var user = await _userManager.FindByEmailAsync(baseUser.Email.ToUpper());
            if (user == null)
            {
                return "Incorrect Email or Password";
            }

            var response = await _signInManager.PasswordSignInAsync(user.UserName, baseUser.Password, false, false);
            if (!response.Succeeded)
            {
                result.SetError("Error", $"wrong {baseUser.Email} or {baseUser.Password}");
                return ("Incorrect Email or Password");
            }

            return await _jwtRepository.GenerateJwtToken(baseUser);
        }
    }
}
