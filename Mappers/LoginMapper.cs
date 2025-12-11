using System;
using sharp_tasks.Models;
using sharp_tasks.ViewModels;

namespace sharp_tasks.Mappers;

public class LoginMapper
{
    public static Login GetLoginFromLoginVM(LoginVM model)
    {
        return new Login
        {
            Username = model.Username,
            Password = model.Password,
        };
    }
}
