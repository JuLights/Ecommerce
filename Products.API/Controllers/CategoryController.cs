using MediatR;
using Microsoft.AspNetCore.Mvc;
using Products.Application.Queries.Categories;
using Shared.Helpers;

namespace Products.API.Controllers;


[Route("api/[controller]")]
[ApiController]
public class CategoryController(IMediator mediator, ILogHelper logHelper) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await mediator.Send(new GetAllCategoryQuery());
        
        logHelper.LogInfo("GetAllMenuQuery executed successfully");
        
        return new JsonResult(result);
    }
}