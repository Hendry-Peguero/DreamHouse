using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using DreamHouse.Core.Application.Dtos.Account;
using DreamHouse.Core.Application.Enums;
using DreamHouse.Core.Application.Helpers;
using DreamHouse.Core.Application.Interfaces.Helpers;

namespace DreamHouse.Middlewares
{
    public class LoginAuthorize : IAsyncActionFilter
    {
        protected readonly IUserHelper userHelper;
        private readonly AuthenticationResponse userLogged;
        string? principalUserRol;

        public LoginAuthorize(IUserHelper userHelper)
        {
            this.userHelper = userHelper;
            userLogged = userHelper.GetUser();
            principalUserRol = userLogged?.Roles![^1];
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (userHelper.HasUser())
            {
                context.Result = RedirectRoutesHelper.routeAccessDenied;
            }
            else
            {
                await next();
            }
        }
    }
}
