using Microsoft.AspNetCore.Mvc;

namespace DreamHouse.Controllers
{
    public class AgentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
