using Authorization.Domain.Models;

namespace Authorization.Infrastructure.Interfaces;

public interface IAuthRepository
{
    Task<User> GetUser(string username);
    Task<User?> GetUserById(int userId);
    Task SignUpUser(User user);
    Task UpdateUser(User user);
    Task DeleteUser(int userId);
    Task SaveRefreshToken(int userId, string token, string refreshToken);
    Task<string?> GetRefreshToken(int userId);
}