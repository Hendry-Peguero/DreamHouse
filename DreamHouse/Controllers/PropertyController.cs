using DreamHouse.Core.Application.Dtos.Filters;
using DreamHouse.Core.Application.Helpers;
using DreamHouse.Core.Application.Interfaces.Helpers;
using DreamHouse.Core.Application.Interfaces.Services;
using DreamHouse.Core.Application.ViewModels.Property;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DreamHouse.Controllers
{

    public class PropertyController : Controller
    {
        private readonly IPropertyService propertyService;
        private readonly IPropertyTypeService propertyTypeService;
        private readonly ISaleTypeService saleTypeService;
        private readonly IImprovementService improvementService;
        private readonly IJsonHelper jsonHelper;

        public PropertyController(
            IPropertyService propertyService,
            IPropertyTypeService propertyTypeService,
            ISaleTypeService saleTypeService,
            IImprovementService improvementService,
            IJsonHelper jsonHelper
        )
        {
            this.propertyService = propertyService;
            this.propertyTypeService = propertyTypeService;
            this.saleTypeService = saleTypeService;
            this.improvementService = improvementService;
            this.jsonHelper = jsonHelper;
        }

        [Authorize(Roles = "AGENT")]
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            // Create the instance and filled
            var model = new PropertySaveViewModel() {
                PropertyTypes = await propertyTypeService.GetAllAsync(),
                SaleTypes = await saleTypeService.GetAllAsync(),
                Improvements = await improvementService.GetAllAsync()
            };

            return View("SaveProperty", model);
        }

        [Authorize(Roles = "AGENT")]
        [HttpPost]
        public async Task<IActionResult> Add(PropertySaveViewModel propertySaveViewModel)
        {
            return RedirectRoutesHelper.routeBasicHome;
        }


        [Authorize(Roles = "CLIENT")]
        public async Task<IActionResult> FavoriteProperties(PropertiesFilter filter)
        {
            // Create the key and save the filter
            TempData["OnlyFavorites"] = jsonHelper.Serialize(filter);
            return RedirectRoutesHelper.routeBasicHome;
        }

        [Authorize(Roles = "AGENT")]
        public async Task<IActionResult> PropertyMaintenance(PropertiesFilter filter)
        {
            // Create the key and save the filter
            TempData["PropertyMaintenance"] = jsonHelper.Serialize(filter);
            return RedirectRoutesHelper.routeBasicHome;
        }

        [Authorize(Roles = "CLIENT")]
        public async Task<IActionResult> ConfigFavorite(int propertyId)
        {
            await propertyService.ConfigFavorite(propertyId);
            return RedirectRoutesHelper.routeBasicHome;
        }
    }
}
