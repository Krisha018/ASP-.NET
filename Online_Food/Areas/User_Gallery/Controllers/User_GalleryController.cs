using Microsoft.AspNetCore.Mvc;
using Online_Food.DAL;

namespace Online_Food.Areas.User_Gallery.Controllers
{
    [Area("User_Gallery")]
    [Route("User_Gallery/[controller]/[action]")]
    public class User_GalleryController : Controller
    {
        public IActionResult User_GalleryList()
        {
            FoodOrder_Base_DAL dal1 = new FoodOrder_Base_DAL();
            ViewBag.FoodOrderList = dal1.FoodOrderList();
            return View();  
        }
    }
}
