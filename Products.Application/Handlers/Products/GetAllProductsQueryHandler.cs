using MapsterMapper;
using MediatR;
using Products.Application.DTO.Products;
using Products.Application.Queries.Products;
using Products.Infrastructure.Interfaces;

namespace Products.Application.Handlers.Products;

public class GetAllProductsQueryHandler(IProductRepository repository, IMapper mapper) 
    : IRequestHandler<GetAllProductsQuery, IEnumerable<ResponseProductDto>>
{
    public async Task<IEnumerable<ResponseProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var result = await repository.GetAllAsync(request.Page, request.Amount);

        var mappedResult = mapper.Map<IEnumerable<ResponseProductDto>>(result);

        return mappedResult;
    }
}