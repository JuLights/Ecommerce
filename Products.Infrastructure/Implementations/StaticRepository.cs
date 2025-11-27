using System.Data;
using Dapper;
using Products.Domain.Models;
using Products.Infrastructure.Interfaces;
using Shared.Exceptions;

namespace Products.Infrastructure.Implementations;

public class StaticRepository(IDbConnection connection) : IStaticRepository
{
    public async Task<IEnumerable<ColorDb>> GetAllColorsAsync()
    {
        try
        {
            var colors = await connection.QueryAsync<ColorDb>("SELECT Id, Name FROM dbo.Colors");
            return colors;
        }
        catch (Exception ex)
        {
            throw new UserFriendlyException(ErrorMessages.ColorsDataNotFound);
        }
    }

    public Task<IEnumerable<SubCategoryDb>> GetAllSubCategoriesAsync()
    {
        try
        {
            var subCategories = connection.QueryAsync<SubCategoryDb>(
                "SELECT Id, CategoryId, Name, Description, IsDeleted FROM dbo.SubCategories");
            return subCategories;
        }
        catch (Exception ex)
        {
            throw new UserFriendlyException(ErrorMessages.SubCategoriesDataNotFound);
        }
    }
}