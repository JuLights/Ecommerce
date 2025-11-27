using MediatR;
using Products.Application.DTO.Statics;

namespace Products.Application.Queries.Statics;

public record GetAllColorsQuery : IRequest<IEnumerable<ResponseColorDto>>;