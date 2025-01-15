using MediatR;
using Products.Application.DTO.Products;

namespace Products.Application.Commands.Products;

public record CreateProductCommand(RequestProductDto ProductDto) : IRequest<int>;