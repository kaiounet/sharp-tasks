using sharp_tasks.Services;
using sharp_tasks.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<LoggingFilter>();
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ISessionManagerService, SessionManagerService>();
builder.Services.AddScoped<IAuthenticationProvider, AuthenticationService>();
builder.Services.AddScoped<AuthenticationFilter>();
builder.Services.AddScoped<IThemeProvider, ThemeService>();
builder.Services.AddScoped<ThemeFilter>();
builder.Services.AddScoped<LoggingFilter>();
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Tasks}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
