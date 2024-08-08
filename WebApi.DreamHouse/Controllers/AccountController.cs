using DreamHouse.Core.Application.Dtos.Account;
using DreamHouse.Core.Application.Enums;
using DreamHouse.Core.Application.Interfaces.Services.User;
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
