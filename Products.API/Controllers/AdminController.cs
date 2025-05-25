using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Products.Application.Commands.Categories;
using Products.Application.DTO.Categories;
using Shared.Helpers;

namespace Products.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AdminController(IMediator mediator, ILogHelper helper) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] RequestCategoryDto requestCategoryDto)
    {
        var result = await mediator.Send(new CreateCategoryCommand(requestCategoryDto));
        
        if (result)
        {
            helper.LogInfo("Category created successfully");
            return new JsonResult(result)
            {
                StatusCode = 201,
                Value = result
            };
        }
        
        helper.LogInfo("Error creating category");
        return BadRequest();
    }
    
    public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryDto updateCategoryDto)
    {
        var result = await mediator.Send(new UpdateCategoryCommand(updateCategoryDto));
        
        if (result)
        {
            helper.LogInfo("Category updated successfully");
            return new JsonResult(result)
            {
                StatusCode = 200,
                Value = result
            };
        }
        
        helper.LogInfo("UpdateCategory method called");
        return Ok();
    }
}