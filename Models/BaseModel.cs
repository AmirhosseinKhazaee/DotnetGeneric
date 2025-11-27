public abstract class BaseModel
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public DateTime createdAt { get; set; }
}
