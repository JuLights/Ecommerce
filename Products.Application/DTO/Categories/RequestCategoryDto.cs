using Shared.DTO;

namespace Products.Application.DTO.Categories;

public record RequestCategoryDto
{
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public List<RequestSubCategoryDto> SubCategories { get; set; } = [];
}

public record RequestSubCategoryDto
{
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
}
