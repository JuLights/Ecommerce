using Shared.DTO;

namespace Products.Application.DTO.Products;

public record ResponseSingleProductDto : BaseDto
{
    public int SubCategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string LongDescription { get; set; } = string.Empty;
    public string Size { get; set; } = string.Empty;
    public IEnumerable<ResponseColorQuantity> ColorQuantities { get; set; } = [];
    //prices
    public bool IsDiscounted { get; set; }
    public decimal DiscountPercentage { get; set; }
    public decimal Price { get; set; }
    
    public IEnumerable<string>? ImageLinks { get; set; } = [];
}

public record ResponseColorQuantity
{
    public int Quantity { get; set; }
    public int ColorId { get; set; }
    public string ColorName { get; set; } = string.Empty;
}