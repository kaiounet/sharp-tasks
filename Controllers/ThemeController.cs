using Microsoft.AspNetCore.Mvc;
using sharp_tasks.Constants;
using sharp_tasks.Filters;

namespace sharp_tasks.Controllers;

[TypeFilter(typeof(ThemeFilter))]
public class ThemeController : Controller
{
    public IActionResult Toggle()
    {
        var currentTheme = Request.Cookies[ThemeConstants.CookieName] ?? ThemeConstants.LightTheme;
        var newTheme = currentTheme == ThemeConstants.LightTheme ? ThemeConstants.DarkTheme : ThemeConstants.LightTheme;

        Response.Cookies.Append(ThemeConstants.CookieName, newTheme, new CookieOptions
        {
            HttpOnly = true,
            Secure = false, // set to true in production with HTTPS
            SameSite = SameSiteMode.Strict,
            Path = "/",
            Expires = DateTimeOffset.Now.AddYears(1)
        });

        // Redirect to the home/tasks page for safety
        return RedirectToAction("Index", "Tasks");
    }
}