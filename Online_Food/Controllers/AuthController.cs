using Microsoft.AspNetCore.Mvc;
using Online_Food.DAL;
using Online_Food.Models;



namespace Online_Food.Controllers
{
    public class AuthController : Controller
    {
        private readonly Auth_DAL _auth;

        #region LoginView
        public IActionResult Login()
        {
            return View();
        }
        #endregion

        [HttpPost]
        #region Login
        public IActionResult Login(LoginModel login)
        {
            if (login.Email != null && login.Password != null)
            {
                Auth_DAL auth = new Auth_DAL();
                var User = auth.PR_UserLogin1(login.Email);
                if (User != null)
                {
                    if (User.Password.Equals(login.Password))
                    {
                        if (User.IsAdmin == "admin")
                        {

                            return RedirectToAction("Index", "Admin", new { area = "Admin" });
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }


                else
                {
                    // user not found
                }
            }

            return View();
        }

        #endregion
    }
}
