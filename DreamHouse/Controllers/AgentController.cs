using DreamHouse.Core.Application.Helpers;
using DreamHouse.Core.Application.Interfaces.Helpers;
using DreamHouse.Core.Application.Interfaces.Services.User;
using DreamHouse.Core.Application.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DreamHouse.Controllers
{
    [Authorize(Roles = "AGENT")]
    public class AgentController : Controller
    {
        private readonly IUserService userService;
        private readonly IUserHelper userHelper;

        public AgentController(
            IUserService userService,
            IUserHelper userHelper
        )
        {
            this.userService = userService;
            this.userHelper = userHelper;
        }

        [HttpGet]
        public async Task<IActionResult> EditAgentProfile()
        {
            var agentId = userHelper.GetUser()!.Id;
            return View("SaveAgent", await userService.FindyByIdAsync(agentId));
        }

        [HttpPost]
        public async Task<IActionResult> EditAgentProfile(UserSaveViewModel userSaveVm)
        {
            if (!ModelState.IsValid) return View("SaveAgent", userSaveVm);

            await userService.UpdateAgentAsync(userSaveVm);

            return RedirectRoutesHelper.routeBasicHome;
        }
    }
}
