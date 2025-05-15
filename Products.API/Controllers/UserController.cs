using MediatR;
using Microsoft.AspNetCore.Mvc;
using Products.Application.DTO.Products;
using Shared.Helpers;

namespace Products.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IMediator mediator, ILogHelper logHelper) : ControllerBase
{
    // [HttpPost("AddToCart")]
    // public async Task<IActionResult> AddProductsToCart([FromBody] )
    // {
    //     await mediator.Send(AddProductsToCartCommand())
    //     
    //     logHelper.LogInfo("AddCartCommand executed successfully");
    //     
    //     return new JsonResult(result);
    // }
    //
    // [HttpGet("GetCart")]
    // public async Task<IActionResult> GetCartProducts()
    // {
    //     
    //     
    //     return;
    // }
}