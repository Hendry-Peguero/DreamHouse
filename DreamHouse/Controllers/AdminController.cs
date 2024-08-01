using DreamHouse.Core.Application.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;

namespace DreamHouse.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserService userService;

        public AdminController(IUserService userService)
        {
            this.userService = userService;
        }

        public IActionResult Home()
        {
            return View(userService.GetAdmins());
        }

        [HttpGet]
        public async Task<IActionResult> AdminMaintance()
        {
            return View(await userService.GetAdmins());
        }
    }
}
