using AutoMapper;
using DreamHouse.Core.Application.Helpers;
using DreamHouse.Core.Application.Interfaces.Helpers;
using DreamHouse.Core.Application.Interfaces.Services;
using DreamHouse.Core.Application.Interfaces.Services.User;
using DreamHouse.Core.Application.Services;
using DreamHouse.Core.Application.Services.User;
using DreamHouse.Core.Application.ViewModels.Agent;
using DreamHouse.Core.Application.ViewModels.Home;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DreamHouse.Controllers
{
    public class DetailPropertiesController : Controller
    {
        private readonly IUserService userService;
        private readonly IUserHelper userHelper;
        private readonly IPropertyService propertyService;
        private readonly IMapper mapper;

        public DetailPropertiesController(
            IUserHelper userHelper,
            IPropertyService propertyService,
            IUserService userService,
            IMapper mapper)
        {
            this.userHelper = userHelper;
            this.propertyService = propertyService;
            this.userService = userService;
            this.mapper = mapper;
        }
        public async Task<IActionResult> HomeDetail(int id)
        {
            // Check for denied action
            if (TempData.ContainsKey("LoginAccessDenied"))
            {
                ViewBag.LoginAccessDenied = TempData["LoginAccessDenied"] as bool?;
            }

            // Obtener la propiedad
            var property = await propertyService.GetPropertyDetailsAsync(id);
            if (property == null)
            {
                return NotFound();
            }

            // Obtener el agente asociado a la propiedad
            var agent = await userService.FindyByIdAsync(property.AgentId);
            if (agent != null)
            {
                property.Agent = mapper.Map<AgentViewModel>(agent);
            }
            else
            {
                return NotFound();
            }

            return View(property);
        }
    }
}
