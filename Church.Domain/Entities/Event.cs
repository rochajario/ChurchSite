using Church.Domain.Enums;

namespace Church.Domain.Entities
{
    public class Event : BaseEntity<long>
    {
        public DateTime Date { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public EventType Type { get; set; }
        public Address Address { get; set; } = null!;
    }
}
