using System.Data;
using Authorization.Domain.Models;
using Authorization.Infrastructure.Interfaces;
using Dapper;
using Shared.Exceptions;

namespace Authorization.Infrastructure.Implementations;

public class AuthRepository(IDbConnection connection) : IAuthRepository
{
    public async Task<User> GetUser(string username)
    {
        var grid = await connection.QueryMultipleAsync("[dbo].[SP_GetUser]", new
        {
            Username = username
        }, commandType: CommandType.StoredProcedure);

        var user = await grid.ReadSingleOrDefaultAsync<User>();

        if (user == null) throw new UserFriendlyException(ErrorMessages.AuthNotPermitted);

        var roles = (await grid.ReadAsync<Role>()).ToList();

        return user with
        {
            Roles = roles
        };
    }


    public async Task<User?> GetUserById(int userId)
    {
        var grid = await connection.QueryMultipleAsync("[dbo].[SP_GetUserById]", new
        {
            Id = userId
        }, commandType: CommandType.StoredProcedure);

        var user = await grid.ReadSingleOrDefaultAsync<User>();

        if (user == null) throw new UserFriendlyException(ErrorMessages.AuthNotPermitted);

        var roles = (await grid.ReadAsync<Role>()).ToList();
        return user with
        {
            Roles = roles
        };
    }

    public async Task SignUpUser(User user)
    {
        await connection.ExecuteAsync("[dbo].[SP_SignUp]", new
        {
            user.Username,
            user.Password,
            user.FirstName,
            user.LastName,
            user.Email,
            user.PhoneNumber,
            user.ConfirmPassword,
            user.RoleId,
            user.UserGroupId,
            user.DepartmentId
        }, commandType: CommandType.StoredProcedure);
    }


    public async Task UpdateUser(User user)
    {
        await connection.ExecuteAsync("[dbo].[SP_UpdateUser]", new
        {
            user.Id,
            user.Username,
            user.FirstName,
            user.LastName,
            user.Email,
            user.PhoneNumber,
            user.RoleId,
            user.Password,
            user.ConfirmPassword,
            user.UserGroupId,
            user.DepartmentId
        }, commandType: CommandType.StoredProcedure);
    }


    public async Task DeleteUser(int userId)
    {
        await connection.ExecuteAsync("[dbo].[SP_DeleteUser]", new
        {
            Id = userId
        }, commandType: CommandType.StoredProcedure);
    }


    public async Task SaveRefreshToken(int userId, string token, string refreshToken)
    {
        await connection.ExecuteAsync("[dbo].[SP_SaveRefreshToken]", new
        {
            UserId = userId,
            Token = token,
            RefreshToken = refreshToken
        }, commandType: CommandType.StoredProcedure);
    }

    public async Task<string?> GetRefreshToken(int userId)
    {
        var token = await connection.QueryFirstOrDefaultAsync<string>("[dbo].[SP_GetUserRefreshToken]", new
        {
            UserId = userId
        }, commandType: CommandType.StoredProcedure);

        return token;
    }
}