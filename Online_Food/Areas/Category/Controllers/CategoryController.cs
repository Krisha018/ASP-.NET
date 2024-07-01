using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Online_Food.Areas.Category.Models;
using Online_Food.Areas.FoodOrder.Models;
using Online_Food.Areas.OFD_Users.Models;
using Online_Food.DAL;
using System.Data.Common;
using System.Data;

namespace Online_Food.Areas.Category.Controllers
{
    [Area("Category")]
    [Route("Category/[controller]/[action]")]
    public class CategoryController : Controller
    {
        private readonly IConfiguration configuration;

        public CategoryController(IConfiguration _configuration)
        {
            this.configuration = _configuration;
        }


        #region CategorySearchModel
        public IActionResult CategorySearch(CategorySearchModel CategorysSearch)
        {
            try
            {
                String ConnString = this.configuration.GetConnectionString("MyConnectionString");
                Database db = new SqlDatabase(ConnString);
                DbCommand dbCmd = db.GetStoredProcCommand("PR_Category_Search");
                db.AddInParameter(dbCmd, "@CategoryName", (DbType)SqlDbType.VarChar, CategorysSearch.CategoryName);
                db.AddInParameter(dbCmd, "CategoryType", (DbType)SqlDbType.VarChar, CategorysSearch.CategoryType);



                using (IDataReader dr = db.ExecuteReader(dbCmd))
                {
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    List<CategoryModel> modelList = new List<CategoryModel>();

                    foreach (DataRow row in dt.Rows)
                    {
                        CategoryModel users = new CategoryModel();
                        users.CategoryID = Convert.ToInt32(row["CategoryID"]);

                        users.CategoryName = row["CategoryName"].ToString();
                        users.CategoryType = row["CategoryType"].ToString();
                        users.Created = Convert.ToDateTime(row["Created"]);
                        users.Modified = Convert.ToDateTime(row["Modified"]);
                        // Set other properties here

                        modelList.Add(users);
                    }

                    return View("CategoryList", modelList);
                }
            }
            catch (Exception ex)
            {

                return View("CategoryList");

            }
        }
        #endregion

        #region SelectAll

        public IActionResult CategoryList()
        {

          Category_Base_DAL   dal = new Category_Base_DAL();
            return View(dal.CategoryList());

        }
        #endregion
        #region DeleteCategory
        public IActionResult DeleteCategory(int CategoryID)
        {

            Category_Base_DAL dal = new Category_Base_DAL();
            TempData["message"] = dal.DeleteCategory(CategoryID); ;
            return RedirectToAction("CategoryList");
        }
        #endregion
        #region CategoryAddEdit

        public IActionResult CategoryAddEdit(int CategoryID)
        {
            if (CategoryID == null)
            {
                return View();
            }
            else
            {
                Category_Base_DAL dal = new Category_Base_DAL();
                CategoryModel model = dal.Category_SelectByPK(CategoryID);
                return View(model);
            }

        }
        #endregion


        #region SaveForAddEdit
        public IActionResult SaveForAddEdit(CategoryModel model)
        {
            bool ans = false;
            Console.WriteLine(model.CategoryID);
            Category_Base_DAL dal = new Category_Base_DAL();
            if (model.CategoryID != 0)
            {
                ans = dal.Category_Update(model);
                TempData["message"] = "Record Updated Successfully";
            }
            else
            {
                ans = dal.Category_Insert(model);
                TempData["message"] = "Record Inserted Successfully";
            }
            if (ans)
            {
                return RedirectToAction("CategoryList");
            }
            else
            {
                return RedirectToAction("CategoryList");
            }
        }
        #endregion

        #region CategoryDetails
        public IActionResult CategoryDetails(int CategoryID)
        {
            Category_Base_DAL bal = new Category_Base_DAL();
            CategoryModel model = bal.Category_SelectByPK(CategoryID);
            return View(model);

        }
        #endregion
        #region #Cancel
        public IActionResult Cancel()
        {
            return RedirectToAction("CategoryList");
        }
        #endregion

    }
}
