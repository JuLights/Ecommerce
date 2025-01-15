using Products.Domain.Models;

namespace Products.Infrastructure.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync(int page, int amount);
    Task<Product> GetSingle(int id);
    Task<int> CreateAsync(Product product);
    Task<bool> UpdateAsync(Product product);
    Task<bool> DeleteAsync(int id);
}