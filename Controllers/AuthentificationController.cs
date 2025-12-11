using Microsoft.AspNetCore.Mvc;
using sharp_tasks.Services;
using sharp_tasks.ViewModels;
using sharp_tasks.Filters;
using sharp_tasks.Mappers;

namespace sharp_tasks.Controllers
{
    public class AuthentificationController : Controller
    {
        private readonly IAuthentificationService _authentificationService;

        public AuthentificationController(IAuthentificationService authentificationService)
        {
            _authentificationService = authentificationService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Models.Login login = LoginMapper.GetLoginFromLoginVM(model);

            if (!_authentificationService.Authentificate(login))
            {
                ViewBag.loginError = true;
                return View(model);
            }

            return RedirectToAction(nameof(Index), "Tasks");
        }

        [TypeFilter(typeof(AuthentificationFilter))]
        public IActionResult Logout()
        {
            _authentificationService.Logout();
            return RedirectToAction(nameof(Login));
        }
    }
}
