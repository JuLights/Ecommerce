using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Options;
using Products.Application.DTO.Categories;
using Products.Application.Queries.Categories;
using Products.Infrastructure.Interfaces;
using Services.Interfaces;
using Shared.Exceptions;
using Shared.Models;

namespace Products.Application.Handlers.Categories;

public class GetAllCategoryQueryHandler(
    IMapper mapper, 
    ICategoryRepository repository, 
    ICacheService cacheService,
    IOptions<AppSettings> _appSettings) : IRequestHandler<GetAllCategoryQuery, IEnumerable<ResponseCategoryDto>>
{
    public async Task<IEnumerable<ResponseCategoryDto>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
    {
        //var categories = await repository.GetAllAsync();

        string cacheKey = _appSettings.Value.CacheCategoryKey;

        var expiration = TimeSpan.FromHours(_appSettings.Value.CategoryCachingTime);

        var categories = await cacheService.GetOrCreateAsync(
            cacheKey,
            async () => await repository.GetAllAsync(),
            expiration);
        
        if (!categories.Any())
        {
            throw new UserFriendlyException(ErrorMessages.CategoryDataNotFound);
        }
        
        return mapper.Map<IEnumerable<ResponseCategoryDto>>(categories);
    }
}