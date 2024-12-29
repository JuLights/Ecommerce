using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Shared.Helpers;

public class AuthHelper(IHttpContextAccessor httpContextAccessor)
{
    public virtual int GetUserId()
    {
        var httpContext = httpContextAccessor.HttpContext;
        if (httpContext == null) return -1; 
        
        if (!int.TryParse(httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId))
            return -1;
        return userId;
    }

    public static string? GetUsername(ClaimsPrincipal user)
    {
        return user.Claims.FirstOrDefault(c => c.Type == "Username")?.Value;
    }
}