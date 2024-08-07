using AutoMapper;
using DreamHouse.Core.Application.Dtos.Account;
using DreamHouse.Core.Application.Enums;
using DreamHouse.Core.Application.Helpers;
using DreamHouse.Core.Application.Interfaces.Helpers;
using DreamHouse.Core.Application.Interfaces.Services;
using DreamHouse.Core.Application.Interfaces.Services.User;
using DreamHouse.Core.Application.Services.User;
using DreamHouse.Core.Application.ViewModels.Auth;
using DreamHouse.Core.Application.ViewModels.User;
using DreamHouse.Infrastructure.Identity.Entities;
using DreamHouse.Infrastructure.Identity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using QuickBank.Helpers;
using System.Text;

namespace DreamHouse.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly IUserService userService;
        private readonly IUserHelper userHelper;
        private readonly IAccountService accountService;
        private readonly IMapper mapper;
        private readonly IRegisterValidationService registerValidationService;

        public AuthorizationController(
            IUserService userService, 
            IUserHelper userHelper,
            IAccountService accountService,
            IMapper mapper,
            IRegisterValidationService registerValidationService)
        {
            this.userService = userService;
            this.userHelper = userHelper;
            this.accountService = accountService;
            this.mapper = mapper;
            this.registerValidationService = registerValidationService;
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
        public async Task<IActionResult> Register(UserSaveViewModel vm)
        {
            ModelState.AddModelErrorRange(await registerValidationService.ValidateUserRegistrationAsync(vm));
            if (!ModelState.IsValid) return View("Register", vm);

            var origin = Request.Headers["origin"];
            RegisterResponse response = await userService.RegisterClienAndAgentAsync(vm, origin);

            if (response.HasError)
            {
                vm.HasError = response.HasError;
                vm.ErrorDescription = response.ErrorDescription;
                return View(vm);
            }

            return RedirectToAction("Login");
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            string response = await userService.ConfirmEmailAsync(userId, token);
            if (response == "Success")
            {
                return RedirectToAction("Login");
            }

            return View("Error", response); // Puedes mostrar un mensaje de error específico aquí
        }

    }
}
