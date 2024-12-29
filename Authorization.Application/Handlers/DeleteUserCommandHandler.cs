using Authorization.Application.Commands;
using Authorization.Infrastructure.Interfaces;
using MediatR;
using Shared.Exceptions;

namespace Authorization.Application.Handlers;

public class DeleteUserCommandHandler(IAuthRepository authRepository) : IRequestHandler<DeleteUserCommand, bool>
{
    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await authRepository.GetUserById(request.UserId);

        if (existingUser == null) throw new UserFriendlyException(ErrorMessages.UserNotFound);

        await authRepository.DeleteUser(request.UserId);

        return true;
    }
}