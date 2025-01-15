using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Products.Application.Commands.Products;
using Products.Application.DTO.Products;
using Products.Application.Queries.Products;
using Shared.Helpers;

namespace Products.API.Controllers;

[Route("api/[controller]")]
[Authorize]
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
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetSingle(int id)
    {
        var result = await mediator.Send(new GetSingleProductQuery(id));
        
        logHelper.LogInfo("GetSingleProductQuery executed successfully");
        
        return new JsonResult(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RequestProductDto requestProductDto)
    {
        var result = await mediator.Send(new CreateProductCommand(requestProductDto));
        
        logHelper.LogInfo("CreateProductCommand executed successfully");
        
        return new JsonResult(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateProductDto updateProductDto)
    {
        var result = await mediator.Send(new UpdateProductCommand(updateProductDto));
        
        logHelper.LogInfo("UpdateProductCommand executed successfully");
        
        return result ? Ok() : BadRequest();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await mediator.Send(new DeleteProductCommand(id));
        
        logHelper.LogInfo("DeleteProductCommand executed successfully");

        return result ? Ok() : BadRequest();
    }
    
}