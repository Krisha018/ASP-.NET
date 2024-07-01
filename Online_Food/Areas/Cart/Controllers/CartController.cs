using Microsoft.AspNetCore.Mvc;
using Online_Food.Areas.Cart.Models;
using Online_Food.Areas.OFD_Users.Models;
using Online_Food.DAL;

namespace Online_Food.Areas.Cart.Controllers
{
    [Area("Cart")]
    [Route("Cart/[controller]/[action]")]
    public class CartController : Controller
    {
        private readonly IConfiguration configuration;

        public CartController(IConfiguration _configuration)
        {
            this.configuration = _configuration;
        }


        #region SelectAll

        public IActionResult CartList()
        {

            Cart_DAL dal = new Cart_DAL();
            return View(dal.CartList());

        }
        #endregion
        #region DeleteCart
        public IActionResult DeleteCart(int CartID)
        {

            Cart_DAL dal = new Cart_DAL();
            TempData["message"] = dal.DeleteCart(CartID); ;
            return RedirectToAction("CartList");
        }
        #endregion
        #region CartAddEdit

        public IActionResult CartAddEdit(int CartID)
        {
            if (CartID == null)
            {
                return View();
            }
            else
            {
                Cart_DAL dal = new Cart_DAL();
                CartModel model = dal.SelectCartItemByID(CartID);
                return View(model);
            }

        }
        #endregion


        #region SaveForAddEdit
        public IActionResult SaveForAddEdit(CartModel model)
        {
            bool ans = false;
            Console.WriteLine(model.CartID);
            Cart_DAL dal = new Cart_DAL();
            if (model.CartID != 0)
            {
                ans = dal.UpdateCartItem(model);
                TempData["message"] = "Record Updated Successfully";
            }
            else
            {
                ans = dal.InsertCartItem(model);
                TempData["message"] = "Record Inserted Successfully";
            }
            if (ans)
            {
                return RedirectToAction("CartList");
            }
            else
            {
                return RedirectToAction("CartList");
            }
        }
        #endregion

        #region CartDetails
        public IActionResult CartDetails(int CartID)
        {
            Cart_DAL bal = new Cart_DAL();
            CartModel model = bal.SelectCartItemByID(CartID);
            return View(model);

        }
        #endregion
        #region #Cancel
        public IActionResult Cancel()
        {
            return RedirectToAction("CartList");
        }
        #endregion


    }
}
