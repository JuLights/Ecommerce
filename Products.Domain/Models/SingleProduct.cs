using Shared.Models;

namespace Products.Domain.Models;

public record SingleProduct : BaseEntity
{
    public int SubCategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string LongDescription { get; set; } = string.Empty;
    public string Size { get; set; } = string.Empty;
    //quantities
    public List<SingleColorQuantity> ColorQuantities { get; set; } = [];
    //prices
    public bool IsDiscounted { get; set; }
    public decimal DiscountPercentage { get; set; }
    public decimal Price { get; set; }
    
    public IEnumerable<ProductImage> ProductImages { get; set; } = [];
}

public record SingleColorQuantity
{
    public int Quantity { get; set; }
    public int ColorId { get; set; }
    public string? ColorName { get; set; } = string.Empty;
}