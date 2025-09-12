namespace Base.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Task<string> CreateRole(string name);
        Task<string> DeleteRole(string name);
        Task<string> AssignRole(string email, string roleName);
        Task<string> DeleteUserRole(string email, string roleName);
        Task<string> DeleteUserAccount(string email);
    }
}
