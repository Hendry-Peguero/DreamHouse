using DreamHouse.Core.Application.Dtos.Filters;
using DreamHouse.Core.Application.Helpers;
using DreamHouse.Core.Application.Interfaces.Helpers;
using DreamHouse.Core.Application.Interfaces.Repositories;
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
        private readonly IPropertyImageRepository propertyImageRepository;
        private readonly IPropertyImprovementRepository propertyImprovementRepository;
        private readonly ISaleTypeService saleTypeService;
        private readonly IImprovementService improvementService;
        private readonly IJsonHelper jsonHelper;

        public PropertyController(
            IPropertyService propertyService,
            IPropertyTypeService propertyTypeService,
            IPropertyImageRepository propertyImageRepository,
            IPropertyImprovementRepository propertyImprovementRepository,
            ISaleTypeService saleTypeService,
            IImprovementService improvementService,
            IJsonHelper jsonHelper
        )
        {
            this.propertyService = propertyService;
            this.propertyTypeService = propertyTypeService;
            this.propertyImageRepository = propertyImageRepository;
            this.propertyImprovementRepository = propertyImprovementRepository;
            this.saleTypeService = saleTypeService;
            this.improvementService = improvementService;
            this.jsonHelper = jsonHelper;
        }

        #region Agent

        [Authorize(Roles = "AGENT")]
        public async Task<IActionResult> PropertyMaintenance(PropertiesFilter filter)
        {
            // Create the key and save the filter
            TempData["PropertyMaintenance"] = jsonHelper.Serialize(filter);
            return RedirectRoutesHelper.routeBasicHome;
        }

        [HttpGet]
        [Authorize(Roles = "AGENT")]
        public async Task<IActionResult> Add()
        {
            // Create the instance and filled
            var model = new PropertySaveViewModel()
            {
                PropertyTypes = await propertyTypeService.GetAllAsync(),
                SaleTypes = await saleTypeService.GetAllAsync(),
                Improvements = await improvementService.GetAllAsync(),
                ImagesUrl = new()
            };

            return View("SaveProperty", model);
        }

        [HttpPost]
        [Authorize(Roles = "AGENT")]
        public async Task<IActionResult> Add(PropertySaveViewModel propertySaveViewModel)
        {
            // Make all validations
            if (!ModelState.IsValid)
            {
                propertySaveViewModel.PropertyTypes = await propertyTypeService.GetAllAsync();
                propertySaveViewModel.SaleTypes = await saleTypeService.GetAllAsync();
                propertySaveViewModel.Improvements = await improvementService.GetAllAsync();
                return View("SaveProperty", propertySaveViewModel);
            }

            // Add the property
            await propertyService.AddAsync(propertySaveViewModel);

            return RedirectRoutesHelper.routePropertyMaintance;
        }

        [HttpGet]
        [Authorize(Roles = "AGENT")]
        public async Task<IActionResult> Edit(int propertyId)
        {
            // Load necesary data 
            var property = await propertyService.GetByIdAsync(propertyId);
            property.PropertyTypes = await propertyTypeService.GetAllAsync();
            property.SaleTypes = await saleTypeService.GetAllAsync();
            property.Improvements = await improvementService.GetAllAsync();

            // Load the improvements Selected
            property.IdSelectedImprovements = 
                (await propertyImprovementRepository.GetAllAsync())
                .Where(i => i.PropertyId == propertyId)
                .Select(i => i.ImprovementId)
                .ToList();

            // Load the images
            var images = (await propertyImageRepository.GetAllAsync()).Where(img => img.PropertyId == propertyId);
            property.ImagesUrl = new();
            foreach (var img in images) property.ImagesUrl.Add(img.ImageUrl);
            
            return View("SaveProperty", property);
        }

        [HttpPost]
        [Authorize(Roles = "AGENT")]
        public async Task<IActionResult> Edit(PropertySaveViewModel propertySaveViewModel)
        {
            //Remove validatiosn
            ModelState.Remove(nameof(propertySaveViewModel.Images));

            // Make all validations
            if (!ModelState.IsValid)
            {
                propertySaveViewModel.PropertyTypes = await propertyTypeService.GetAllAsync();
                propertySaveViewModel.SaleTypes = await saleTypeService.GetAllAsync();
                propertySaveViewModel.Improvements = await improvementService.GetAllAsync();
                return View("SaveProperty", propertySaveViewModel);
            }

            // Update the property
            await propertyService.UpdateAsync(propertySaveViewModel, propertySaveViewModel.Id);

            return RedirectRoutesHelper.routePropertyMaintance;
        }

        [HttpGet]
        [Authorize(Roles = "AGENT")]
        public async Task<IActionResult> ConfirmDelete(int propertyId)
        {
            return View("ConfirmDeleteProperty", await propertyService.GetByIdAsync(propertyId));
        }

        [HttpPost]
        [Authorize(Roles = "AGENT")]
        public async Task<IActionResult> DeleteConfirmed(int propertyId)
        {
            await propertyService.DeleteAsync(propertyId);
            return RedirectRoutesHelper.routePropertyMaintance;
        }

        #endregion

        #region Client

        [Authorize(Roles = "CLIENT")]
        public IActionResult FavoriteProperties(PropertiesFilter filter)
        {
            // Create the key and save the filter
            TempData["OnlyFavorites"] = jsonHelper.Serialize(filter);
            return RedirectRoutesHelper.routeBasicHome;
        }

        [Authorize(Roles = "CLIENT")]
        public async Task<IActionResult> ConfigFavorite(int propertyId)
        {
            await propertyService.ConfigFavorite(propertyId);
            return RedirectRoutesHelper.routeBasicHome;
        }

        #endregion

    }
}
