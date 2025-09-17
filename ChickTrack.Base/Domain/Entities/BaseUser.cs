namespace Base.Domain.Entities
{
    public class BaseUser : IdentityUser
    {
        public string FullName { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public string? CreatedBy { get; set; }

        public DateTime LastModifiedOn { get; set; }

        public string? LastModifiedBy { get; set; }

    }
}