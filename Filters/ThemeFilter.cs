using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using sharp_tasks.Services;

namespace sharp_tasks.Filters;

public class ThemeFilter : ActionFilterAttribute
{
    private readonly IThemeProvider _themeProvider;

    public ThemeFilter(IThemeProvider themeProvider)
    {
        _themeProvider = themeProvider;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var theme = _themeProvider.GetTheme();
        if (context.Controller is Controller controller)
        {
            controller.ViewBag.Theme = theme;
        }
        base.OnActionExecuting(context);
    }
}