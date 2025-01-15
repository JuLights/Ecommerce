using Shared.Models;

namespace Products.Domain.Models;

public record Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string LongDescription { get; set; } = string.Empty;
    public int ColorId { get; set; }
    public string Color { get; set; } = string.Empty;
    public string Size { get; set; } = string.Empty;
    public IEnumerable<byte[]> Images { get; set; } = [];
}