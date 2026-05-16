using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Products.Application.Commands.Products;
using Products.Application.DTO.Products;
using Products.Application.Queries.Products;
using Shared.Helpers;

namespace Products.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController(IMediator mediator, ILogHelper logHelper) : ControllerBase
{
    [HttpGet("{page:int}/{amount:int}")]
    public async Task<IActionResult> GetAll(int page, int amount)
    {
        var result = await mediator.Send(new GetAllProductsQuery(page, amount));
        
        logHelper.LogInfo("GetAllProductsQuery executed successfully");
        
        return new JsonResult(result);
    }
    
    [HttpGet("SubCategory/{subCategoryId:int}")]
    public async Task<IActionResult> GetProductsBySubCategoryId(int subCategoryId)
    {
        var result = await mediator.Send(new GetProductsBySubCategoryIdQuery(subCategoryId));
        
        logHelper.LogInfo("GetProductsBySubCategoryIdQuery executed successfully");
        
        return new JsonResult(result);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetSingle(int id)
    {
        var result = await mediator.Send(new GetSingleProductQuery(id));
        
        logHelper.LogInfo("GetSingleProductQuery executed successfully");
        
        return new JsonResult(result);
    }

    [HttpGet("images/{imageName}")]
    public async Task<IActionResult> GetImage(string imageName)
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Products", imageName);

        if (!System.IO.File.Exists(filePath))
        {
            return NotFound();
        }

        var fileExtension = Path.GetExtension(imageName).ToLower();
        var contentType = "application/octet-stream";
        
        if (fileExtension is ".jpg" or ".jpeg")
        {
            contentType = "image/jpeg";
        }
        else if (fileExtension == ".png")
        {
            contentType = "image/png";
        }
        else if (fileExtension == ".gif")
        {
            contentType = "image/gif";
        }
        else if (fileExtension == ".bmp")
        {
            contentType = "image/bmp";
        }

        var imageBytes = await System.IO.File.ReadAllBytesAsync(filePath);
        return File(imageBytes, contentType);
    }
    
    
    //Admin
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromForm] RequestProductDto requestProductDto)
    {
        var result = await mediator.Send(new CreateProductCommand(requestProductDto));
        
        logHelper.LogInfo("CreateProductCommand executed successfully");
        
        return new JsonResult(result);
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update([FromBody] UpdateProductDto updateProductDto)
    {
        var result = await mediator.Send(new UpdateProductCommand(updateProductDto));
        
        logHelper.LogInfo("UpdateProductCommand executed successfully");
        
        return result ? Ok() : BadRequest();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await mediator.Send(new DeleteProductCommand(id));
        
        logHelper.LogInfo("DeleteProductCommand executed successfully");

        return result ? Ok() : BadRequest();
    }
    
}