using Shared.DTO;

namespace Products.Application.DTO.Products;

public record ResponseProductDto : BaseDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string LongDescription { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public string Size { get; set; } = string.Empty;
    public IEnumerable<byte[]> Images { get; set; } = [];
}