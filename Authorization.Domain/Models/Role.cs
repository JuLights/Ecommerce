using Shared.Models;

namespace Authorization.Domain.Models;

public record Role : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public int RoleCreatorId { get; set; }
    public string CreatorName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
