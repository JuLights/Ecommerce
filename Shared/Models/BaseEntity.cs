
namespace Shared.Models;

public abstract record BaseEntity
{
    public int Id { get; init; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; set; }
    public DateTime DeletedAt { get; set; }
}