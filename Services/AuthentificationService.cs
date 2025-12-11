using System;
using sharp_tasks.Models;

namespace sharp_tasks.Services;

public class AuthentificationService : IAuthentificationService
{
    private readonly ISessionManagerService _sessionManager;

    public AuthentificationService(ISessionManagerService sessionManager)
    {
        _sessionManager = sessionManager;
    }

    public bool Authentificate(Login model)
    {
        if (IsAuthentificated()) { return true; }

        if (model.Username.Equals(new string(model.Password.Reverse().ToArray())))
        {
            _sessionManager.Add("isAuth", "");
            return true;
        }

        return false;
    }

    public bool IsAuthentificated()
    {
        return _sessionManager.Get<string>("isAuth") != null;
    }

    public void Logout()
    {
        _sessionManager.Remove("isAuth");
    }
}
