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

    public async Task<Product> GetSingle(int id)
    {
        var result = await connection.QuerySingleOrDefaultAsync<Product>(
            "SP_GetSingleProduct",
            new
            {
                Id = id
            },
            commandType: CommandType.StoredProcedure
        );
        
        if(result == null)
            throw new UserFriendlyException(ErrorMessages.ProductDataNotFound);

        return result;
    }

    public async Task<int> CreateAsync(Product product)
    {
        
        var parameters = new DynamicParameters();
        parameters.Add("SubCategoryId", product.SubCategoryId);
        parameters.Add("Name", product.Name);
        parameters.Add("Description", product.Description);
        parameters.Add("LongDescription", product.LongDescription);
        parameters.Add("ColorId", product.ColorId);
        parameters.Add("Size", product.Size);
        parameters.Add("Quantity", product.Quantity);
        parameters.Add("IsDiscounted", product.IsDiscounted);
        parameters.Add("DiscountPercentage", product.DiscountPercentage);
        parameters.Add("Price", product.Price);
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

    public async Task<bool> UpdateAsync(Product product)
    {
        var result = await connection.ExecuteAsync(
            "SP_UpdateProduct",
            new
            {
                Id = product.Id,
                SubCategoryId = product.SubCategoryId,
                Name = product.Name,
                Description = product.Description,
                LongDescription = product.LongDescription,
                ColorId = product.ColorId,
                Size = product.Size,
                Quantity = product.Quantity,
                IsDiscounted = product.IsDiscounted,
                DiscountPercentage = product.DiscountPercentage,
                Price = product.Price
            },
            commandType: CommandType.StoredProcedure
            );
        
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