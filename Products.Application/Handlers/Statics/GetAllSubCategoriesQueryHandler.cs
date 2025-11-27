using MapsterMapper;
using MediatR;
using Products.Application.DTO.Statics;
using Products.Application.Queries.Statics;
using Products.Infrastructure.Interfaces;

namespace Products.Application.Handlers.Statics;

public class GetAllSubCategoriesQueryHandler(IStaticRepository repository, IMapper mapper) :
    IRequestHandler<GetAllSubCategoriesQuery, IEnumerable<ResponseSubCategoryDto>>
{
    public async Task<IEnumerable<ResponseSubCategoryDto>> Handle(GetAllSubCategoriesQuery request, CancellationToken cancellationToken)
    {
        var subCategories = await repository.GetAllSubCategoriesAsync();
        var mappedSubCategories = mapper.Map<IEnumerable<ResponseSubCategoryDto>>(subCategories);
        return mappedSubCategories;
    }
} 