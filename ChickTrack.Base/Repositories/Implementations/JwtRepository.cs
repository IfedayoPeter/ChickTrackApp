using ChickTrack.Base.Domain.Entities;
using ChickTrack.Base.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ChickTrack.Base.Repositories.Implementations
{
    public class JwtRepository : IJwtRepository
    {
        private readonly RoleManager<IdentityRole> _roleManger;
        private readonly UserManager<BaseUser> _userManger;
        private readonly IConfiguration _configuration;

        public JwtRepository(
            UserManager<BaseUser> userManger,
            RoleManager<IdentityRole> roleManger,
            IConfiguration configuration)
        {
            _configuration = configuration;
            _roleManger = roleManger;
            _userManger = userManger;
        }
        public async Task<string> GenerateJwtToken(BaseUserLogin baseUser)
        {
            Result<string> result = new(false);

            //Get user from userManager
            var user = await _userManger.FindByEmailAsync(baseUser.Email);
            if (user == null)
            {
                result.SetError("Error", $"user {baseUser.Email} not found");
                throw new ApplicationException("user not found");
            }

            //Get roles assigned to the user
            var userRoles = await _userManger.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, baseUser.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            //Include user roles as claims
            authClaims.AddRange(userRoles.Select(role =>
            new Claim(ClaimTypes.Role, role)));

            var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Validissuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256Signature)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
