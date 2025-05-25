using System.Data;
using Dapper;
using Products.Domain.Models;
using Products.Infrastructure.Interfaces;
using Shared.Exceptions;

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

    public async Task<bool> CreateAsync(Category category)
    {
        var dataTable = new DataTable();
        dataTable.Columns.Add("CategoryId", typeof(int));
        dataTable.Columns.Add("Name", typeof(string));
        dataTable.Columns.Add("Description", typeof(string));
        
        foreach (var subCategory in category.SubCategories)
        {
            dataTable.Rows.Add(
                subCategory.CategoryId,
                subCategory.Name,
                subCategory.Description
            );
        }
        
        var parameters = new DynamicParameters();
        parameters.Add("Name", category.Name);
        parameters.Add("Description", category.Description);
        parameters.Add("SubCategories", dataTable.AsTableValuedParameter("dbo.SubCategoryType"));
        
        var result = await connection.ExecuteAsync(
            "SP_CreateCategory",
            parameters,
            commandType: CommandType.StoredProcedure
        );

        if (result >= 1)
            return true;

        throw new UserFriendlyException(ErrorMessages.CategoryNotCreated);
    }

    public async Task<bool> UpdateAsync(Category category)
    {
        var dataTable = new DataTable();
        if (category.SubCategories.Any())
        {
            dataTable.Columns.Add("Id", typeof(int));
            dataTable.Columns.Add("CategoryId", typeof(int));
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Description", typeof(string));
            foreach (var subCategory in category.SubCategories)
            {
                dataTable.Rows.Add(
                    subCategory.CategoryId,
                    subCategory.Name,
                    subCategory.Description
                );
            }
        }
        
        var parameters = new DynamicParameters();
        parameters.Add("Id", category.Id);
        parameters.Add("Name", category.Name);
        parameters.Add("Description", category.Description);
        parameters.Add("SubCategories", dataTable.AsTableValuedParameter("dbo.SubCategoryType"));
        
        var result = await connection.ExecuteAsync(
            "SP_UpdateCategory",
            parameters,
            commandType: CommandType.StoredProcedure
        );

        if (result >= 1)
            return true;

        throw new UserFriendlyException(ErrorMessages.CategoryNotUpdated);
        
    }
}