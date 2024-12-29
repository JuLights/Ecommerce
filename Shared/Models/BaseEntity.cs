namespace Shared.Models;

public abstract record BaseEntity
{
    public int Id { get; init; }
    public bool IsDeleted { get; set; }
}