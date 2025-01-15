using MediatR;
using Products.Application.DTO.Products;

namespace Products.Application.Queries.Products;

public record GetAllProductsQuery(int Page, int Amount) : IRequest<IEnumerable<ResponseProductDto>>;