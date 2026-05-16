using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Options;
using Products.Application.DTO.Statics;
using Products.Application.Queries.Statics;
using Products.Infrastructure.Interfaces;
using Services.Interfaces;
using Shared.Models;

namespace Products.Application.Handlers.Statics;

public class GetAllSubCategoriesQueryHandler(IStaticRepository repository, IMapper mapper, ICacheService cacheService, IOptions<AppSettings> _appSettings) :
    IRequestHandler<GetAllSubCategoriesQuery, IEnumerable<ResponseSubCategoryDto>>
{
    public async Task<IEnumerable<ResponseSubCategoryDto>> Handle(GetAllSubCategoriesQuery request, CancellationToken cancellationToken)
    {
        //var subCategories = await repository.GetAllSubCategoriesAsync();
        string cacheKey = _appSettings.Value.CacheSubCategoryKey;

        // Cache for 24 hours since it's static data
        var expiration = TimeSpan.FromHours(_appSettings.Value.CategoryCachingTime);

        // GetOrCreateAsync will check the cache first. 
        // If empty, it runs the repository call, caches the result, and returns it.
        var subCategories = await cacheService.GetOrCreateAsync(
            cacheKey,
            async () => await repository.GetAllSubCategoriesAsync(),
            expiration);


        var mappedSubCategories = mapper.Map<IEnumerable<ResponseSubCategoryDto>>(subCategories);
        return mappedSubCategories;
    }
} 