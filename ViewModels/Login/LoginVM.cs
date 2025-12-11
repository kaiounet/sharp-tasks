using System;
using System.ComponentModel.DataAnnotations;

namespace sharp_tasks.ViewModels;

public class LoginVM
{
    [Required(ErrorMessage = "Username is required!")]
    public string? Username { get; set; }
    [Required(ErrorMessage = "Password is required!")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
}
