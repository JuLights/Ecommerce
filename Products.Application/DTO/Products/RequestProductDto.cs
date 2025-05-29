using Microsoft.AspNetCore.Http;

namespace Products.Application.DTO.Products;

public record RequestProductDto
{
    public int SubCategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string LongDescription { get; set; } = string.Empty;
    public string Size { get; set; } = string.Empty;
    
    public IEnumerable<RequestSingleColorQuantity> ColorQuantities { get; set; } = [];
    //quanitites
    // public int OverallQuantity { get; set; } // not needed, calculated in repository or in handler
    //prices
    public bool IsDiscounted { get; set; }
    public decimal DiscountPercentage { get; set; }
    public decimal Price { get; set; }
    
    public IEnumerable<IFormFile>? Images { get; set; } = [];
}

public record RequestSingleColorQuantity
{
    public int Quantity { get; set; }
    public int ColorId { get; set; }
}