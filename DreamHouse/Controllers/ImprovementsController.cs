using DreamHouse.Core.Application.Helpers;
using DreamHouse.Core.Application.Interfaces.Services;
using DreamHouse.Core.Application.Interfaces.Services.Validations;
using DreamHouse.Core.Application.ViewModels.Improvement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickBank.Helpers;

namespace DreamHouse.Controllers
{
    [Authorize(Roles ="ADMIN")]
    public class ImprovementsController : Controller
    {
        private readonly IImprovementService improvementService;
        private readonly IImprovementValidationService improvementValidationService;

        public ImprovementsController(IImprovementService improvementService,
            IImprovementValidationService improvementValidationService)
        {
            this.improvementService = improvementService;
            this.improvementValidationService = improvementValidationService;
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
            ModelState.AddModelErrorRange(await improvementValidationService.DuplicateName(improvementSaveVm));
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
            ModelState.AddModelErrorRange(await improvementValidationService.UpdateDuplicateName(improvementSaveVm));

            if (!ModelState.IsValid)
            {
                return View("Save",improvementSaveVm);
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
