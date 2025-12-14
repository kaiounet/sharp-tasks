using System;
using System.IO;
using Microsoft.AspNetCore.Mvc.Filters;
using sharp_tasks.Services;

namespace sharp_tasks.Filters;

public class LoggingFilter : ActionFilterAttribute
{
    private readonly ISessionManagerService _sessionManager;
    private readonly string _logFilePath;

    public LoggingFilter(ISessionManagerService sessionManager)
    {
        _sessionManager = sessionManager;
        var logDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
        if (!Directory.Exists(logDirectory))
        {
            Directory.CreateDirectory(logDirectory);
        }
        _logFilePath = Path.Combine(logDirectory, "actions.log");
    }

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        var username = _sessionManager.Get<string>("username") ?? "Anonymous";
        var controllerName = context.Controller?.GetType().Name.Replace("Controller", "");
        var actionName = context.ActionDescriptor?.DisplayName?.Split('.')[^1] ?? "Unknown";
        var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        var logLine = $"{timestamp} - {username} - {controllerName} - {actionName}";

        File.AppendAllText(_logFilePath, logLine + Environment.NewLine);
        base.OnActionExecuted(context);
    }
}