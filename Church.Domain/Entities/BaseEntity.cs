namespace Church.Domain.Entities
{
    public abstract class BaseEntity<T>
    {
        public T Id { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
