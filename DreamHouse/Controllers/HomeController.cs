using DreamHouse.Core.Application.Dtos.Filters;
using DreamHouse.Core.Application.Interfaces.Helpers;
using DreamHouse.Core.Application.Interfaces.Services;
using DreamHouse.Core.Application.Interfaces.Services.AdminHome;
using DreamHouse.Core.Application.Interfaces.Services.User;
using DreamHouse.Core.Application.ViewModels.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DreamHouse.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserHelper userHelper;
        private readonly IPropertyService propertyService;
        private readonly IUserService userService;
        private readonly IAdminHomeService adminHomeService;

        public HomeController(
            IUserHelper userHelper,
            IPropertyService propertyService,
            IUserService userService,
            IAdminHomeService adminHomeService)
        {
            this.userHelper = userHelper;
            this.propertyService = propertyService;
            this.userService = userService;
            this.adminHomeService = adminHomeService;
        }

        public async Task<IActionResult> HomeBasic(PropertiesFilter filter)
        {
            // Check for denied action
            if (TempData.ContainsKey("LoginAccessDenied"))
            {
                ViewBag.LoginAccessDenied = TempData["LoginAccessDenied"] as bool?;
            }

            var user = userHelper.GetUser();
            var properties = await propertyService.GetFilteredPropertiesAsync(filter);

            HomeBasicViewModel ClientHomeVm = new () {
                filter = filter,
                Properties = properties
            };

            return View(ClientHomeVm);
        }


        ////////////////////////////////////////////////////////////////////////// AQUIIIIII
        [Authorize(Roles = "CLIENT")]
        public async Task<IActionResult> ClientHome(PropertiesFilter filter)
        {
            // Check for denied action
            if (TempData.ContainsKey("LoginAccessDenied"))
            {
                ViewBag.LoginAccessDenied = TempData["LoginAccessDenied"] as bool?;
            }

            var user = userHelper.GetUser();
            var properties = await propertyService.GetFilteredPropertiesAsync(filter);

            HomeBasicViewModel ClientHomeVm = new()
            {
                filter = filter,
                Properties = properties
            };

            return View(ClientHomeVm);
        }


        [Authorize(Roles = "AGENT")]
        public async Task<IActionResult> AgentHome()
        {
            // Check for denied action
            if (TempData.ContainsKey("LoginAccessDenied"))
            {
                ViewBag.LoginAccessDenied = TempData["LoginAccessDenied"] as bool?;
            }

            var user = userHelper.GetUser();
            HomeBasicViewModel AgentHomeVm = new();

            return View(AgentHomeVm);
        }

        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> AdminHome()
        {
            // Check for denied action
            if (TempData.ContainsKey("LoginAccessDenied"))
            {
                ViewBag.LoginAccessDenied = TempData["LoginAccessDenied"] as bool?;
            }

            var user = userHelper.GetUser();
            AdminHomeViewModel AdminHomeVm = await adminHomeService.DisplayValuesHome();

            return View(AdminHomeVm);
        }
    }
}
