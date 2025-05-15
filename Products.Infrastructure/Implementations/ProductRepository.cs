using System.Data;
using Dapper;
using Products.Domain.Models;
using Products.Infrastructure.Interfaces;
using Shared.Exceptions;

namespace Products.Infrastructure.Implementations;

public class ProductRepository(IDbConnection connection) : IProductRepository
{
    public async Task<IEnumerable<Product>> GetAllAsync(int page, int amount)
    {
        var result = await connection.QueryAsync<Product>(
            "SP_GetAllProducts",
            new
            {
                Page = page,
                Amount = amount
            },
            commandType: CommandType.StoredProcedure
        );


        if (!result.Any())
            throw new UserFriendlyException(ErrorMessages.ProductsDataNotFound);

        return result;
    }

    public async Task<IEnumerable<Product>> GetProductsBySubCategoryId(int subCategoryId)
    {
        var products = await connection.QueryAsync<Product>(
            "SP_GetProductsBySubCategoryId",
            new
            {
                SubCategoryId = subCategoryId
            },
            commandType: CommandType.StoredProcedure
        );

        return products;
    }

    public async Task<SingleProduct> GetSingle(int id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", id);

        using var multi = await connection.QueryMultipleAsync(
            "SP_GetSingleProduct",
            parameters,
            commandType: CommandType.StoredProcedure
        );

        var product = await multi.ReadFirstOrDefaultAsync<SingleProduct>();
        if (product == null)
            throw new UserFriendlyException(ErrorMessages.ProductDataNotFound);

        var colorQuantities = (await multi.ReadAsync<SingleColorQuantity>()).ToList();
        product.ColorQuantities = colorQuantities;

        return product;
    }

    public async Task<int> CreateAsync(SingleProduct product)
    {
        int overallQuantity = 0;
        
        // Create DataTable for ColorQuantities
        var colorQuantitiesTable = new DataTable();
        colorQuantitiesTable.Columns.Add("Quantity", typeof(int));
        colorQuantitiesTable.Columns.Add("ColorId", typeof(int));
        colorQuantitiesTable.Columns.Add("ColorName", typeof(string));
        
        var parameters = new DynamicParameters();
        parameters.Add("SubCategoryId", product.SubCategoryId);
        parameters.Add("Name", product.Name);
        parameters.Add("Description", product.Description);
        parameters.Add("LongDescription", product.LongDescription);
        parameters.Add("Size", product.Size);
        
        foreach (var colorQty in product.ColorQuantities)
        {
            overallQuantity += colorQty.Quantity; // for overall quantity
            
            colorQuantitiesTable.Rows.Add(colorQty.Quantity, colorQty.ColorId, colorQty.ColorName);
        }
        
        parameters.Add("OverallQuantity", overallQuantity);
        parameters.Add("IsDiscounted", product.IsDiscounted);
        parameters.Add("DiscountPercentage", product.DiscountPercentage);
        parameters.Add("Price", product.Price);
        
        // Add the DataTable as a parameter
        parameters.Add("ColorQuantities", colorQuantitiesTable.AsTableValuedParameter("dbo.ColorQuantityTableType"));
        //output id
        parameters.Add("ProductId", dbType: DbType.Int32, direction: ParameterDirection.Output);
        
        await connection.ExecuteAsync(
            "SP_CreateProduct",
            parameters,
            commandType: CommandType.StoredProcedure
        );
        
        var productId = parameters.Get<int>("ProductId");

        if (productId < 1)
            throw new UserFriendlyException(ErrorMessages.ProductNotCreated);
        
        return productId;
    }

    public async Task<bool> UpdateAsync(SingleProduct product)
    {
        int overallQuantity = 0;
        
        // Create DataTable for ColorQuantities
        var colorQuantitiesTable = new DataTable();
        colorQuantitiesTable.Columns.Add("Quantity", typeof(int));
        colorQuantitiesTable.Columns.Add("ColorId", typeof(int));
        colorQuantitiesTable.Columns.Add("ColorName", typeof(string));
        
        var parameters = new DynamicParameters();
        parameters.Add("Id", product.Id);
        parameters.Add("SubCategoryId", product.SubCategoryId);
        parameters.Add("Name", product.Name);
        parameters.Add("Description", product.Description);
        parameters.Add("LongDescription", product.LongDescription);
        parameters.Add("Size", product.Size);
        
        foreach (var colorQty in product.ColorQuantities)
        {
            overallQuantity += colorQty.Quantity; // for overall quantity
            
            colorQuantitiesTable.Rows.Add(colorQty.Quantity, colorQty.ColorId, colorQty.ColorName);
        }
        
        parameters.Add("OverallQuantity", overallQuantity);
        parameters.Add("IsDiscounted", product.IsDiscounted);
        parameters.Add("DiscountPercentage", product.DiscountPercentage);
        parameters.Add("Price", product.Price);
        
        // Add the DataTable as a parameter
        parameters.Add("ColorQuantities", colorQuantitiesTable.AsTableValuedParameter("dbo.ColorQuantityTableType"));
        
        
        var result = await connection.ExecuteAsync(
            "SP_UpdateProduct",
            parameters,
            commandType: CommandType.StoredProcedure);
        
        if (result < 1)
            throw new UserFriendlyException(ErrorMessages.ProductNotUpdated);

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var result = await connection.ExecuteAsync(
            "SP_DeleteProduct",
            new
            {
                Id = id
            },
            commandType: CommandType.StoredProcedure
        );
        
        if(result < 1)
            throw new UserFriendlyException(ErrorMessages.ProductNotDeleted);
        
        return true;
    }
}