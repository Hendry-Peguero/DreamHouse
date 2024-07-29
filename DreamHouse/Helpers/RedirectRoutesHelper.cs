using Microsoft.AspNetCore.Mvc;

namespace DreamHouse.Core.Application.Helpers
{
    public static class RedirectRoutesHelper
    {
        public static RedirectToRouteResult routeClientHome = new RedirectToRouteResult(new { controller = "Home", action = "ClientHome" });
        public static RedirectToRouteResult routeAgentHome = new RedirectToRouteResult(new { controller = "Home", action = "AgentHome" });
        public static RedirectToRouteResult routeAdminHome = new RedirectToRouteResult(new { controller = "Home", action = "AdminHome" });

        public static RedirectToRouteResult routeUndefiniedHome = new RedirectToRouteResult(new { controller = "", action = "" });
    }
}
