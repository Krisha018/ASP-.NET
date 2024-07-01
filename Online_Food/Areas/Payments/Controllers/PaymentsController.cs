using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Online_Food.Areas.FoodOrder.Models;
using Online_Food.Areas.Payments.Models;
using Online_Food.DAL;
using System.Data.Common;
using System.Data;

namespace Online_Food.Areas.Payments.Controllers
{
    [Area("Payments")]
    [Route("Payments/[controller]/[action]")]
    public class PaymentsController : Controller
    {
        private readonly IConfiguration configuration;

        public PaymentsController(IConfiguration _configuration)
        {
            this.configuration = _configuration;
        }

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


        //#region PaymentsSearchModel
        //public IActionResult PaymentsSearch(PaymentsSearchModel PaymentSearch)
        //{
        //    try
        //    {
        //        String ConnString = this.configuration.GetConnectionString("MyConnectionString");
        //        Database db = new SqlDatabase(ConnString);
        //        DbCommand dbCmd = db.GetStoredProcCommand("PR_Payments_Search");
        //        db.AddInParameter(dbCmd, "@Amount", (DbType)SqlDbType.VarChar, PaymentSearch.Amount);
        //        db.AddInParameter(dbCmd, "@PaymentStatus", (DbType)SqlDbType.VarChar, PaymentSearch.PaymentStatus);



        //        using (IDataReader dr = db.ExecuteReader(dbCmd))
        //        {
        //            DataTable dt = new DataTable();
        //            dt.Load(dr);
        //            List<PaymentsModel> modelList = new List<PaymentsModel>();

        //            foreach (DataRow row in dt.Rows)
        //            {
        //                PaymentsModel users = new PaymentsModel();
        //                users.PaymentID = Convert.ToInt32(row["PaymentID"]);
        //                users.OrderID = Convert.ToInt32(row["OrderID"]);
        //                users.Amount = Convert.ToString(row["Amount"]);
        //                users.PaymentDate = Convert.ToDateTime(row["PaymentDate"]);
        //                users.PaymentStatus = row["PaymentStatus"].ToString();
        //                users.Created = Convert.ToDateTime(row["Created"]);
        //                users.Modified = Convert.ToDateTime(row["Modified"]);
        //                // Set other properties here

        //                modelList.Add(users);
        //            }

        //            return View("PaymentsList", modelList);
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        return null;

        //    }
        //}
        //#endregion

        #region PaymentsSearch

        public IActionResult PaymentsSearch(PaymentsSearchModel food)
        {
            try
            {
                // Ensure the parameter is not null or empty
                string TotalAmount = food.TotalAmount ?? string.Empty;
                string PaymentStatus = food.PaymentStatus ?? string.Empty;

                // Use the appropriate DbType and ParameterDirection
                Database db = new SqlDatabase(configuration.GetConnectionString("MyConnectionString"));
                DbCommand dbCmd = db.GetStoredProcCommand("PR_Payments_Search");

                db.AddInParameter(dbCmd, "@TotalAmount", DbType.String, string.IsNullOrEmpty(TotalAmount) ? (object)DBNull.Value : TotalAmount);
                db.AddInParameter(dbCmd, "@PaymentStatus", DbType.String, string.IsNullOrEmpty(PaymentStatus) ? (object)DBNull.Value : PaymentStatus);

                using (IDataReader dr = db.ExecuteReader(dbCmd))
                {
                    DataTable dt = new DataTable();
                    dt.Load(dr);

                    List<PaymentsModel> modelList = new List<PaymentsModel>();

                    foreach (DataRow row in dt.Rows)
                    {
                        PaymentsModel payments = new PaymentsModel
                        {
                            PaymentID = Convert.ToInt32(row["PaymentID"]),
                            OrderID = Convert.ToInt32(row["OrderID"]),
                            TotalAmount = Convert.ToString(row["TotalAmount"]),
                            Amount = Convert.ToString(row["Amount"]),
                            PaymentDate = Convert.ToDateTime(row["PaymentDate"]),
                            PaymentStatus = row["PaymentStatus"].ToString(),
                            Created = Convert.ToDateTime(row["Created"]),
                            Modified = Convert.ToDateTime(row["Modified"])
                        };

                        modelList.Add(payments);
                    }

                    // Pass the modelList to the "PaymentsList" view
                    return View("PaymentsList", modelList);
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                return View("PaymentsList", new List<PaymentsSearchModel>());
            }
        }



        #endregion





        #region SelectAll

        public IActionResult PaymentsList()
        {

            Payments_Base_DAL dal = new Payments_Base_DAL();
            return View(dal.PaymentsList());

        }


        #endregion
        #region DeletePayment
        public IActionResult DeletePayment(int PaymentID)
        {

            Payments_Base_DAL dal = new Payments_Base_DAL();
            TempData["message"] = dal.DeletePayment(PaymentID); ;
            return RedirectToAction("PaymentsList");
        }
        #endregion

        #region PaymentsAddEdit

        public IActionResult PaymentsAddEdit(int PaymentID)
        {
            FoodorderDropDownList();
            if (PaymentID == null)
            {
                return View();
            }
            else
            {
                Payments_Base_DAL dal = new Payments_Base_DAL();
                PaymentsModel model = dal.Payments_SelectByPK(PaymentID);
                return View(model);
            }

        }
        #endregion


        #region SaveForAddEdit
        public IActionResult SaveForAddEdit(PaymentsModel model)
        {
            bool ans = false;
            Console.WriteLine(model.PaymentID);
            Payments_Base_DAL dal = new Payments_Base_DAL();
            if (model.PaymentID != 0)
            {
                ans = dal.Payments_Update(model);
                TempData["message"] = "Record Updated Successfully";
            }
            else
            {
                ans = dal.Payments_Insert(model);
                TempData["message"] = "Record Inserted Successfully";
            }
            if (ans)
            {
                return RedirectToAction("PaymentsList");
            }
            else
            {
                return RedirectToAction("PaymentsList");
            }
        }
        #endregion

        #region PaymentsDetails
        public IActionResult PaymentsDetails(int PaymentID)
        {
            Payments_Base_DAL bal = new Payments_Base_DAL();
            PaymentsModel model = bal.Payments_SelectByPK(PaymentID);
            return View(model);

        }
        #endregion

        #region #Cancel
        public IActionResult Cancel()
        {
            return RedirectToAction("PaymentsList");
        }
        #endregion
    }
}
