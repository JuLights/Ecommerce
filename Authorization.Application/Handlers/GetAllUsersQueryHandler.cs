using Authorization.Application.DTO;
using Authorization.Application.Queries;
using Authorization.Infrastructure.Interfaces;
using MapsterMapper;
using MediatR;

namespace Authorization.Application.Handlers;

public class GetAllUsersQueryHandler(IUserRepository repository, IMapper mapper) : 
    IRequestHandler<GetAllUsersQuery, IEnumerable<UserDto>>
{
    public async Task<IEnumerable<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await repository.GetAllAsync();
        
        return mapper.Map<IEnumerable<UserDto>>(users);
    }
}