using AutoMapper;
using DreamHouse.Core.Application.Dtos.Account;
using DreamHouse.Core.Application.Enums;
using DreamHouse.Core.Application.Helpers;
using DreamHouse.Core.Application.Interfaces.Helpers;
using DreamHouse.Core.Application.Interfaces.Services.User;
using DreamHouse.Core.Application.ViewModels.Auth;
using DreamHouse.Core.Application.ViewModels.User;
using DreamHouse.Infrastructure.Identity.Services;
using Microsoft.AspNetCore.Mvc;

namespace DreamHouse.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly IUserService userService;
        private readonly IUserHelper userHelper;
        private readonly IAccountService accountService;
        private readonly IMapper mapper;

        public AuthorizationController(
            IUserService userService, 
            IUserHelper userHelper,
            IAccountService accountService,
            IMapper mapper)
        {
            this.userService = userService;
            this.userHelper = userHelper;
            this.accountService = accountService;
            this.mapper = mapper;
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
                    case nameof(ERoles.CLIENT): return RedirectRoutesHelper.routeBasicHome;
                    case nameof(ERoles.AGENT): return RedirectRoutesHelper.routeBasicHome;
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
            return RedirectRoutesHelper.routeBasicHome;
        }
        public async Task<IActionResult> AccessDenied()
        {
            return View();
        }


        public IActionResult Register()
        {
            return View(new UserSaveViewModel());

        }

        [HttpPost]
        public async Task<IActionResult> Register(UserSaveViewModel userSaveViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(userSaveViewModel);
            }

            RegisterResponse response = await userService.RegisterAsync(userSaveViewModel);

            if (response.HasError)
            {
                ModelState.AddModelError(string.Empty, response.ErrorDescription);
                return View(userSaveViewModel);
            }

            return RedirectToAction("Login");
        }
    }
}
