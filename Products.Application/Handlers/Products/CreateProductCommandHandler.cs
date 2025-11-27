using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Products.Application.Commands.Products;
using Products.Application.DTO.Products;
using Products.Domain.Models;
using Products.Infrastructure.Interfaces;

namespace Products.Application.Handlers.Products;

public class CreateProductCommandHandler(IProductRepository repository, IMapper mapper, IConfiguration configuration)
    : IRequestHandler<CreateProductCommand, int>
{
    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var mappedProduct = mapper.Map<SingleProduct>(request.ProductDto);
        //
        // TODO: need to handle images
        // TODO: 1. Need to generate: Unique Image Name, Get and extension
        // TODO: 2. Save on Disk: get DiskPath
        // TODO: 3. Need to write service to retrieve image from disk, image link from our server

        mappedProduct.ColorQuantities = new List<SingleColorQuantity>();
        
        IEnumerable<RequestSingleColorQuantity> colorQuantities =
            JsonConvert.DeserializeObject<IEnumerable<RequestSingleColorQuantity>>(
                request.ProductDto.ColorQuantitiesJson) ?? [];

        foreach (var colorQuantity in colorQuantities)
        {
            mappedProduct.ColorQuantities.Add(new SingleColorQuantity()
            {
                ColorId = colorQuantity.ColorId,
                Quantity = colorQuantity.Quantity
            });
        }
        
        if (request.ProductDto.Images != null)
        {
            string curDir = Directory.GetCurrentDirectory(); 
            string productsDir = Path.Combine(curDir, "Products");
        
            if (!Directory.Exists(productsDir))
                Directory.CreateDirectory(productsDir);
        
            var productImages = new List<ProductImage>();
            foreach (var image in request.ProductDto.Images)
            {
                var imageName = Path.GetFileNameWithoutExtension(image.FileName);
                var imageExt = Path.GetExtension(image.FileName).ToLower();
                var imageDiskPath = Path.Combine(productsDir, image.FileName);
                var publicPath = $"images/{imageName}" +
                                 $"{imageExt}";
                
                await using var stream = new FileStream(imageDiskPath, FileMode.Create);
                await image.CopyToAsync(stream);

                productImages.Add(new ProductImage()
                {
                    ImageName = imageName,
                    ImageExt = imageExt,
                    DiskPath = imageDiskPath,
                    PublicPath = publicPath
                });
                
            }
            mappedProduct.ProductImages = productImages;
        }
        
        //
        var result = await repository.CreateAsync(mappedProduct);

        return result;
    }
}