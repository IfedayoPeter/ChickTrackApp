namespace Base.Domain.Entities
{
    public class BaseUserLogin
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
