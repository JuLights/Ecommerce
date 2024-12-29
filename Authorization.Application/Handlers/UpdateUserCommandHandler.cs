using Authorization.Application.Commands;
using Authorization.Domain.Models;
using Authorization.Infrastructure.Interfaces;
using MapsterMapper;
using MediatR;
using Shared.Exceptions;

namespace Authorization.Application.Handlers;

public class UpdateUserCommandHandler(IAuthRepository authRepository,
    IMapper mapper) : IRequestHandler<UpdateUserCommand, bool>
{
    public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await authRepository.GetUserById(request.User.Id);

        if (existingUser == null) throw new UserFriendlyException(ErrorMessages.UserNotFound);

        var updatedUser = existingUser with
        {
            Username = request.User.Username,
            FirstName = request.User.FirstName,
            LastName = request.User.LastName,
            Email = request.User.Email,
            PhoneNumber = request.User.PhoneNumber,
            RoleId = request.User.RoleId,
            UserGroupId = request.User.UserGroupId,
            DepartmentId = request.User.DepartmentId
        };

        var mappedUser = mapper.Map<User>(updatedUser);

        await authRepository.UpdateUser(mappedUser);

        return true;
    }
}