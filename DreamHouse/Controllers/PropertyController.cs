using DreamHouse.Core.Application.Dtos.Filters;
using DreamHouse.Core.Application.Helpers;
using DreamHouse.Core.Application.Interfaces.Helpers;
using DreamHouse.Core.Application.Interfaces.Services;
using DreamHouse.Core.Application.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;

namespace DreamHouse.Controllers
{
    public class PropertyController : Controller
    {
        private readonly IPropertyService propertyService;
        private readonly IJsonHelper jsonHelper;

        public PropertyController(
            IPropertyService propertyService,
            IJsonHelper jsonHelper
        )
        {
            this.propertyService = propertyService;
            this.jsonHelper = jsonHelper;
        }

        public async Task<IActionResult> FavoriteProperties(PropertiesFilter filter)
        {
            // Create the key and save the filter
            TempData["OnlyFavorites"] = jsonHelper.Serialize(filter);
            return RedirectRoutesHelper.routeBasicHome;
        }

        public async Task<IActionResult> ConfigFavorite(int propertyId)
        {
            await propertyService.ConfigFavorite(propertyId);
            return RedirectRoutesHelper.routeBasicHome;
        }
    }
}
