using ChickTrack.Base.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace ChickTrack.Base.Presentation.Controllers
{
        [Authorize(Roles = "Admin")] // Restrict to Admins only
        [Route("api/[controller]")]
        [ApiController]
        public class RoleController : ControllerBase
        {
            private readonly IRoleRepository _roleRepository;

            public RoleController(IRoleRepository roleRepository)
            {
                _roleRepository = roleRepository;
            }

            [HttpPost("create")]
            public async Task<IActionResult> CreateRole([FromBody] string roleName)
            {
                var result = await _roleRepository.CreateRole(roleName);
                return Ok(new { message = result });
            }

            [HttpDelete("delete")]
            public async Task<IActionResult> DeleteRole([FromBody] string roleName)
            {
                var result = await _roleRepository.DeleteRole(roleName);
                return Ok(new { message = result });
            }

            [HttpPost("assign-role")]
            public async Task<IActionResult> AssignRole([FromBody] AssignRoleRequest request)
            {
                var result = await _roleRepository.AssignRole(request.Email, request.RoleName);
                return Ok(new { message = result });
            }

            [HttpDelete("remove-user-role")]
            public async Task<IActionResult> DeleteUserRole([FromBody] AssignRoleRequest request)
            {
                var result = await _roleRepository.DeleteUserRole(request.Email, request.RoleName);
                return Ok(new { message = result });
            }

            [HttpDelete("delete-user")]
            public async Task<IActionResult> DeleteUserAccount([FromBody] string email)
            {
                var result = await _roleRepository.DeleteUserAccount(email);
                return Ok(new { message = result });
            }
        }

        public class AssignRoleRequest
        {
            public string Email { get; set; } = string.Empty;
            public string RoleName { get; set; } = string.Empty;
        }
}
