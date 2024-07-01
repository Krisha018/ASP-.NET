using Microsoft.AspNetCore.Mvc;
using Online_Food.DAL;

namespace Online_Food.Areas.About.Controllers
{
    [Area("About")]
    [Route("About/[controller]/[action]")]
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            User_About_Base_DAL count_Base_DAL = new User_About_Base_DAL();
            ViewBag.OFD_UsersCount = count_Base_DAL.OFD_UsersCount();
            ViewBag.OFD_RestaurantsCount = count_Base_DAL.OFD_RestaurantsCount();
            ViewBag.Menu_ItemsCount = count_Base_DAL.Menu_ItemsCount();


            return View("AboutList");
        }

    }
}
