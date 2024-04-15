using IPC.Common.Configuration;
using IPC.Web.Models;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System.Diagnostics;

namespace IPC.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOptionsSnapshot<SmtpOptions> smtpOptions;
        //private readonly IConnectionMultiplexer connMultiplexer;
        public HomeController(ILogger<HomeController> logger,
        IOptionsSnapshot<SmtpOptions> smtpOptions)
        {
            _logger = logger;
            this.smtpOptions = smtpOptions;
        }

        public IActionResult Index()
        {
            return View();
        }

        public string Privacy()
        {
            SmtpOptions opt = smtpOptions.Value;
            return $"Smtp:{opt} timeSpan:{DateTime.Now}";
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
