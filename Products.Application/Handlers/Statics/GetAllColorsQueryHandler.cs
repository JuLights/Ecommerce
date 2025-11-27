using MapsterMapper;
using MediatR;
using Products.Application.DTO.Statics;
using Products.Application.Queries.Statics;
using Products.Infrastructure.Interfaces;

namespace Products.Application.Handlers.Statics;

public class GetAllColorsQueryHandler(IStaticRepository repository, IMapper mapper) : 
    IRequestHandler<GetAllColorsQuery, IEnumerable<ResponseColorDto>>
{
    public async Task<IEnumerable<ResponseColorDto>> Handle(GetAllColorsQuery request, CancellationToken cancellationToken)
    {
        var colors = await repository.GetAllColorsAsync();
        
        var mappedColors = mapper.Map<IEnumerable<ResponseColorDto>>(colors);

        return mappedColors;
    }
}