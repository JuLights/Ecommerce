using System.Data;
using Dapper;
using Products.Domain.Models;
using Products.Infrastructure.Interfaces;

namespace Products.Infrastructure.Implementations;

public class CategoryRepository(IDbConnection connection) : ICategoryRepository
{
    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        using var multi = await connection.QueryMultipleAsync(
            "SP_GetAllCategory",
            commandType: CommandType.StoredProcedure
        );
        
        var categories = (await multi.ReadAsync<Category>()).ToList();
        var subCategories = (await multi.ReadAsync<SubCategory>()).ToList();
        
        foreach (var cat in categories)
        {
            cat.SubCategories = [];
            foreach (var sub in subCategories.Where(sub => sub.CategoryId == cat.Id))
            {
                cat.SubCategories.Add(sub);
            }
        }
        
        return categories;
    }
}