using Shared.DTO;

namespace Products.Application.DTO.Categories;

public record RequestCategoryDto : BaseDto
{
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public List<RequestSubCategoryDto> SubCategories { get; set; } = [];
}

public record RequestSubCategoryDto : BaseDto
{
    public int CategoryId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
}
