using DreamHouse.Core.Application.Helpers;
using DreamHouse.Core.Application.Interfaces.Services;
using DreamHouse.Core.Application.ViewModels.PropertyType;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DreamHouse.Controllers
{
    [Authorize(Roles ="ADMIN")]
    public class PropertyTypeMaintanceController : Controller
    {
        private readonly IPropertyTypeService propertyTypeService;

        public PropertyTypeMaintanceController(IPropertyTypeService propertyTypeService)
        {
            this.propertyTypeService = propertyTypeService;
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
