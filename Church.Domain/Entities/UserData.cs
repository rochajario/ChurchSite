namespace Church.Domain.Entities
{
    public class UserData : BaseEntity<long>
    {
        public string AspNetUserId { get; set; } = null!;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public bool IsAdmin { get; set; } = false;
        public bool WereAccepted { get; set; } = false;
        public Address Address { get; set; } = null!;
    }
}
