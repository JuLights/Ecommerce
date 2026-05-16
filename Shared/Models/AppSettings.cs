namespace Shared.Models;

public class AppSettings
{
    public int DefaultProductId { get; set; }
    public string CacheColorKey { get; set; } = string.Empty;
    public int ColorCachingTime { get; set; }
    public string CacheCategoryKey { get; set; } = string.Empty;
    public int CategoryCachingTime { get; set; }
    public string CacheSubCategoryKey { get; set; } = string.Empty;
}
