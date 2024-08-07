using DreamHouse.Core.Application.Enums;
using DreamHouse.Core.Application.Helpers;
using DreamHouse.Core.Application.Interfaces.Services;
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
        private readonly IPropertyService propertyService;

        public AdministrationUserController(IUserService userService,
            IUserValidationService userValidationService,
            IPropertyService propertyService)
        {
            this.userService = userService;
            this.userValidationService = userValidationService;
            this.propertyService = propertyService;
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
            var agents = await userService.GetAgents();
            foreach (var agent in agents)
            {
                agent.NumberPropertiesAssigned = await propertyService.GetAllFromAgentAsync(agent.Id);
            }
            return View(agents);
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
            var user = await userService.ChangeUserState(id);
            switch (user.UserType)
            {
                case "AGENT":
                    return RedirectRoutesHelper.routeAgentMaintance;
                case "ADMIN":
                    return RedirectRoutesHelper.adminMaintanceHome;
                case "DEVELOPER":
                    return RedirectRoutesHelper.developerMaintanceHome;
                default:
                    return RedirectRoutesHelper.routeUndefiniedHome;
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            return View("SaveUser", await userService.FindyByIdAsync(id));
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
                default:
                    return RedirectRoutesHelper.routeUndefiniedHome;
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            return View(await userService.FindyByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(string id)
        {
            await userService.DeleteAsync(id);
            return RedirectRoutesHelper.routeAgentMaintance;
        }

    }
}
