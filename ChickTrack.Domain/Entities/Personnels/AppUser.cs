using ChickTrack.Base.Domain.Entities;
using ChickTrack.Domain.Entities.Financials;
using Microsoft.AspNetCore.Identity;

namespace ChickTrack.Domain.Entities.Personnels
{
    public class AppUser : BaseUser
    {
        public ICollection<Investment> Investments { get; set; }
    }
}
