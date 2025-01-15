namespace Shared.Helpers;

public class ImageHelper
{
    private const string ProductImagesFolder = "ProductImages";
    
    public static async Task<IEnumerable<string>> CreateImageAsync(List<byte[]> images, string name)
    {
        if (!Directory.Exists(ProductImagesFolder))
        {
            Directory.CreateDirectory(ProductImagesFolder);
        }
        
        var savedImagePaths = new List<string>();
        
        foreach (var image in images)
        {
            var fileName = $"{name}_{Guid.NewGuid()}.jpg";
            var filePath = Path.Combine(ProductImagesFolder, fileName);
            
            await File.WriteAllBytesAsync(filePath, image);
            savedImagePaths.Add(filePath);
        }

        return savedImagePaths;
    }
    
    public static async Task<IEnumerable<byte[]>> GetImagesAsync(IEnumerable<string> imagePaths)
    {
        var images = new List<byte[]>();
        
        foreach (var imagePath in imagePaths)
        {
            var image = await File.ReadAllBytesAsync(imagePath);
            images.Add(image);
        }

        return images;
    }
}