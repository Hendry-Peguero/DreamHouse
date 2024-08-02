using DreamHouse.Core.Application.Enums;
using DreamHouse.Core.Application.Helpers;
using DreamHouse.Core.Application.Interfaces.Services.User;
using DreamHouse.Core.Application.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickBank.Helpers;

namespace DreamHouse.Controllers
{
    [Authorize (Roles ="ADMIN")]
    public class AdministrationUserController : Controller
    {
        private readonly IUserService userService;
        private readonly IUserValidationService userValidationService;

        public AdministrationUserController(IUserService userService,
            IUserValidationService userValidationService)
        {
            this.userService = userService;
            this.userValidationService = userValidationService;
        }

        public IActionResult Home()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AdminMaintance()
        {
            return View(await userService.GetAdmins());
        }

        [HttpGet]
        public async Task<IActionResult> Add(ERoles roles)
        {
            var userSaveVm = new UserSaveViewModel()
            {
                UserType = roles.ToString()
            };
            return View("SaveUser", userSaveVm);
        }
        
        [HttpPost]
        public async Task<IActionResult> AddUser(UserSaveViewModel userSaveVm)
        {
            ModelState.AddModelErrorRange(await userValidationService.PasswordValidation(userSaveVm));
            ModelState.AddModelErrorRange(await userValidationService.UserSaveValidation(userSaveVm));

            if (!ModelState.IsValid)
            {
                return View("SaveUser", userSaveVm);
            }

            var response = await userService.RegisterAsync(userSaveVm);

            if (response.HasError)
            {
                userSaveVm.HasError = true;
                userSaveVm.ErrorDescription = response.ErrorDescription;
                return View("SaveUser", userSaveVm);
            }
            return RedirectRoutesHelper.adminMaintanceHome;
        }

    }
}
