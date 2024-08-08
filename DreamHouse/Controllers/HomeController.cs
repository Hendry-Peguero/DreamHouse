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
        private readonly IJsonHelper jsonHelper;

        public HomeController(
            IUserHelper userHelper,
            IPropertyService propertyService,
            IUserService userService,
            IAdminHomeService adminHomeService,
            IJsonHelper jsonHelper
        )
        {
            this.userHelper = userHelper;
            this.propertyService = propertyService;
            this.jsonHelper = jsonHelper;
            this.adminHomeService = adminHomeService;
        }


        public async Task<IActionResult> HomeBasic(PropertiesFilter filter)
        {
            // Check for denied action
            if (TempData.ContainsKey("LoginAccessDenied"))
            {
                ViewBag.LoginAccessDenied = TempData["LoginAccessDenied"] as bool?;
            }

            // Default properties to show
            var properties = await propertyService.GetFilteredPropertiesAsync(filter);

            // Check if the option [PropertiesForClient is created]
            ViewBag.PropertiesForClient = false;
            if (TempData.ContainsKey("PropertiesForClient"))
            {
                filter = jsonHelper.Deserialize<PropertiesFilter>((TempData["PropertiesForClient"] as string)!)!;
                properties = await propertyService.GetFilteredPropertiesForClientAsync(filter);
                ViewBag.PropertiesForClient = true;
            }

            // Check if the option [PropertiesForAgent is created]
            ViewBag.PropertiesForAgent = false;
            if (TempData.ContainsKey("PropertiesForAgent"))
            {
                filter = jsonHelper.Deserialize<PropertiesFilter>((TempData["PropertiesForAgent"] as string)!)!;
                properties = await propertyService.GetFilteredPropertiesForAgentAsync(filter);
                ViewBag.PropertiesForAgent = true;
            }

            // Check if the option [PropertiesForFavorites is created]
            ViewBag.PropertiesForFavorites = false;
            if (TempData.ContainsKey("PropertiesForFavorites"))
            {
                filter = jsonHelper.Deserialize<PropertiesFilter>((TempData["PropertiesForFavorites"] as string)!)!;
                properties = await propertyService.GetFilteredPropertiesByFavoriteAsync(filter);
                ViewBag.PropertiesForFavorites = true;
            }

            // Check if the option [PropertiesForMaintenance is created]
            ViewBag.PropertiesForMaintenance = false;
            if (TempData.ContainsKey("PropertiesForMaintenance"))
            {
                filter = jsonHelper.Deserialize<PropertiesFilter>((TempData["PropertiesForMaintenance"] as string)!)!;
                properties = await propertyService.GetFilteredPropertiesForAgentAsync(filter);
                ViewBag.PropertiesForMaintenance = true;
            }

            // Check if the option [PropertiesForSpecifictAgent is created]
            ViewBag.PropertiesForSpecifictAgent = false;
            if (TempData.ContainsKey("PropertiesForSpecifictAgent"))
            {
                var agentId = TempData["AgentId"]!.ToString();
                filter = jsonHelper.Deserialize<PropertiesFilter>((TempData["PropertiesForSpecifictAgent"] as string)!)!;
                properties = await propertyService.GetFilteredPropertiesByAgentIdAsync(filter, agentId);
                ViewBag.PropertiesForSpecifictAgent = true;
                ViewBag.AgentId = agentId;
            }

            HomeBasicViewModel ClientHomeVm = new()
            {
                filter = filter,
                Properties = properties
            };

            return View(ClientHomeVm);
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
            //var AdminHomeVm = new AdminHomeViewModel();
            return View(AdminHomeVm);
        }
    }
}
