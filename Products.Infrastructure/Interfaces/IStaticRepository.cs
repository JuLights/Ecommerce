using Products.Domain.Models;

namespace Products.Infrastructure.Interfaces;

public interface IStaticRepository
{
    Task<IEnumerable<ColorDb>> GetAllColorsAsync();
    Task<IEnumerable<SubCategoryDb>> GetAllSubCategoriesAsync();
}