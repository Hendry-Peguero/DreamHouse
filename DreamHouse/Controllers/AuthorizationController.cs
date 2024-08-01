using DreamHouse.Core.Application.Dtos.Account;
using DreamHouse.Core.Application.Enums;
using DreamHouse.Core.Application.Helpers;
using DreamHouse.Core.Application.Interfaces.Helpers;
using DreamHouse.Core.Application.Interfaces.Services.User;
using DreamHouse.Core.Application.ViewModels.Auth;
using Microsoft.AspNetCore.Mvc;

namespace DreamHouse.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly IUserService userService;
        private readonly IUserHelper userHelper;

        public AuthorizationController(IUserService userService, IUserHelper userHelper)
        {
            this.userService = userService;
            this.userHelper = userHelper;
        }

        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.HasErrors = true;
                return View(loginVm);
            }

            AuthenticationResponse responseLogin = await userService.LoginAsync(loginVm);
            if (responseLogin != null && responseLogin.HasError != true)
            {
                userHelper.SetUser(responseLogin);
                string principalRole = responseLogin.Roles![^1];

                switch (principalRole)
                {
                    case nameof(ERoles.CLIENT): return RedirectRoutesHelper.routeClientHome;
                    case nameof(ERoles.AGENT): return RedirectRoutesHelper.routeAgentHome;
                    case nameof(ERoles.ADMIN): return RedirectRoutesHelper.routeAdminHome;
                    default: return RedirectRoutesHelper.routeUndefiniedHome;
                }
            }
            else
            {
                loginVm.HasError = true;
                loginVm.ErrorDescription = responseLogin.ErrorDescription;
                ViewBag.HasErrors = true;
                return View(loginVm);
            }
        }
        public async Task<IActionResult> SignOut()
        {
            await userService.SignOutAsync();
            userHelper.RemoveUser();
            return RedirectToRoute(new { controller = "Authorization", action = "Login" });
        }
        public async Task<IActionResult> AccessDenied()
        {
            return View();
        }
    }
}
