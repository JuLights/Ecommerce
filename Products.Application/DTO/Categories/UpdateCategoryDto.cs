
namespace Products.Application.DTO.Categories;

public record UpdateCategoryDto
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public IEnumerable<UpdateSubCategoryDto>? SubCategories { get; set; }
}
public record UpdateSubCategoryDto
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
