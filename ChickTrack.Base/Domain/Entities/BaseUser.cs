using Microsoft.AspNetCore.Identity;

namespace ChickTrack.Base.Domain.Entities
{
    public class BaseUser : IdentityUser
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}