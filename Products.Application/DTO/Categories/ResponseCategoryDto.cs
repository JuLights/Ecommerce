using Shared.DTO;

namespace Products.Application.DTO.Categories;

public record ResponseCategoryDto : BaseDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<ResponseSubCategoryDto> SubCategories { get; set; } = [];
}

public record ResponseSubCategoryDto : BaseDto
{
    public int CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}