using DreamHouse.Core.Application.Dtos.Account;
using DreamHouse.Core.Application.Enums;
using DreamHouse.Core.Application.Interfaces.Services.User;
using DreamHouse.Infrastructure.Identity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.DreamHouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;
        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpPost("autenticate")]
        public async Task<IActionResult> AuthenticateAsync(AuthenticationRequest request)
        {
            // Check if the user is a CLIENT OR AGENT
            var user = await accountService.FindByNameOrEmailAsync(request.Email);
            if (user != null)
            {
                string principalRole = user.Roles![^1];
                if (principalRole == ERoles.CLIENT.ToString() || principalRole == ERoles.AGENT.ToString())
                {
                    return BadRequest($"{principalRole} UNABLE TO LOG IN THE API");
                }
            }

            return Ok(await accountService.AuthenticateAsync(request));
        }

        [Authorize(Roles ="ADMIN")]
        [HttpPost("registerAdmin")]
        public async Task<IActionResult> RegisterAdminAsync(RegisterRequest request)
        {
            request.UserType = ERoles.ADMIN.ToString();
            return Ok(await accountService.RegisterUserAsync(request));
        }

        [Authorize]
        [HttpPost("registerDeveloper")]
        public async Task<IActionResult> RegisterDeveloperAsync(RegisterRequest request)
        {
            request.UserType = ERoles.DEVELOPER.ToString();
            return Ok(await accountService.RegisterUserAsync(request));
        }
    }
}
