using MediatR;
using Products.Application.DTO.Products;

namespace Products.Application.Queries.Products;

public record GetSingleProductQuery(int Id) : IRequest<ResponseSingleProductDto>;