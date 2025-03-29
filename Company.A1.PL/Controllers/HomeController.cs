using System.Diagnostics;
using System.Text;
using Company.A1.PL.Models;
using Company.A1.PL.Services;
using Microsoft.AspNetCore.Mvc;

namespace Company.A1.PL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IScopedService scopedService01;
        private readonly IScopedService scopedService02;
        private readonly ITransientService transientService01;
        private readonly ITransientService transientService02;
        private readonly ISingletonServices singletonServices01;
        private readonly ISingletonServices singletonServices02;

        public HomeController(
            ILogger<HomeController> logger ,
            IScopedService scopedService01,
            IScopedService scopedService02,
            ITransientService transientService01,
            ITransientService transientService02,
            ISingletonServices singletonServices01,
            ISingletonServices singletonServices02

            )
        {
            _logger = logger;
            this.scopedService01 = scopedService01;
            this.scopedService02 = scopedService02;
            this.transientService01 = transientService01;
            this.transientService02 = transientService02;
            this.singletonServices01 = singletonServices01;
            this.singletonServices02 = singletonServices02;
        }
        public string TestLifeTime()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append($"ScopedService01 :: {scopedService01.GetGuid()}\n");
            builder.Append($"ScopedService02 :: {scopedService02.GetGuid()}\n\n");
            builder.Append($"transientService01 :: {transientService01.GetGuid()}\n");
            builder.Append($"transientService02 :: {transientService02.GetGuid()} \n\n");
            builder.Append($"singletonServices01 :: {singletonServices01.GetGuid()} \n");
            builder.Append($"singletonServices02 :: {singletonServices02.GetGuid()}\n\n");

            return builder.ToString();  
        }

        public IActionResult Index()
        {
            return View();
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
