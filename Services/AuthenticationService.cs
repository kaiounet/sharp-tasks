using System;
using sharp_tasks.Models;

namespace sharp_tasks.Services;

public class AuthenticationService : IAuthenticationProvider
{
    private readonly ISessionManagerService _sessionManager;

    public AuthenticationService(ISessionManagerService sessionManager)
    {
        _sessionManager = sessionManager;
    }

    public bool AuthenticateUser(Login model)
    {
        if (IsAuthenticated()) { return true; }

        if (model.Username.Equals(new string(model.Password.Reverse().ToArray())))
        {
            _sessionManager.Add("isAuth", "");
            _sessionManager.Add("username", model.Username);
            return true;
        }

        return false;
    }

    public bool IsAuthenticated()
    {
        return _sessionManager.Get<string>("isAuth") != null;
    }

    public void Logout()
    {
        _sessionManager.Remove("isAuth");
        _sessionManager.Remove("username");
    }
}
