using MediatR;
using Microsoft.AspNetCore.Mvc;
using Products.Application.Queries.Statics;
using Shared.Helpers;

namespace Products.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StaticsController(IMediator mediator, ILogHelper logHelper) : ControllerBase
{
    [HttpGet("getcolors")]
    public async Task<IActionResult> GetColors()
    {
        var result = await mediator.Send(new GetAllColorsQuery());
        
        logHelper.LogInfo("GetAllColorsQuery executed successfully");
        
        return new JsonResult(result);
    }
    
    [HttpGet("getsubcategories")]
    public async Task<IActionResult> GetSubCategories()
    {
        var result = await mediator.Send(new GetAllSubCategoriesQuery());
        
        logHelper.LogInfo("GetAllSubCategoriesQuery executed successfully");
        
        return new JsonResult(result);
    }
    
    
}