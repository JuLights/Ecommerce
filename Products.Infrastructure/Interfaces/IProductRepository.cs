using System.Collections;
using Products.Domain.Models;

namespace Products.Infrastructure.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync(int page, int amount);
    Task<IEnumerable<Product>> GetProductsBySubCategoryId(int subCategoryId);
    Task<SingleProduct> GetSingle(int id);
    Task<int> CreateAsync(SingleProduct product);
    Task<bool> UpdateAsync(SingleProduct product);
    Task<bool> DeleteAsync(int id);
}