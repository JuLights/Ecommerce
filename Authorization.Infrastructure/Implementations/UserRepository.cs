using System.Data;
using Authorization.Domain.Models;
using Authorization.Infrastructure.Interfaces;
using Dapper;
using Shared.Exceptions;

namespace Authorization.Infrastructure.Implementations;

public class UserRepository(IDbConnection connection) : IUserRepository
{
    public async Task<IEnumerable<UserDb>> GetAllAsync()
    {
        try
        {
            var users = await connection.QueryAsync<UserDb>(
                "SELECT Id, Username, Email, FirstName, LastName, PhoneNumber, IsAdmin, CreateDate, LastUpdateDate FROM dbo.Users"
            );
            return users;
        }
        catch (Exception e)
        {
            throw new UserFriendlyException(ErrorMessages.UsersDataNotFound);
        }
    }
}