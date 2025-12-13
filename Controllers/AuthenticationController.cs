using Microsoft.AspNetCore.Mvc;
using sharp_tasks.Services;
using sharp_tasks.ViewModels;
using sharp_tasks.Filters;
using sharp_tasks.Mappers;

namespace sharp_tasks.Controllers
{
    [TypeFilter(typeof(ThemeFilter))]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationProvider _authenticationService;

        public AuthenticationController(IAuthenticationProvider authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public IActionResult Login()
        {
            if (_authenticationService.IsAuthenticated())
            {
                return RedirectToAction(nameof(Index), "Tasks");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginVM model)
        {
            if (_authenticationService.IsAuthenticated())
            {
                return RedirectToAction(nameof(Index), "Tasks");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Models.Login login = LoginMapper.GetLoginFromLoginVM(model);

            if (!_authenticationService.AuthenticateUser(login))
            {
                ViewBag.loginError = true;
                return View(model);
            }

            return RedirectToAction(nameof(Index), "Tasks");
        }

        [TypeFilter(typeof(AuthenticationFilter))]
        public IActionResult Logout()
        {
            _authenticationService.Logout();
            return RedirectToAction(nameof(Login));
        }
    }
}
