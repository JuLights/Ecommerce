using MediatR;

namespace Products.Application.Commands.Products;

public record DeleteProductCommand(int Id) : IRequest<bool>;