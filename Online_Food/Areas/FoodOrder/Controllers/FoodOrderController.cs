using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Online_Food.Areas.FoodOrder.Models;
using Online_Food.DAL;
using System.Data.Common;
using System.Data;
using Online_Food.Areas.OFD_Users.Models;
using Online_Food.Areas.Menu_Items.Models;

namespace Online_Food.Areas.FoodOrder.Controllers
{
    [Area("FoodOrder")]
    [Route("FoodOrder/[controller]/[action]")]
    public class FoodOrderController : Controller
    {
        private readonly IConfiguration configuration;

        public FoodOrderController(IConfiguration _configuration)
        {
            this.configuration = _configuration;
        }

        #region OFD_User_DropDownList

        public void OFD_User_DropDownList()
        {
            FoodOrder_Main_DAL order = new FoodOrder_Main_DAL();
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

        #region FoodOrderSearch

        public IActionResult FoodOrderSearch(FoodOrderSearchModel food)
        {
            try
            {
                // Ensure the parameter is not null or empty
                string FirstName = food.FirstName ?? string.Empty;
                string TotalAmount = food.TotalAmount ?? string.Empty;

                // Use the appropriate DbType and ParameterDirection
                Database db = new SqlDatabase(configuration.GetConnectionString("MyConnectionString"));
                DbCommand dbCmd = db.GetStoredProcCommand("PR_Food_Order_Search2");

                db.AddInParameter(dbCmd, "@FirstName", DbType.String, FirstName);
                db.AddInParameter(dbCmd, "@TotalAmount", DbType.String, TotalAmount);

                using (IDataReader dr = db.ExecuteReader(dbCmd))
                {
                    DataTable dt = new DataTable();
                    dt.Load(dr);

                    List<FoodOrderModel> modelList = new List<FoodOrderModel>();

                    foreach (DataRow row in dt.Rows)
                    {
                        FoodOrderModel foodOrder = new FoodOrderModel();
                        foodOrder.OrderID = Convert.ToInt32(row["OrderID"]);
                        foodOrder.UserID = Convert.ToInt32(row["UserID"]);
                        foodOrder.FoodImg = row["FoodImg"].ToString();
                        foodOrder.FirstName = row["FirstName"].ToString();
                        foodOrder.OrderDate = Convert.ToDateTime(row["OrderDate"]);
                        foodOrder.TotalAmount = Convert.ToString(row["TotalAmount"]);
                        foodOrder.DeliveryAddress = row["DeliveryAddress"].ToString();
                        foodOrder.Created = Convert.ToDateTime(row["Created"]);
                        foodOrder.Modified = Convert.ToDateTime(row["Modified"]);

                        modelList.Add(foodOrder);
                    }

                    // Pass the modelList to the "FoodOrderList" view
                    return View("FoodOrderList", modelList);
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                return View("FoodOrderList", new List<FoodOrderSearchModel>());
            }
        }


        #endregion




        #region SelectAll

        public IActionResult FoodOrderList()
        {

            FoodOrder_Base_DAL dal = new FoodOrder_Base_DAL();
            return View(dal.FoodOrderList());

        }


        #endregion
        #region DeleteFood
        public IActionResult DeleteFood(int OrderID)
        {

            FoodOrder_Base_DAL dal = new FoodOrder_Base_DAL();
            TempData["message"] = dal.DeleteFood(OrderID); ;
            return RedirectToAction("FoodOrderList");
        }
        #endregion

        #region AddEditFood

        public IActionResult FoodOrderAddEdit(int OrderID)
        {
            OFD_User_DropDownList();
            if (OrderID == null)
            {
                return View();
            }
            else
            {
                FoodOrder_Base_DAL dal = new FoodOrder_Base_DAL();
                FoodOrderModel model = dal.FoodOrder_SelectByPK(OrderID);
                return View(model);
            }

        }
        #endregion

        #region SaveForAddEdit
        public IActionResult SaveForAddEdit(FoodOrderModel model)
        {
            bool ans = false;
            Console.WriteLine(model.OrderID);
            FoodOrder_Base_DAL dal = new FoodOrder_Base_DAL();
            if (model.OrderID != 0)
            {
                ans = dal.FoodOrder_Update(model);
                TempData["message"] = "Record Updated Successfully";
            }
            else
            {
                ans = dal.FoodOrder_Insert(model);
                TempData["message"] = "Record Inserted Successfully";
            }
            if (ans)
            {
                return RedirectToAction("FoodOrderList");
            }
            else
            {
                return RedirectToAction("FoodOrderList");
            }
        }
        #endregion

        #region FoodOrderDetails
        public IActionResult FoodOrderDetails(int OrderID)
        {
            FoodOrder_Base_DAL bal = new FoodOrder_Base_DAL();
            FoodOrderModel model = bal.FoodOrder_SelectByPK(OrderID);
            return View(model);

        }
        #endregion

        #region #Cancel
        public IActionResult Cancel()
        {
            return RedirectToAction("FoodOrderList");
        }
        #endregion

    }
}
