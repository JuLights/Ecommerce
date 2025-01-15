using MapsterMapper;
using MediatR;
using Products.Application.Commands.Products;
using Products.Domain.Models;
using Products.Infrastructure.Interfaces;

namespace Products.Application.Handlers.Products;

public class UpdateProductCommandHandler(IProductRepository repository, IMapper mapper) 
    : IRequestHandler<UpdateProductCommand,bool>
{
    public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var mappedProduct = mapper.Map<Product>(request.UpdateProductDto);
        
        var result = await repository.UpdateAsync(mappedProduct);
        
        return result;
    }
}