using DreamHouse.Core.Application.Helpers;
using DreamHouse.Core.Application.Interfaces.Services;
using DreamHouse.Core.Application.Interfaces.Services.Validations;
using DreamHouse.Core.Application.Services;
using DreamHouse.Core.Application.ViewModels.PropertyType;
using DreamHouse.Core.Application.ViewModels.SaleType;
using DreamHouse.Core.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickBank.Helpers;

namespace DreamHouse.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class SalesTypeController : Controller
    {
        private readonly ISaleTypeService saleTypeService;
        private readonly ISalesTypeValidationService duplicateNameValidationService;

        public SalesTypeController(ISaleTypeService saleTypeService,
            ISalesTypeValidationService duplicateNameValidationService)
        {
            this.saleTypeService = saleTypeService;
            this.duplicateNameValidationService = duplicateNameValidationService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await saleTypeService.GetAllViewModelWithInclude());
        }

        [HttpGet]
        public async Task<IActionResult> Save()
        {
            return View(new SaleTypeSaveViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Save(SaleTypeSaveViewModel saleTypeSaveVm)
        {
            ModelState.AddModelErrorRange(await duplicateNameValidationService.DuplicateName(saleTypeSaveVm));

            if (!ModelState.IsValid)
            {
                return View(saleTypeSaveVm);
            }
            await saleTypeService.AddAsync(saleTypeSaveVm);
            return RedirectRoutesHelper.routeSalesTypeIndex;
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            return View("Save", await saleTypeService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaleTypeSaveViewModel saleTypeSaveVm)
        {
            ModelState.AddModelErrorRange(await duplicateNameValidationService.UpdateDuplicateName(saleTypeSaveVm));

            if (!ModelState.IsValid)
            {
                return View("Save", saleTypeSaveVm);
            }
            await saleTypeService.UpdateAsync(saleTypeSaveVm, saleTypeSaveVm.Id.Value);
            return RedirectRoutesHelper.routeSalesTypeIndex;
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            return View(await saleTypeService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            await saleTypeService.DeleteAsync(id);
            return RedirectRoutesHelper.routeSalesTypeIndex;
        }
    }
}
