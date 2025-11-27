using Shared.DTO;

namespace Products.Application.DTO.Statics;

public record ResponseSubCategoryDto : BaseDto
{
    public int CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}