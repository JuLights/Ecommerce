using Authorization.Application.DTO;
using MediatR;

namespace Authorization.Application.Queries;

public record GetAllUsersQuery : IRequest<IEnumerable<UserDto>>;