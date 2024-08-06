using DreamHouse.Core.Application.Helpers;
using DreamHouse.Core.Application.Interfaces.Services;
using DreamHouse.Core.Application.Services;
using DreamHouse.Core.Application.ViewModels.Improvement;
using DreamHouse.Core.Application.ViewModels.PropertyType;
using DreamHouse.Core.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DreamHouse.Controllers
{
    public class ImprovementsController : Controller
    {
        private readonly IImprovementService improvementService;

        public ImprovementsController(IImprovementService improvementService)
        {
            this.improvementService = improvementService;
        }
        
        public async Task<IActionResult> Index()
        {
            return View(await improvementService.GetAllAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Save()
        {
            return View(new ImprovementSaveViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Save(ImprovementSaveViewModel improvementSaveVm)
        {
            //hay que agregarle las validaciones de nombre duplicado
            if (!ModelState.IsValid)
            {
                return View(improvementSaveVm);
            }
            await improvementService.AddAsync(improvementSaveVm);
            return RedirectRoutesHelper.routeImprovementMaintance;
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            return View("Save", await improvementService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ImprovementSaveViewModel improvementSaveVm)
        {
            if (!ModelState.IsValid)
            {
                return View(improvementSaveVm);
            }
            await improvementService.UpdateAsync(improvementSaveVm, improvementSaveVm.Id.Value);
            return RedirectRoutesHelper.routeImprovementMaintance;
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            return View(await improvementService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            await improvementService.DeleteAsync(id);
            return RedirectRoutesHelper.routeImprovementMaintance;
        }
    }
}
