using System.Collections;
using MapsterMapper;
using MediatR;
using Products.Application.DTO.Products;
using Products.Application.Queries.Products;
using Products.Infrastructure.Interfaces;
using Shared.Exceptions;

namespace Products.Application.Handlers.Products;

public class GetProductsBySubCategoryIdQueryHandler(IMapper mapper, IProductRepository repository)
    : IRequestHandler<GetProductsBySubCategoryIdQuery,IEnumerable<ResponseProductDto>>
{
    public async Task<IEnumerable<ResponseProductDto>> Handle(GetProductsBySubCategoryIdQuery request, CancellationToken cancellationToken)
    {
        var result = await repository.GetProductsBySubCategoryId(request.SubCategoryId);
        if (!result.Any())
        {
            throw new UserFriendlyException(ErrorMessages.ProductDataNotFound);
        }
        return mapper.Map<IEnumerable<ResponseProductDto>>(result);
    }
}