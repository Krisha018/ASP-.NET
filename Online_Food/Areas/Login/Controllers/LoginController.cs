using Online_Food.Areas.Login.Models;
using Online_Food.DAL;
using Microsoft.AspNetCore.Mvc;


namespace Online_Food.Areas.Login.Controllers
{
    [Area("Login")]
    [Route("Login/{controller}/{action}")]
    public class LoginController : Controller
    {


        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(LoginModel model)
        {

            if (model.Email != null && model.Password != null)
            {
                Login_Base_DAL login_Base_DAL = new Login_Base_DAL();
                LoginModel loginModel = login_Base_DAL.Login(model);
                Console.WriteLine(loginModel.Password + "Hello");
                if (loginModel != null)
                {
                    if (loginModel.Password.Equals(model.Password))
                    {
                        //redirect to main page
                        return RedirectToAction("Index", "Admin", new { area = "Admin" });

                    }
                    else
                    {
                        //password not match
                        TempData["Message"] = "password not match";
                        return View("Index");

                    }
                }
                else

                {
                    //User Not found
                    TempData["Message"] = "User Not found";
                    return View("Index");
                }

            }
            else
            {
                // empty
                return View("Index");
            }
        }
    }
}
