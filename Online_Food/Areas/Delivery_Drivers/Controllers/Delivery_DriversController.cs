using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Online_Food.Areas.Delivery_Drivers.Models;
using Online_Food.Areas.OFD_Users.Models;
using Online_Food.DAL;
using System.Data.Common;
using System.Data;

namespace Online_Food.Areas.Delivery_Drivers.Controllers
{
    [Area("Delivery_Drivers")]
    [Route("Delivery_Drivers/[controller]/[action]")]
    public class Delivery_DriversController : Controller
    {
        private readonly IConfiguration configuration;

        public Delivery_DriversController(IConfiguration _configuration)
        {
            this.configuration = _configuration;
        }


        #region Delivery_DriversSearchModel
        public IActionResult Delivery_DriversSearch(Delivery_DriversSearchModel Delivery_DriverSearch)
        {
            try
            {
                String ConnString = this.configuration.GetConnectionString("MyConnectionString");
                Database db = new SqlDatabase(ConnString);
                DbCommand dbCmd = db.GetStoredProcCommand("PR_Delivery_Drivers_Search");
                db.AddInParameter(dbCmd, "@FirstName", (DbType)SqlDbType.VarChar, Delivery_DriverSearch.FirstName);
                db.AddInParameter(dbCmd, "@LastName", (DbType)SqlDbType.VarChar, Delivery_DriverSearch.LastName);



                using (IDataReader dr = db.ExecuteReader(dbCmd))
                {
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    List<Delivery_DriversModel> modelList = new List<Delivery_DriversModel>();

                    foreach (DataRow row in dt.Rows)
                    {
                        Delivery_DriversModel users = new Delivery_DriversModel();
                        users.DriverID = Convert.ToInt32(row["DriverID"]);
                        users.FirstName = row["FirstName"].ToString();
                        users.LastName = row["LastName"].ToString();
                        users.PhoneNumber = row["PhoneNumber"].ToString();
                        users.DriverLicenceNo = row["DriverLicenceNo"].ToString();
                        users.DriverLicencePhoto = row["DriverLicencePhoto"].ToString();
                         users.Created = Convert.ToDateTime(row["Created"]);
                        users.Modified = Convert.ToDateTime(row["Modified"]);
                        // Set other properties here

                        modelList.Add(users);
                    }

                    return View("Delivery_DriversList", modelList);
                }
            }
            catch (Exception ex)
            {

                return null;

            }
        }
        #endregion
        #region SelectAll

        public IActionResult Delivery_DriversList()
        {

            Delivery_Drivers_Base_DAL dal = new Delivery_Drivers_Base_DAL();
            return View(dal.Delivery_DriversList());

        }
        #endregion
        #region DeleteDrivers
        public IActionResult DeleteDrivers(int DriverID)
        {

            Delivery_Drivers_Base_DAL dal = new Delivery_Drivers_Base_DAL();
            TempData["message"] = dal.DeleteDrivers(DriverID); ;
            return RedirectToAction("Delivery_DriversList");
        }
        #endregion
        #region Delivery_DriversAddEdit

        public IActionResult Delivery_DriversAddEdit(int DriverID)
        {
            if (DriverID == null)
            {
                return View();
            }
            else
            {
                Delivery_Drivers_Base_DAL dal = new Delivery_Drivers_Base_DAL();
                Delivery_DriversModel model = dal.Delivery_Drivers_SelectByPK(DriverID);
                return View(model);
            }

        }
        #endregion


        #region SaveForAddEdit
        public IActionResult SaveForAddEdit(Delivery_DriversModel model)
        {
            bool ans = false;
            Console.WriteLine(model.DriverID);
            Delivery_Drivers_Base_DAL dal = new Delivery_Drivers_Base_DAL();
            if (model.DriverID != 0)
            {
                ans = dal.Delivery_Drivers_Update(model);
                TempData["message"] = "Record Updated Successfully";
            }
            else
            {
                ans = dal.Delivery_Drivers_Insert(model);
                TempData["message"] = "Record Inserted Successfully";

            }
            if (ans)
            {
                return RedirectToAction("Delivery_DriversList");
            }
            else
            {
                return RedirectToAction("Delivery_DriversList");
            }
        }
        #endregion

        #region Delivery_DriversDetails
        public IActionResult Delivery_DriversDetails(int DriverID)
        {
            Delivery_Drivers_Base_DAL bal = new Delivery_Drivers_Base_DAL();
            Delivery_DriversModel model = bal.Delivery_Drivers_SelectByPK(DriverID);
            return View(model);

        }
        #endregion
        #region #Cancel
        public IActionResult Cancel()
        {
            return RedirectToAction("Delivery_DriversList");
        }
        #endregion
    }
}
