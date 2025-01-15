using Shared.DTO;

namespace Products.Application.DTO.Products;

public record UpdateProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string LongDescription { get; set; } = string.Empty;
    public int ColorId { get; set; }
    public string Size { get; set; } = string.Empty;
    public IEnumerable<byte[]>? Images { get; set; } = [];
}