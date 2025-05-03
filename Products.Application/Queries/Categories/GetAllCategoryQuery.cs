using MediatR;
using Products.Application.DTO.Categories;

namespace Products.Application.Queries.Categories;

public record GetAllCategoryQuery : IRequest<IEnumerable<ResponseCategoryDto>>;