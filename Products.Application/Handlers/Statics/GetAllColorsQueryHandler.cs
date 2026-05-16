using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Options;
using Products.Application.DTO.Statics;
using Products.Application.Queries.Statics;
using Products.Infrastructure.Interfaces;
using Services.Interfaces;
using Shared.Models;

namespace Products.Application.Handlers.Statics;

public class GetAllColorsQueryHandler(IStaticRepository repository, IMapper mapper, ICacheService cacheService, IOptions<AppSettings> _appSettings) : 
    IRequestHandler<GetAllColorsQuery, IEnumerable<ResponseColorDto>>
{
    public async Task<IEnumerable<ResponseColorDto>> Handle(GetAllColorsQuery request, CancellationToken cancellationToken)
    {
        //var colors = await repository.GetAllColorsAsync();

        string cacheKey = _appSettings.Value.CacheColorKey;
        var expiration = TimeSpan.FromHours(_appSettings.Value.ColorCachingTime);

        var colors = await cacheService.GetOrCreateAsync(
            cacheKey,
            async () => await repository.GetAllColorsAsync(),
            expiration);

        var mappedColors = mapper.Map<IEnumerable<ResponseColorDto>>(colors);

        return mappedColors;
    }
}