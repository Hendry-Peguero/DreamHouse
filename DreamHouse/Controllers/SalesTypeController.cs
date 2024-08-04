using DreamHouse.Core.Application.Helpers;
using DreamHouse.Core.Application.Interfaces.Services;
using DreamHouse.Core.Application.Services;
using DreamHouse.Core.Application.ViewModels.PropertyType;
using DreamHouse.Core.Application.ViewModels.SaleType;
using DreamHouse.Core.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DreamHouse.Controllers
{
    public class SalesTypeController : Controller
    {
        private readonly ISaleTypeService saleTypeService;

        public SalesTypeController(ISaleTypeService saleTypeService)
        {
            this.saleTypeService = saleTypeService;
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
            if (!ModelState.IsValid)
            {
                return View(saleTypeSaveVm);
            }
            await saleTypeService.AddAsync(saleTypeSaveVm);
            return RedirectRoutesHelper.routeSalesTypeIndex;
        }

        //[HttpGet]
        //public async Task<IActionResult> Edit(int id)
        //{

        //    return View("Save", await saleTypeService.GetByIdAsync(id));
        //}

        //[HttpPost]
        //public async Task<IActionResult> Edit(PropertyTypeSaveViewModel propertyTypeSaveVm)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(propertyTypeSaveVm);
        //    }
        //    await propertyTypeService.UpdateAsync(propertyTypeSaveVm, propertyTypeSaveVm.Id.Value);
        //    return RedirectRoutesHelper.routePropertyTypeIndex;
        //}

        //[HttpGet]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    return View(await propertyTypeService.GetByIdAsync(id));
        //}

        //[HttpPost]
        //public async Task<IActionResult> DeletePost(int id)
        //{
        //    await propertyTypeService.DeleteAsync(id);
        //    return RedirectRoutesHelper.routePropertyTypeIndex;
        //}
    }
}
