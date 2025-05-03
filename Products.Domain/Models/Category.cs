using Shared.Models;

namespace Products.Domain.Models;

public record Category : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<SubCategory> SubCategories { get; set; } = [];
}

public record SubCategory : BaseEntity
{
    public int CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}