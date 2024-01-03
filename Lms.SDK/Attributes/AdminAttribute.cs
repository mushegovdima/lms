using Lms.SDK.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Lms.SDK.Attributes;

public class AdminAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (context.HttpContext.User.IsAdmin())
            return;

        context.Result = new StatusCodeResult(403); // Forbidden
    }
}
