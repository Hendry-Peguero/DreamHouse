using DreamHouse.Core.Application.Helpers;
using DreamHouse.Core.Application.Interfaces.Services;
using DreamHouse.Core.Application.Interfaces.Services.Validations;
using DreamHouse.Core.Application.Services.User;
using DreamHouse.Core.Application.ViewModels.PropertyType;
using DreamHouse.Core.Application.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickBank.Helpers;

namespace DreamHouse.Controllers
{
    [Authorize(Roles ="ADMIN")]
    public class PropertyTypeMaintanceController : Controller
    {
        private readonly IPropertyTypeService propertyTypeService;
        private readonly IDuplicateNameValidationService duplicateNameValidationService;

        public PropertyTypeMaintanceController(IPropertyTypeService propertyTypeService,
            IDuplicateNameValidationService duplicateNameValidationService)
        {
            this.propertyTypeService = propertyTypeService;
            this.duplicateNameValidationService = duplicateNameValidationService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await propertyTypeService.GetAllViewModelWithInclude());
        }

        [HttpGet]
        public async Task<IActionResult> Save()
        {
            return View(new PropertyTypeSaveViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Save(PropertyTypeSaveViewModel propertyTypeSaveVm)
        {
            ModelState.AddModelErrorRange(await duplicateNameValidationService.DuplicateName(propertyTypeSaveVm));

            //hay que agregarle las validaciones de nombre duplicado
            if (!ModelState.IsValid)
            {
                return View(propertyTypeSaveVm);
            }

            await propertyTypeService.AddAsync(propertyTypeSaveVm);
            return RedirectRoutesHelper.routePropertyTypeIndex;
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            return View("Save",  await propertyTypeService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PropertyTypeSaveViewModel propertyTypeSaveVm)
        {
            if (!ModelState.IsValid)
            {
                return View(propertyTypeSaveVm);
            }
            await propertyTypeService.UpdateAsync(propertyTypeSaveVm,propertyTypeSaveVm.Id.Value);
            return RedirectRoutesHelper.routePropertyTypeIndex;
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            return View(await propertyTypeService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            await propertyTypeService.DeleteAsync(id);
            return RedirectRoutesHelper.routePropertyTypeIndex;
        }
    }
}
