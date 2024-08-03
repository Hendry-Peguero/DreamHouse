using DreamHouse.Core.Application.Helpers;
using DreamHouse.Core.Application.Interfaces.Helpers;
using DreamHouse.Core.Application.Interfaces.Services;
using DreamHouse.Core.Application.Services;
using DreamHouse.Core.Application.ViewModels.Home;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DreamHouse.Controllers
{
    public class DetailPropertiesController : Controller
    {
        private readonly IUserHelper userHelper;
        private readonly IPropertyService propertyService;

        public DetailPropertiesController(
            IUserHelper userHelper,
            IPropertyService propertyService)
        {
            this.userHelper = userHelper;
            this.propertyService = propertyService;
        }
        public async Task<IActionResult> HomeDetail(int id)
        {
            // Check for denied action
            if (TempData.ContainsKey("LoginAccessDenied"))
            {
                ViewBag.LoginAccessDenied = TempData["LoginAccessDenied"] as bool?;
            }

            var user = userHelper.GetUser();
            var property = await propertyService.GetPropertyDetailsAsync(id);

            if (property == null)
            {
                return NotFound();
            }

            return View(property);
        }
    }
}
