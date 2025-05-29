using Shared.Models;

namespace Products.Domain.Models;

public record Product : BaseEntity
{
    public int SubCategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    
    //quantities
    public int OverallQuantity { get; set; }
    //prices
    public bool IsDiscounted { get; set; }
    public decimal DiscountPercentage { get; set; }
    public decimal Price { get; set; }
    
    public IEnumerable<string> ProductImages { get; set; } = []; //links
}