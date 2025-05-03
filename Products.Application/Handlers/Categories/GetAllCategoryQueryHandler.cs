using MapsterMapper;
using MediatR;
using Products.Application.DTO.Categories;
using Products.Application.Queries.Categories;
using Products.Infrastructure.Interfaces;
using Shared.Exceptions;

namespace Products.Application.Handlers.Categories;

public class GetAllCategoryQueryHandler(IMapper mapper, ICategoryRepository repository) : IRequestHandler<GetAllCategoryQuery, IEnumerable<ResponseCategoryDto>>
{
    public async Task<IEnumerable<ResponseCategoryDto>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
    {
        var categories = await repository.GetAllAsync();
        
        if (!categories.Any())
        {
            throw new UserFriendlyException(ErrorMessages.CategoryDataNotFound);
        }
        
        return mapper.Map<IEnumerable<ResponseCategoryDto>>(categories);
    }
}