using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Online_Food.Areas.OFD_Users.Models;
using Online_Food.DAL;
using System.Data.SqlClient;
using System.Data;
using Online_Food.Areas.Booking.Models;
using System.Data.Common;
using Online_Food.Areas.Menu_Items.Models;

namespace Online_Food.Areas.Booking.Controllers
{
    [Area("Booking")]
    [Route("Booking/[controller]/[action]")]
    public class BookingController : Controller
    {
        private readonly IConfiguration configuration;

        public BookingController(IConfiguration _configuration)
        {
            this.configuration = _configuration;
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
            ViewBag.DropDownMenu = countryDropDownModelList;
        }
        #endregion



        #region BookingSearchModel
        public IActionResult BookingSearch(BookingSearchModel bookingSearch)
        {
            try
            {
                String ConnString = this.configuration.GetConnectionString("MyConnectionString");
                Database db = new SqlDatabase(ConnString);
                DbCommand dbCmd = db.GetStoredProcCommand("Booking_Search");
                db.AddInParameter(dbCmd, "@FirstName", (DbType)SqlDbType.VarChar, bookingSearch.FirstName);
                db.AddInParameter(dbCmd, "@MenuItemName", (DbType)SqlDbType.VarChar, bookingSearch.MenuItemName);



                using (IDataReader dr = db.ExecuteReader(dbCmd))
                {
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    List<BookingModel> modelList = new List<BookingModel>();

                    foreach (DataRow row in dt.Rows)
                    {
                        BookingModel users = new BookingModel();
                        users.BookingID = Convert.ToInt32(row["BookingID"]);
                        users.UserID = Convert.ToInt32(row["UserID"]);
                        users.FirstName = row["FirstName"].ToString();

                        users.Address = row["Address"].ToString();
                        users.OrderDate = Convert.ToDateTime(row["OrderDate"]);
                        users.MenuItemID = Convert.ToInt32(row["MenuItemID"]);
                        users.MenuItemName = row["MenuItemName"].ToString();


                        users.Email = row["Email"].ToString();
                       
                        users.PhoneNumber = row["PhoneNumber"].ToString();
                        users.Created = Convert.ToDateTime(row["Created"]);
                        users.Modified = Convert.ToDateTime(row["Modified"]);
                        // Set other properties here

                        modelList.Add(users);
                    }

                    return View("BookingList", modelList);
                }
            }
            catch (Exception ex)
            {

                return null;

            }
        }
        #endregion

        #region SelectAll

        public IActionResult BookingList()
        {

            Booking_Base_DAL dal = new Booking_Base_DAL();
            return View(dal.BookingList());

        }
        #endregion
        #region DeleteUsers
        public IActionResult DeleteUsers(int BookingID)
        {

            Booking_Base_DAL dal = new Booking_Base_DAL();
            TempData["message"] = dal.DeleteUsers(BookingID); ;
            return RedirectToAction("BookingList");
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
                Booking_Base_DAL dal = new Booking_Base_DAL();
                BookingModel model = dal.Booking_SelectByPK(BookingID);
                return View(model);
            }

        }
        #endregion


        #region SaveForAddEdit
        public IActionResult SaveForAddEdit(BookingModel model)
        {
            bool ans = false;
            Console.WriteLine(model.UserID);
            Booking_Base_DAL dal = new Booking_Base_DAL();
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

        #region BookingDetails
        public IActionResult BookingDetails(int BookingID)
        {
            Booking_Base_DAL bal = new Booking_Base_DAL();
            BookingModel model = bal.Booking_SelectByPK(BookingID);
            return View(model);

        }
        #endregion
        #region #Cancel
        public IActionResult Cancel()
        {
            return RedirectToAction("BookingList");
        }
        #endregion


       


    }
}
