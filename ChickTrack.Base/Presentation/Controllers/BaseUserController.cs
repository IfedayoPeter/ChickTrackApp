using ChickTrack.Base.Domain.Entities;
using ChickTrack.Base.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace ChickTrack.Base.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseUserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public BaseUserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] BaseUserLogin userLogin)
        {
            var token = await _userRepository.LogInAsync(userLogin);
            if (string.IsNullOrEmpty(token))
                return Unauthorized(new { message = "Invalid email or password" });

            return Ok(new { token });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null) return NotFound(new { message = "User not found" });

            return Ok(user);
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null) return NotFound(new { message = "User not found" });

            return Ok(user);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            var user = new BaseUser
            {
                UserName = request.UserName,
                Email = request.Email,
                FullName = request.FullName
            };

            var result = await _userRepository.CreateUserAsync(user, request.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new { message = "User created successfully" });
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest request)
        {
            var user = await _userRepository.GetUserByIdAsync(request.Id);
            if (user == null) return NotFound(new { message = "User not found" });

            user.FullName = request.FullName ?? user.FullName;
            user.Email = request.Email ?? user.Email;
            user.UserName = request.UserName ?? user.UserName;

            var result = await _userRepository.UpdateUserAsync(user);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new { message = "User updated successfully" });
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var result = await _userRepository.DeleteUserAsync(id);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new { message = "User deleted successfully" });
        }
    }

    public class CreateUserRequest
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class UpdateUserRequest
    {
        public string Id { get; set; } = string.Empty;
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
    }
}
