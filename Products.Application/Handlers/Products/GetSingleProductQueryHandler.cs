using MapsterMapper;
using MediatR;
using Products.Application.DTO.Products;
using Products.Application.Queries.Products;
using Products.Infrastructure.Interfaces;

namespace Products.Application.Handlers.Products;

public class GetSingleProductQueryHandler(IProductRepository repository, IMapper mapper)
    : IRequestHandler<GetSingleProductQuery, ResponseSingleProductDto>
{
    public async Task<ResponseSingleProductDto> Handle(GetSingleProductQuery request, CancellationToken cancellationToken)
    {
        var result = await repository.GetSingle(request.Id);

        var mappedResult = mapper.Map<ResponseSingleProductDto>(result);
        
        return mappedResult;
    }
}