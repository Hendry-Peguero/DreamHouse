using DreamHouse.Core.Application.Enums;
using DreamHouse.Core.Application.Helpers;
using DreamHouse.Core.Application.Interfaces.Services.User;
using DreamHouse.Core.Application.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickBank.Helpers;

namespace DreamHouse.Controllers
{
    [Authorize(Roles = "ADMIN")]
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
        public async Task<IActionResult> DeveloperMaintance()
        {
            return View(await userService.GetDevelopers());
        }

        [HttpGet]
        public async Task<IActionResult> AgentMaintance()
        {
            return View(await userService.GetDevelopers());
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
                return View("SaveUser", userSaveVm);

            var response = await userService.RegisterAsync(userSaveVm);

            if (response.HasError)
            {
                userSaveVm.HasError = true;
                userSaveVm.ErrorDescription = response.ErrorDescription;
                return View("SaveUser", userSaveVm);
            }

            switch (userSaveVm.UserType)
            {
                case "ADMIN":
                    return RedirectRoutesHelper.adminMaintanceHome;
                case "DEVELOPER":
                    return RedirectRoutesHelper.developerMaintanceHome;
                default:
                    return RedirectRoutesHelper.routeUndefiniedHome;
            }
        }

        [HttpGet]
        public async Task<IActionResult> ChangeUserState(string id)
        {
            return View(await userService.FindyByIdAsync(id));
        }

        [HttpGet]
        public async Task<IActionResult> ChangeUserStatePost(string id)
        {
            var user = await userService.FindyByIdAsync(id);
            user.Status = (user.Status != (int)EUserStatus.ACTIVE) ? (int)EUserStatus.ACTIVE : (int)EUserStatus.INACTIVE;

            return View(await userService.UpdateUserAsync(user));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            return View("SaveUser",await userService.FindyByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserSaveViewModel userSaveVm)
        {
            ModelState.AddModelErrorRange(await userValidationService.UserUpdateValidation(userSaveVm));
            ModelState.AddModelErrorRange(await userValidationService.PasswordValidation(userSaveVm));

            
            if (!ModelState.IsValid)
                return View("SaveUser", userSaveVm);

            await userService.UpdateUserAsync(userSaveVm);

            switch (userSaveVm.UserType)
            {
                case "ADMIN":
                    return RedirectRoutesHelper.adminMaintanceHome;
                case "DEVELOPER":
                    return RedirectRoutesHelper.developerMaintanceHome;
                default :
                    return RedirectRoutesHelper.routeUndefiniedHome;
            }
        }
    }
}
