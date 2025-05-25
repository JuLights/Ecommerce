using Products.Domain.Models;

namespace Products.Infrastructure.Interfaces;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllAsync();
    Task<bool> CreateAsync(Category category);
    Task<bool> UpdateAsync(Category category);
}