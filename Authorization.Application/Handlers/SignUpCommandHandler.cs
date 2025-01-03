using Authorization.Application.Commands;
using Authorization.Infrastructure;
using Authorization.Infrastructure.Interfaces;
using MapsterMapper;
using MediatR;
using Shared.Exceptions;

namespace Authorization.Application.Handlers;

public class SignUpCommandHandler(IAuthRepository authRepository, IMapper mapper)
    : IRequestHandler<SignUpCommand>
{
    public async Task Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        var passwordHash = PasswordService.HashPassword(request.User.Password);

        request.User = request.User with { Password = passwordHash, ConfirmPassword = passwordHash };
        var mappedUser = mapper.Map<Domain.Models.User>(request.User);
        // if (!request.User.IsValidPassword()) throw new UserFriendlyException(ErrorMessages.InvalidPassword);

        await authRepository.SignUpUser(mappedUser);
    }
}