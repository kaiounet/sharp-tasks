using Microsoft.AspNetCore.Mvc;
using sharp_tasks.Filters;
using sharp_tasks.Services;

namespace sharp_tasks.Controllers;

[TypeFilter(typeof(ThemeFilter))]
public class ThemeController : Controller
{
    private const string CookieName = "theme";
    private const string LightTheme = "light";
    private const string DarkTheme = "dark";

    public IActionResult Toggle()
    {
        var currentTheme = Request.Cookies[CookieName] ?? LightTheme;
        var newTheme = currentTheme == LightTheme ? DarkTheme : LightTheme;

        Response.Cookies.Append(CookieName, newTheme, new CookieOptions
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