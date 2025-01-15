using MediatR;
using Products.Application.Commands.Products;
using Products.Infrastructure.Interfaces;

namespace Products.Application.Handlers.Products;

public class DeleteProductCommandHandler(IProductRepository repository) : IRequestHandler<DeleteProductCommand, bool>
{
    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var result = await repository.DeleteAsync(request.Id);

        return result;
    }
}