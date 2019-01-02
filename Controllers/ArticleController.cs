using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace pwdExposed.Controllers
{
    public class ArticleController : Controller
    {
        private ILogger<ArticleController> _logger { get; set; }

        public ArticleController(
            ILogger<ArticleController> logger
            )
        {
            _logger = logger;
        }


        public IActionResult Index()
        {
            ViewBag.Title = "All Articles";
            return View();
        }



        [HttpGet]
        [AllowAnonymous]
        public IActionResult PasswordGuidelines()
        {
            ViewBag.Title = "Guidelines for keeping your accounts safer";
            return View();
        }



    }
}