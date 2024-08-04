using DreamHouse.Core.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace DreamHouse.Controllers
{
    public class PropertyTypeMaintanceController : Controller
    {
        private readonly IPropertyTypeService propertyTypeService;

        public PropertyTypeMaintanceController(IPropertyTypeService propertyTypeService)
        {
            this.propertyTypeService = propertyTypeService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await propertyTypeService.GetAllViewModelWithInclude());
        }
    }
}
