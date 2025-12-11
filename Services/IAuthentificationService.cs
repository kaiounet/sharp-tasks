using System;
using sharp_tasks.Models;

namespace sharp_tasks.Services;

public interface IAuthentificationService
{
    public bool Authentificate(Login model);
    public void Logout();
    public bool IsAuthentificated();
}
