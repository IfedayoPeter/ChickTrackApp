namespace Base.Presentation.Controllers
{
    [Authorize(Policy = "AdminOnly")]
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
        [Authorize(Policy = "SuperAdminOnly")]
        public async Task<IActionResult> DeleteRole([FromBody] string roleName)
        {
            var result = await _roleRepository.DeleteRole(roleName);
            return Ok(new { message = result });
        }

        [HttpPost("assign-role")]
        [Authorize(Policy = "SuperAdminOnly")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleRequest request)
        {
            var result = await _roleRepository.AssignRole(request.Email, request.RoleName);
            return Ok(new { message = result });
        }

        [HttpDelete("remove-user-role")]
        [Authorize(Policy = "SuperAdminOnly")]
        public async Task<IActionResult> DeleteUserRole([FromBody] AssignRoleRequest request)
        {
            var result = await _roleRepository.DeleteUserRole(request.Email, request.RoleName);
            return Ok(new { message = result });
        }

        [HttpDelete("delete-user")]
        [Authorize(Policy = "SuperAdminOnly")]
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
