using DreamHouse.Core.Application.Dtos.Filters;
using DreamHouse.Core.Application.Interfaces.Helpers;
using DreamHouse.Core.Application.Interfaces.Services;
using DreamHouse.Core.Application.ViewModels.Home;
using DreamHouse.Core.Application.ViewModels.Property;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DreamHouse.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserHelper userHelper;
        private readonly IPropertyService propertyService;
        private readonly IJsonHelper jsonHelper;

        public HomeController(
            IUserHelper userHelper,
            IPropertyService propertyService,
            IJsonHelper jsonHelper
        )
        {
            this.userHelper = userHelper;
            this.propertyService = propertyService;
            this.jsonHelper = jsonHelper;
        }


        public async Task<IActionResult> HomeBasic(PropertiesFilter filter)
        {
            // Check for denied action
            if (TempData.ContainsKey("LoginAccessDenied"))
            {
                ViewBag.LoginAccessDenied = TempData["LoginAccessDenied"] as bool?;
            }

            // Check if the option [OnlyFavorite is created]
            ViewBag.OnlyFavorites = false;
            if (TempData.ContainsKey("OnlyFavorites"))
            {
                filter = jsonHelper.Deserialize<PropertiesFilter>((TempData["OnlyFavorites"] as string)!)!;
                ViewBag.OnlyFavorites = true;
            }

            var properties = ViewBag.OnlyFavorites ?
                  await propertyService.GetFilteredPropertiesByFavoriteAsync(filter)
                : await propertyService.GetFilteredPropertiesByRoleAsync(filter);

            HomeBasicViewModel ClientHomeVm = new () {
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
            AdminHomeViewModel AdminHomeVm = new();

            return View(AdminHomeVm);
        }
    }
}
