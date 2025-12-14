using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using sharp_tasks.Services;

namespace sharp_tasks.Filters;

public class AuthenticationFilter : ActionFilterAttribute
{
    private readonly IAuthenticationProvider _authenticationService;

    public AuthenticationFilter(IAuthenticationProvider authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);
        if (!_authenticationService.IsAuthenticated())
        {
            context.Result = new RedirectResult("/Authentication/Login");
        }
    }

}
