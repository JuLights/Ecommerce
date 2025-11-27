using MediatR;
using Products.Application.DTO.Statics;

namespace Products.Application.Queries.Statics;

public record GetAllSubCategoriesQuery : IRequest<IEnumerable<ResponseSubCategoryDto>>;