namespace Base.Repositories.Interfaces
{
    public interface IJwtRepository
    {
        Task<string> GenerateJwtToken(BaseUserLogin baseUserLogin);
    }
}
