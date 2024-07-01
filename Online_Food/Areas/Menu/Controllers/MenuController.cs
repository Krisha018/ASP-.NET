using Microsoft.AspNetCore.Mvc;
using Online_Food.Areas.Menu_Items.Models;
using Online_Food.DAL;

namespace Online_Food.Areas.Menu.Controllers
{
    [Area("Menu")]
    [Route("Menu/[controller]/[action]")]
    public class MenuController : Controller
    {
        public IActionResult MenuList()
        {
           
            UserMenu_Base_DAL dal1 = new UserMenu_Base_DAL();
            ViewBag.Menu_ItemsList = dal1.Menu_ItemsList();

            Category_Base_DAL dal = new Category_Base_DAL();
            ViewBag.CategoryList = dal.CategoryList(); 
            return View();
           
        }
       
    }
}
