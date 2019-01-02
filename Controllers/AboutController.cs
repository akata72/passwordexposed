using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace pwdExposed.Controllers
{
    public class AboutController : Controller
    {

        private ILogger<AboutController> _logger { get; set; }

        /* Constructor - with DI - Logger, AppSettings and dbContext */
        public AboutController(
            ILogger<AboutController> logger
            )
        {
            _logger = logger;
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult WhoAndWhy()
        {
            ViewBag.Title = "Is your password exposed? - About - Who and why";
            ViewBag.Header = "About - Who and why";
            return View();
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Contact()
        {
            ViewBag.Title = "Is your password exposed? - Contact Information";
            return View();
        }

    }







}
