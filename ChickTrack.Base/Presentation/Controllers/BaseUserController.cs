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
            email = email.ToUpper();
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null) return NotFound(new { message = "User not found" });

            return Ok(user);
        }

        [HttpGet("all-users")]
        public async Task<ActionResult<IEnumerable<BaseUser>>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUserAsync();
            return Ok(users);
        }

        [HttpGet]
        public async Task<ActionResult<Result<dynamic>>> GetAllUsers(
        [FromQuery] string search = null,
        [FromQuery] string filter = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string select = null)
        {
            var users = await _userRepository.GetAllUsersAsync(search, filter, page, pageSize, select);
            return Ok(users);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            var user = new BaseUser
            {
                UserName = request.UserName,
                Email = request.Email,
                FullName = request.FullName,
                PhoneNumber = request.PhoneNumber,
                CreatedOn = request.CreatedOn,
                CreatedBy = request.CreatedBy,
                LastModifiedOn = request.LastModifiedOn,
                LastModifiedBy = request.LastModifiedBy,
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
            user.PhoneNumber = request.PhoneNumber ?? user.PhoneNumber;
            user.LastModifiedOn = request.LastModifiedOn;
            user.LastModifiedBy = request.LastModifiedBy;

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
        public string PhoneNumber {  get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; }
        public DateTime LastModifiedOn { get; set; } = DateTime.Now;
        public string? LastModifiedBy { get; set; }

    }

    public class UpdateUserRequest
    {
        public string Id { get; set; } = string.Empty;
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime LastModifiedOn { get; set; } = DateTime.Now;
        public string? LastModifiedBy { get; set; }
    }
}
