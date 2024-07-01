using Microsoft.AspNetCore.Mvc;
using Online_Food.DAL;

namespace Online_Food.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/{controller}/{action}")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            Count_Base_DAL count_Base_DAL = new Count_Base_DAL();
            ViewBag.OFD_UsersCount = count_Base_DAL.OFD_UsersCount();
            ViewBag.OFD_RestaurantsCount = count_Base_DAL.OFD_RestaurantsCount();
            ViewBag.Menu_ItemsCount = count_Base_DAL.Menu_ItemsCount();
            ViewBag.FoodOrderCount = count_Base_DAL.FoodOrderCount();
            ViewBag.OrdersCount = count_Base_DAL.OrdersCount();
            ViewBag.PaymentsCount = count_Base_DAL.PaymentsCount();
            ViewBag.CategoryCount = count_Base_DAL.CategoryCount();
            ViewBag.Delivery_DriversCount = count_Base_DAL.Delivery_DriversCount();

            return View();
           
        }
    }
}
