using Microsoft.AspNetCore.Mvc;
using Online_Food.Areas.Menu.Models;
using Online_Food.Areas.Menu_Items.Models;
using Online_Food.DAL;
using Online_Food.Models;
using System.Diagnostics;

namespace Online_Food.Controllers
{
   
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Index()
        {
            User_About_Base_DAL count_Base_DAL = new User_About_Base_DAL();
            ViewBag.OFD_UsersCount = count_Base_DAL.OFD_UsersCount();
            ViewBag.OFD_RestaurantsCount = count_Base_DAL.OFD_RestaurantsCount();
            ViewBag.Menu_ItemsCount = count_Base_DAL.Menu_ItemsCount();

            UserMenu_Base_DAL dal = new UserMenu_Base_DAL();
            ViewBag.MenuList = dal.Menu_ItemsList();
            ViewBag.Menu_ItemsList = dal.Menu_ItemsList();

            FoodOrder_Base_DAL dal2 = new FoodOrder_Base_DAL();
            ViewBag.FoodOrderList = dal2.FoodOrderList();


            Category_Base_DAL dal1 = new Category_Base_DAL();
            ViewBag.CategoryList = dal1.CategoryList();

            return View();
        }

       



    }
}