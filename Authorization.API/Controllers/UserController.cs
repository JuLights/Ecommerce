using Authorization.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authorization.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class UserController(IMediator mediator) : ControllerBase
{
    [Authorize(Roles = "Admin")]
    [HttpGet("getallusers")]
    public async Task<IActionResult> GetUserRoles()
    {
        var users = await mediator.Send(new GetAllUsersQuery());
        
        return new JsonResult(value:
        new {
            users = users,
            total = users.Count()
        });
    }
}