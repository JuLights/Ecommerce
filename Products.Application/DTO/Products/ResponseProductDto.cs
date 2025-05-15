using Shared.DTO;

namespace Products.Application.DTO.Products;

public record ResponseProductDto : BaseDto
{
    public int SubCategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    // public string LongDescription { get; set; } = string.Empty;
    // public string Color { get; set; } = string.Empty;
    // public string Size { get; set; } = string.Empty;
    
    //quantities
    public int OverallQuantity { get; set; }
    //prices
    public bool IsDiscounted { get; set; }
    public decimal DiscountPercentage { get; set; }
    public decimal Price { get; set; }
    
    public IEnumerable<byte[]> Images { get; set; } = [];
}