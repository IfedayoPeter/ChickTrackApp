using ChickTrack.Base.Domain.Entities;
using ChickTrack.Base.Presentation.Controllers;
using ChickTrack.Base.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChickTrack.API.Controllers
{
    ////[Authorize(Roles = "Admin")]
    //[Route("api/[controller]")]
    //[ApiController]
    //public class UserController : BaseUserController
    //{
    //    public UserController(IUserRepository userRepository) : base(userRepository)
    //    {
    //    }

        //[HttpGet("me")]
        //public IActionResult GetCurrentUser()
        //{
        //    var user = HttpContext.User;
        //    return Ok(user);
        //}

        //[HttpGet("{id}")]
        //public async Task<ActionResult> GetUserById(string request)
        //{
        //    var user = await GetUserById(request);
        //    return Ok(user);
        //}

        //[HttpGet]
        //public async Task<ActionResult> GetUserByEmail(string request)
        //{
        //    var user = await GetUserByEmail(request);
        //    return Ok(user);
        //}

        //[HttpPost]
        //public async Task<ActionResult> CreateUser([FromBody] BaseUser request, string password)
        //{
        //    var response = await CreateUser(request, password);

        //    return Ok(response);
        //}

        //[HttpPut("update")]
        //public async Task<ActionResult> UpdateUser([FromBody] UpdateUserRequest request)
        //{
        //    var response = await UpdateUser(request);

        //    return Ok(response);
        //}

        //[HttpDelete("delete/{id}")]
        //public async Task<ActionResult> DeleteUser(string request)
        //{
        //    var response = await DeleteUser(request);
        //    return Ok(response);
        //}

        //[HttpPost("login")]
        //public async Task<ActionResult> Login([FromBody] BaseUserLogin request)
        //{
        //    var response = await Login(request);
        //    return Ok(response);
        //}

    //    private class UpdateUserRequest
    //    {
    //        public string Id { get; set; } = string.Empty;
    //        public string? FullName { get; set; }
    //        public string? Email { get; set; }
    //        public string? UserName { get; set; }
    //    }
    //}
}
