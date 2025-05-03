namespace Products.Application.DTO.Products;

public record RequestProductDto
{
    public int SubCategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string LongDescription { get; set; } = string.Empty;
    public int ColorId { get; set; }
    public string Size { get; set; } = string.Empty;
    
    //quanitites
    public int Quantity { get; set; }
    //prices
    public bool IsDiscounted { get; set; }
    public decimal DiscountPercentage { get; set; }
    public decimal Price { get; set; }
    
    public IEnumerable<byte[]>? Images { get; set; } = [];
}