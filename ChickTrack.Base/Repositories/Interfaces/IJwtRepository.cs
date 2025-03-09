using ChickTrack.Base.Domain.Entities;

namespace ChickTrack.Base.Repositories.Interfaces
{
    public interface IJwtRepository
    {
        Task<string> GenerateJwtToken(BaseUserLogin baseUserLogin);
    }
}
