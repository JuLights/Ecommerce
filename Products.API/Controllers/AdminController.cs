using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Helpers;

namespace Products.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdminController(IMediator mediator, ILogHelper helper) : ControllerBase
{
    
}