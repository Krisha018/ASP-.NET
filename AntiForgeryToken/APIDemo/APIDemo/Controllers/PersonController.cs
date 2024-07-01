using Microsoft.AspNetCore.Mvc;

namespace APIDemo.Controllers
{
    [ApiController]

    public class PersonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
