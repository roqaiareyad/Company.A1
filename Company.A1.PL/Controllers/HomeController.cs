using System.Diagnostics;
using System.Text;
using Company.A1.PL.Models;
using Company.A1.PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company.A1.PL.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IScopedService scopedService01;
        private readonly IScopedService scopedService02;
        private readonly ISingletonServices singletonService01;
        private readonly ISingletonServices singletonService02;
        private readonly ITransientService transientService01;
        private readonly ITransientService transientService02;

        public HomeController(
            ILogger<HomeController> logger,
            IScopedService scopedService01,
            IScopedService scopedService02,
            ISingletonServices singletonService01,
            ISingletonServices singletonService02,
            ITransientService transientService01,
            ITransientService transientService02
            )
        {
            _logger = logger;
            this.scopedService01 = scopedService01;
            this.scopedService02 = scopedService02;
            this.singletonService01 = singletonService01;
            this.singletonService02 = singletonService02;
            this.transientService01 = transientService01;
            this.transientService02 = transientService02;
        }

        public IActionResult Index()
        {
            return View();
        }
        public string TestLifeTime()
        {
            StringBuilder sb = new();
            sb.Append($"scopedService01 :: {scopedService01.GetGuid()}\n");
            sb.Append($"scopedService02 :: {scopedService02.GetGuid()}\n");
            sb.Append($"singletonService01 :: {singletonService01.GetGuid()}\n");
            sb.Append($"singletonService02 :: {singletonService02.GetGuid()}\n");
            sb.Append($"transientService01 :: {transientService01.GetGuid()}\n");
            sb.Append($"transientService02 :: {transientService02.GetGuid()}\n");
            return sb.ToString();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
