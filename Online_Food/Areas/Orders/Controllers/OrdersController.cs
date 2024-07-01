using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Online_Food.Areas.Menu_Items.Models;
using Online_Food.Areas.OFD_Users.Models;
using Online_Food.Areas.Orders.Models;
using Online_Food.DAL;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using Online_Food.Areas.FoodOrder.Models;
using System.Reflection;




namespace Online_Food.Areas.Orders.Controllers
{
    [Area("Orders")]
    [Route("Orders/[controller]/[action]")]
    public class OrdersController : Controller
    {
        private readonly IConfiguration configuration;

        public OrdersController(IConfiguration _configuration)
        {
            this.configuration = _configuration;
        }

        #region Menu_DropDownList
       
        public void Menu_DropDownList()
        {
            Orders_Main_DAL order = new Orders_Main_DAL();
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


        #region FoodorderDropDownList

        public void FoodorderDropDownList()
        {
            Orders_Main_DAL order = new Orders_Main_DAL();
            DataTable dt = order.FoodorderDropDownList();
            List<FoodOrderDropdowModel> foodDropDownModelList = new List<FoodOrderDropdowModel>();
            foreach (DataRow data in dt.Rows)
            {
                FoodOrderDropdowModel countryDropDownModel = new FoodOrderDropdowModel();
                countryDropDownModel.OrderID = Convert.ToInt32(data["OrderID"]);
                countryDropDownModel.TotalAmount = data["TotalAmount"].ToString();
                foodDropDownModelList.Add(countryDropDownModel);
            }
            ViewBag.FoodorderDropDownList = foodDropDownModelList;
        }
        #endregion



        #region OrdersSearch
        public IActionResult OrdersSearch(OrdersSearchModel orders)
        {
            try
            {
                // Ensure the parameter is not null or empty
                string MenuItemName = orders.MenuItemName ?? string.Empty;
                //string TotalAmount = orders.TotalAmount ?? string.Empty;

                // Use the appropriate DbType and ParameterDirection
                Database db = new SqlDatabase(configuration.GetConnectionString("MyConnectionString"));
                DbCommand dbCmd = db.GetStoredProcCommand("PR_OrderItems_Search");

                db.AddInParameter(dbCmd, "@MenuItemName", DbType.String, MenuItemName);
                //db.AddInParameter(dbCmd, "@TotalAmount", DbType.String, TotalAmount);

                using (IDataReader dr = db.ExecuteReader(dbCmd))
                {
                    DataTable dt = new DataTable();
                    dt.Load(dr);

                    List<OrdersModel> modelList = new List<OrdersModel>();

                    foreach (DataRow row in dt.Rows)
                    {
                        OrdersModel order = new OrdersModel();
                        order.OrderItemID = Convert.ToInt32(row["OrderItemID"]);
                        order.TotalAmount = row["TotalAmount"].ToString();
                        order.OrderID = Convert.ToInt32(row["OrderID"]);
                        order.Subtotal = Convert.ToDecimal(row["Subtotal"]);
                        order.Qty = Convert.ToInt32(row["Qty"]);
                        order.MenuItemID = Convert.ToInt32(row["MenuItemID"]);
                        order.MenuItemName = row["MenuItemName"].ToString();
                        order.Created = Convert.ToDateTime(row["Created"]);
                        order.Modified = Convert.ToDateTime(row["Modified"]);

                        modelList.Add(order);
                    }

                    // Pass the modelList to the "FoodOrderList" view
                    return View("OrdersList", modelList);
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                return View("OrdersList", new List<OrdersSearchModel>());
            }
        }



        #endregion

        #region SelectAll

        public IActionResult OrdersList()
        {

            Orders_Base_DAL dal = new Orders_Base_DAL();
            return View(dal.OrdersList());

        }


        #endregion
        #region DeleteOrder
        public IActionResult DeleteOrder(int OrderItemID)
        {

            Orders_Base_DAL dal = new Orders_Base_DAL();
            TempData["message"] = dal.DeleteOrder(OrderItemID); ;
            return RedirectToAction("OrdersList");
        }
        #endregion



       

        #region AddEditOrders

        public IActionResult OrdersAddEdit(int OrderItemID)
        {
            Menu_DropDownList();
            FoodorderDropDownList();

           if (OrderItemID == 0)
            {
                return View();
            }
            else
            {
                Orders_Base_DAL dal = new Orders_Base_DAL();
                OrdersModel model = dal.OrderItems_SelectByPK(OrderItemID);
                return View(model);
            }

        }
        #endregion

       
        #region SaveForAddEdit
    
        public IActionResult SaveForAddEdit(OrdersModel model)
        {
          
            bool ans = false;
            Menu_DropDownList();
            Orders_Base_DAL dal = new Orders_Base_DAL();
            if (model.OrderItemID != 0)
            {
                ans = dal.OrderItems_Update(model);
                TempData["message"] = "Record Updated Successfully";

            }
            else
            {
                ans = dal.OrderItems_Insert(model);
                TempData["message"] = "Record Inserted Successfully";

            }
            if (ans)
            {
                return RedirectToAction("OrdersList");
            }
            else
            {
                return RedirectToAction("OrdersList");
            }
        }
        #endregion

        #region OrdersDetails
        public IActionResult OrdersDetails(int OrderItemID)
        {
          
            Orders_Base_DAL bal = new Orders_Base_DAL();
            OrdersModel model = bal.OrderItems_SelectByPK(OrderItemID);
            return View(model);

        }
        #endregion

        #region #Cancel
        public IActionResult Cancel()
        {
            return RedirectToAction("OrdersList");
        }
        #endregion
        
    }
}
