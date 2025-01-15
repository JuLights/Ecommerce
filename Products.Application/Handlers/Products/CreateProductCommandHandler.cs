using MapsterMapper;
using MediatR;
using Products.Application.Commands.Products;
using Products.Domain.Models;
using Products.Infrastructure.Interfaces;

namespace Products.Application.Handlers.Products;

public class CreateProductCommandHandler(IProductRepository repository, IMapper mapper)
    : IRequestHandler<CreateProductCommand, int>
{
    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var mappedProduct = mapper.Map<Product>(request.ProductDto);
        
        var result = await repository.CreateAsync(mappedProduct);

        return result;
    }
}