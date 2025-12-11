using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using sharp_tasks.Services;

namespace sharp_tasks.Filters;

public class AuthentificationFilter : ActionFilterAttribute
{
    private readonly IAuthentificationService _authentificationService;

    public AuthentificationFilter(IAuthentificationService authentificationService)
    {
        _authentificationService = authentificationService;
    }

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        base.OnActionExecuted(context);
        if (!_authentificationService.IsAuthentificated())
        {
            context.Result = new RedirectResult("/Authentification/Login");
        }
    }

}
