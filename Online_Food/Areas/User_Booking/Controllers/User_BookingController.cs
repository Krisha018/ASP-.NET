using Microsoft.AspNetCore.Mvc;
using Online_Food.Areas.Menu_Items.Models;
using Online_Food.Areas.OFD_Users.Models;
using Online_Food.Areas.User_Booking.Models;
using Online_Food.DAL;
using System.Data;

namespace Online_Food.Areas.User_Booking.Controllers
{
    [Area("User_Booking")]
    [Route("User_Booking/[controller]/[action]")]
    public class User_BookingController : Controller
    {
        public IActionResult User_BookingList()
        {
            OFD_Users_Base_DAL dal = new OFD_Users_Base_DAL();
            ViewBag.OFD_UsersList = dal.OFD_UsersList();
            return View();


         }
        #region OFD_User_DropDownList

        public void OFD_User_DropDownList()
        {
           Booking_Base_DAL order = new Booking_Base_DAL();
            DataTable dt = order.OFD_User_DropDownList();
            List<OFD_UserDropdownModel> user = new List<OFD_UserDropdownModel>();
            foreach (DataRow data in dt.Rows)
            {
                OFD_UserDropdownModel usermodel = new OFD_UserDropdownModel();
                usermodel.UserID = Convert.ToInt32(data["UserID"]);
                usermodel.FirstName = data["FirstName"].ToString();
                user.Add(usermodel);
            }
            ViewBag.OFD_User_DropDownList = user;
        }
        #endregion


        #region Menu_DropDownList

        public void Menu_DropDownList()
        {
            Booking_Base_DAL order = new Booking_Base_DAL();
            DataTable dt = order.Menu_DropDownList();
            List<Menu_ItemsDropdownModel> countryDropDownModelList = new List<Menu_ItemsDropdownModel>();
            foreach (DataRow data in dt.Rows)
            {
                Menu_ItemsDropdownModel countryDropDownModel = new Menu_ItemsDropdownModel();
                countryDropDownModel.MenuItemID = Convert.ToInt32(data["MenuItemID"]);
                countryDropDownModel.MenuItemName = data["MenuItemName"].ToString();
                countryDropDownModelList.Add(countryDropDownModel);
            }
            ViewBag.Menu_DropDownList = countryDropDownModelList;
        }
        #endregion

        #region AddEditUsers

        public IActionResult BookingAddEdit(int BookingID)
        {
            OFD_User_DropDownList();
            Menu_DropDownList();
            if (BookingID == null)
            {
                return View();
            }
            else
            {
                User_Booking_Base_DAL dal = new User_Booking_Base_DAL();
                 User_BookingModel model = dal.Booking_SelectByPK(BookingID);
                return View(model);
            }

        }
        #endregion


        #region SaveForAddEdit
        public IActionResult SaveForAddEdit(User_BookingModel model)
        {
             OFD_User_DropDownList();
            Menu_DropDownList();
            bool ans = false;
            Console.WriteLine(model.UserID);
            User_Booking_Base_DAL dal = new User_Booking_Base_DAL();
            if (model.BookingID != 0)
            {
                ans = dal.Booking_Update(model);
                TempData["message"] = "Record Updated Successfully";
            }
            else
            {
                ans = dal.Booking_Insert(model);
                TempData["message"] = "Record Inserted Successfully";
            }
            if (ans)
            {
                return RedirectToAction("BookingList");
            }
            else
            {
                return RedirectToAction("BookingList");
            }
        }
        #endregion



    }
}
