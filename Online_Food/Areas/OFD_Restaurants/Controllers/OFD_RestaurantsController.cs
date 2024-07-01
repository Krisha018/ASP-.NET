using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Online_Food.Areas.Menu_Items.Models;
using Online_Food.Areas.OFD_Restaurants.Models;
using Online_Food.Areas.OFD_Users.Models;
using Online_Food.DAL;
using Online_Food.Models;
using System.Data;
using System.Data.Common;


namespace Online_Food.Areas.OFD_Restaurants.Controllers
{
    [Area("OFD_Restaurants")]
    [Route("OFD_Restaurants/[controller]/[action]")]
    public class OFD_RestaurantsController : Controller
    {
        private readonly IConfiguration configuration;

        public OFD_RestaurantsController(IConfiguration _configuration) 
        {
            this.configuration = _configuration;
        }

        #region OFD_RestaurantsSearchModel
        public IActionResult OFD_RestaurantSearch(OFD_RestaurantsSearchModel OFD_RestaurantsSearch)
        {
            try
            {
                String ConnString = this.configuration.GetConnectionString("MyConnectionString");
                Database db = new SqlDatabase(ConnString);
                DbCommand dbCmd = db.GetStoredProcCommand("PR_OFD_Restaurants_Search");
                db.AddInParameter(dbCmd, "@Name", (DbType)SqlDbType.VarChar, OFD_RestaurantsSearch.Name);
                db.AddInParameter(dbCmd, "@Address", (DbType)SqlDbType.VarChar, OFD_RestaurantsSearch.Address);




                using (IDataReader dr = db.ExecuteReader(dbCmd))
                {
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    List<OFD_RestaurantsModel> modelList = new List<OFD_RestaurantsModel>();

                    foreach (DataRow row in dt.Rows)
                    {
                        OFD_RestaurantsModel users = new OFD_RestaurantsModel();
                        users.RestaurantID = Convert.ToInt32(row["RestaurantID"]);
                        users.Name = row["Name"].ToString();
                        users.Description = row["Description"].ToString();
                        users.Address = row["Address"].ToString();
                        users.PhoneNumber = row["PhoneNumber"].ToString();
                        users.Rating = Convert.ToInt32(row["Rating"]);
                        users.Created = Convert.ToDateTime(row["Created"]);
                        users.Modified = Convert.ToDateTime(row["Modified"]);
                        // Set other properties here

                        modelList.Add(users);
                    }

                    return View("OFD_RestaurantsList", modelList);
                }
            }
            catch (Exception ex)
            {

                return null;

            }
        }
        #endregion


        #region SelectAll

        public IActionResult OFD_RestaurantsList()
        {

            OFD_Restaurants_Base_DAL dal = new OFD_Restaurants_Base_DAL();
            return View(dal.OFD_RestaurantsList());

        }


        #endregion
        #region DeleteUsers
        public IActionResult DeleteUsers(int RestaurantID)
        {

            OFD_Restaurants_Base_DAL dal = new OFD_Restaurants_Base_DAL();
            TempData["message"] = dal.DeleteUsers(RestaurantID); ;
            return RedirectToAction("OFD_RestaurantsList");
        }
        #endregion

        #region AddEditUsers

        public IActionResult OFD_RestaurantsAddEdit(int RestaurantID)
        {
            if (RestaurantID == null)
            {
                return View();
            }
            else
            {
                OFD_Restaurants_Base_DAL dal = new OFD_Restaurants_Base_DAL();
                OFD_RestaurantsModel model = dal.OFD_Restaurants_SelectByPK(RestaurantID);
                return View(model);
            }

        }
        #endregion

        #region SaveForAddEdit
        public IActionResult SaveForAddEdit(OFD_RestaurantsModel model)
        {
            bool ans = false;
            Console.WriteLine(model.RestaurantID);
            OFD_Restaurants_Base_DAL dal = new OFD_Restaurants_Base_DAL();
            if (model.RestaurantID != 0)
            {
                ans = dal.OFD_Restaurants_Update(model);
                TempData["message"] = "Record Updated Successfully";
            }
            else
            {
                ans = dal.OFD_Restaurants_Insert(model);
                TempData["message"] = "Record Inserted Successfully";
            }
            if (ans)
            {
                return RedirectToAction("OFD_RestaurantsList");
            }
            else
            {
                return RedirectToAction("OFD_RestaurantsList");
            }
        }
        #endregion

        #region OFD_UserDetails
        public IActionResult OFD_RestaurantsDetails(int RestaurantID)
        {
            OFD_Restaurants_Base_DAL bal = new OFD_Restaurants_Base_DAL();
            OFD_RestaurantsModel model = bal.OFD_Restaurants_SelectByPK(RestaurantID);
            return View(model);

        }
        #endregion

        #region #Cancel
        public IActionResult Cancel()
        {
            return RedirectToAction("OFD_RestaurantsList");
        }
        #endregion
    }
}
