using Microsoft.AspNetCore.Mvc;

namespace pwdExposed.Controllers
{
    public class ToolController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}