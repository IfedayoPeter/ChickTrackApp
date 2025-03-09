using Microsoft.AspNetCore.Identity;

namespace ChickTrack.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public ICollection<Investment> Investments { get; set; }
    }
}
