using MediatR;
using Products.Application.DTO.Products;

namespace Products.Application.Commands.Products;

public record UpdateProductCommand(UpdateProductDto UpdateProductDto) : IRequest<bool>;