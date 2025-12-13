using Microsoft.AspNetCore.Http;
using sharp_tasks.Constants;

namespace sharp_tasks.Services;

public class ThemeService : IThemeProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ThemeService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetTheme()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext?.Request.Cookies.TryGetValue(ThemeConstants.CookieName, out var cookieTheme) == true)
        {
            if (cookieTheme == ThemeConstants.LightTheme || cookieTheme == ThemeConstants.DarkTheme)
            {
                return cookieTheme;
            }
        }
        return ThemeConstants.LightTheme;
    }
}