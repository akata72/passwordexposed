using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using pwdExposed.ViewModels;
using pwdExposed.Services;

namespace pwdExposed.Controllers
{
    public class HomeController : Controller
    {
        private ILogger<HomeController> _logger { get; set; }
        private readonly IEmailSender _emailSender;

        /* Constructor - with DI - Logger, AppSettings and dbContext */
        public HomeController(
            ILogger<HomeController> logger,
            IEmailSender emailSender
            )
        {
            _logger = logger;
            _emailSender = emailSender;
        }      


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            /* Passing an empty model to contain the account information when it posts */
            CheckAccountViewModel model = new CheckAccountViewModel();
            ViewBag.Title = "Exposed Password - Check if your account/password is exposed in a databreach.";
            ViewBag.Description = "Password exposed will tell you if your password or other private information (i.e passwords) are leaked or otherwise exposed in a data breach.";
            var ip_address = HttpContext.Connection.RemoteIpAddress.ToString();
            ViewBag.IPAddress = ip_address;
            _logger.LogInformation("IP Address: {0}", ip_address);

            return View(model);
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult TableDemo()
        {
            ViewBag.Title = "TableDemo";
            ViewBag.Description = "TableDemoDescription";
            return View();
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> SendTestAsync()
        {
            ViewBag.Title = "Send E-mail Test";
            ViewBag.Description = "Tests the SendGrid API";

            await _emailSender.SendEmailAsync("thomas.aure@outlook.com", "Security Code", "Dette er en test");
            return View();
        }

    }
}
