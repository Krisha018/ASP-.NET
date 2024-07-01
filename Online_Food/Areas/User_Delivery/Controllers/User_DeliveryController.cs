using Microsoft.AspNetCore.Mvc;

namespace Online_Food.Areas.User_Delivery.Controllers
{
    [Area("User_Delivery")]
    [Route("User_Delivery/[controller]/[action]")]
    public class User_DeliveryController : Controller
    {
        public IActionResult User_DeliveryList()
        {
            return View("User_DeliveryList");
        }
    }
}
