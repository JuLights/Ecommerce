using Products.Domain.Models;

namespace Products.Infrastructure.Interfaces;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllAsync();
}