using DreamHouse.Core.Application.Interfaces.Helpers;
using DreamHouse.Core.Application.Interfaces.Services;
using DreamHouse.Core.Application.ViewModels.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DreamHouse.Controllers
{

    public class HomeController : Controller
    {
        private readonly IUserHelper userHelper;
        private readonly IPropertyService propertyService;

        public HomeController(
            IUserHelper userHelper,
            IPropertyService propertyService)
        {
            this.userHelper = userHelper;
            this.propertyService = propertyService;
        }

        public async Task<IActionResult> HomeBasic()
        {
            // Check for denied action
            if (TempData.ContainsKey("LoginAccessDenied"))
            {
                ViewBag.LoginAccessDenied = TempData["LoginAccessDenied"] as bool?;
            }

            var user = userHelper.GetUser();
            ClientHomeViewModel ClientHomeVm = new ClientHomeViewModel
            {
                Properties = await propertyService.GetAllWithTypePropertyAndSaleAsync()
            };

            return View(ClientHomeVm);
        }

        [Authorize(Roles = "CLIENT")]
        public async Task<IActionResult> ClientHome()
        {
            // Check for denied action
            if (TempData.ContainsKey("LoginAccessDenied"))
            {
                ViewBag.LoginAccessDenied = TempData["LoginAccessDenied"] as bool?;
            }

            var user = userHelper.GetUser();
            ClientHomeViewModel ClientHomeVm = new();

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
            AgentHomeViewModel AgentHomeVm = new();

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
            AdminHomeViewModel AdminHomeVm = new();

            return View(AdminHomeVm);
        }
    }
}
