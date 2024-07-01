using AntiForgeryToken.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AntiForgeryToken.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [ValidateAntiForgeryToken]
        public ActionResult TransferAmt()
        {
            return Content(Request.Form["amount"] + "has been tranfer to account" + Request.Form["account"]);
        }
        public IActionResult Index()
        {
            return View("TransferAmt");
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
