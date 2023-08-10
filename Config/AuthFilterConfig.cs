using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Artifactan.Config;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthFilterConfig : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = (string?)context.HttpContext.Items["User"];
        if (user == null)
        {
            // not logged in or role not authorized
            context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }
    }
}