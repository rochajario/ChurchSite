namespace Church.Domain.Entities
{
    public class Address : BaseEntity<long>
    {
        public string Street { get; set; } = string.Empty;
        public int Number { get; set; }
        public string Complement { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
    }
}
