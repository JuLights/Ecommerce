using Authorization.Domain.Models;

namespace Authorization.Infrastructure.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<UserDb>> GetAllAsync();
}