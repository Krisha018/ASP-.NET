using Microsoft.AspNetCore.Mvc;
using Online_Food.Areas.Menu_Items.Models;
using Online_Food.DAL;
using System.Data.SqlClient;
using System.Data;
using Online_Food.Areas.Cart.Models;

namespace Online_Food.Areas.User_Cart.Controllers
{
    [Area("User_Cart")]
    [Route("User_Cart/[controller]/[action]")]
    public class User_CartController : Controller
    {
        private IConfiguration Configuration;
        public User_CartController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        public IActionResult User_CartList()
        {
            UserMenu_Base_DAL dal1 = new UserMenu_Base_DAL();
            ViewBag.Menu_ItemsList = dal1.Menu_ItemsList();

            FoodOrder_Base_DAL dal=new FoodOrder_Base_DAL();
            ViewBag.FoodOrderList= dal.FoodOrderList();
        

            OFD_Restaurants_Base_DAL dal2 = new OFD_Restaurants_Base_DAL();
            ViewBag.OFD_RestaurantsList = dal2.OFD_RestaurantsList();

            return View();
        }
        public IActionResult BuyOrderList()
        {
            UserMenu_Base_DAL dal1 = new UserMenu_Base_DAL();
            ViewBag.Menu_ItemsList = dal1.Menu_ItemsList();
            return View();
        }

        public IActionResult Add_Cart()
        {
            UserMenu_Base_DAL dal1 = new UserMenu_Base_DAL();
            ViewBag.Menu_ItemsList = dal1.Menu_ItemsList();

            return View();
        }


        //User_cart_DAL dal = new User_cart_DAL();



        //public IActionResult AddToCart(int CartID, double TotalAmount, double Qty)
        //{
        //    using (var Conn = new SqlConnection(Configuration.GetConnectionString("MyConnectionString")))
        //    {
        //        Conn.Open();

        //        using (var Cmd = Conn.CreateCommand())
        //        {
        //            Cmd.CommandType = CommandType.StoredProcedure;
        //            Cmd.CommandText = "InsertCartItem";
        //            Cmd.Parameters.AddWithValue("@CartID", CartID);
        //            Cmd.Parameters.AddWithValue("@UserID", 10);
        //            Cmd.Parameters.AddWithValue("@Qty", Qty);
        //            Cmd.Parameters.AddWithValue("@TotalAmount", TotalAmount);
        //            Cmd.ExecuteNonQuery();
        //        }

        //        using (var Cmd = Conn.CreateCommand())
        //        {
        //            Cmd.CommandType = CommandType.StoredProcedure;
        //            Cmd.CommandText = "UpdateCartItem";
        //            Cmd.Parameters.AddWithValue("@CartID", CartID);
        //            Cmd.Parameters.AddWithValue("IsCart", 1);
        //            Cmd.ExecuteNonQuery();
        //        }

        //        return RedirectToAction("CartDisplay");
        //    }
        //}

        //public IActionResult CartDisplay()
        //{
        //    DataTable cartItems = new DataTable();
        //    double totalAmount = 0;

        //    using (var Conn = new SqlConnection(Configuration.GetConnectionString("MyConnectionString")))
        //    {
        //        Conn.Open();

        //        using (var Cmd = Conn.CreateCommand())
        //        {
        //            Cmd.CommandType = CommandType.StoredProcedure;
        //            Cmd.CommandText = "SelectAllCartItems";

        //            using (var reader = Cmd.ExecuteReader())
        //            {
        //                cartItems.Load(reader);
        //            }

        //            var totalAmountObject = cartItems.Compute("Sum(TotalAmount)", string.Empty);
        //            if (totalAmountObject != DBNull.Value)
        //            {
        //                totalAmount = Convert.ToDouble(totalAmountObject);
        //            }
        //        }
        //    }

        //    var viewModel = new CartModel
        //    {
               
        //    };

        //    return View(viewModel);
        //}

        //public IActionResult DeleteCartItem(int CartID)
        //{
        //    SqlConnection Conn = new
        //    SqlConnection(Configuration.GetConnectionString("MyConnectionString"));
        //    Conn.Open();
        //    SqlCommand Cmd = Conn.CreateCommand();
        //    Cmd.CommandType = CommandType.StoredProcedure;
        //    Cmd.CommandText = "DeleteCartItem";
        //    Cmd.Parameters.AddWithValue("@CartID", CartID);
        //    Cmd.ExecuteNonQuery();
        //    Cmd.Parameters.Clear();
        //    Cmd = Conn.CreateCommand();
        //    Cmd.CommandType = CommandType.StoredProcedure;
        //    Cmd.CommandText = "UpdateCartItem";
        //    Cmd.Parameters.AddWithValue("@CartID", CartID);
        //    Cmd.Parameters.AddWithValue("IsCart", 1);
        //    Cmd.ExecuteNonQuery();
        //    Conn.Close();
        //    return RedirectToAction("BuyOrderList");
        //}

    }
}
