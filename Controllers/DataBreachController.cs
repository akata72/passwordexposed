using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using Newtonsoft.Json;
using pwdExposed.ViewModels;
using Microsoft.AspNetCore.Authorization;
using pwdExposed.Services;

namespace pwdExposed.Controllers
{
    public class DataBreachController : Controller
    {

        private ILogger<DataBreachController> _logger { get; set; }
        private readonly IEmailSender _emailSender;


        public DataBreachController(
            ILogger<DataBreachController> logger,
            IEmailSender emailSender
            )
        {
            _emailSender = emailSender;
            _logger = logger;
        }


        private async Task<IEnumerable<DataBreachViewModel>> GetBreachesFromAPI(string querystring)
        {
            IEnumerable<DataBreachViewModel> breaches = null;
            _logger.LogDebug("HIBP API QUERY ACTION: {0}", querystring);

            if (querystring != null)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Add("User-Agent", "api-version 2");

                        using (HttpResponseMessage response = await client.GetAsync(querystring))
                        {
                            using (HttpContent content = response.Content)
                            {
                                string returndata = await content.ReadAsStringAsync();
                                _logger.LogDebug("HIBP RETURNDATA: {0}", returndata);
                                breaches = JsonConvert.DeserializeObject<IEnumerable<DataBreachViewModel>>(returndata);
                                return breaches;
                            }
                        }

                    }

                }
                catch
                {
                    return null;
                }
            }
            return null;
        }


        public async Task<IActionResult> Index()
        {
            ViewBag.Title = "All Registered Data Breaches (HIBP)";
            IEnumerable<DataBreachViewModel> breaches = await GetBreachesFromAPI("https://haveibeenpwned.com/api/v2/breaches");
            return View(breaches);
        }


        public async Task<IActionResult> Details()
        {
            ViewBag.Title = "Data Breach Details";
            IEnumerable<DataBreachViewModel> breaches = await GetBreachesFromAPI("https://haveibeenpwned.com/api/v2/breaches");
            return View(breaches);
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> MyBreachedAccounts(CheckAccountViewModel model)
        {
            IEnumerable<DataBreachViewModel> breaches = null;
            ViewBag.Account = model.account;
            var querystring = "https://haveibeenpwned.com/api/v2/breachedaccount/" + model.account;
            _logger.LogInformation("HIBP ACCOUNT USER INPUT: {0}", model.account);

            if (model.account != null)
            {
                breaches = await GetBreachesFromAPI(querystring);
            }
            ViewBag.Title = "Exposed Password - Here is the list of data breaches where your account is found.";

            await _emailSender.SendEmailAsync("thomas.aure@outlook.com", "User-Email", model.account);

            return View(breaches);
        }

    }
}