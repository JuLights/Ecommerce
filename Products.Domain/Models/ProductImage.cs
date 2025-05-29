using Shared.Models;

namespace Products.Domain.Models;

public record ProductImage : BaseEntity
{
    public int ProductId { get; set; }
    public string ImageName { get; set; } = string.Empty;
    public string ImageExt { get; set; } = string.Empty;
    public string DiskPath { get; set; } = string.Empty;
    public string PublicPath { get; set; } = string.Empty;
}