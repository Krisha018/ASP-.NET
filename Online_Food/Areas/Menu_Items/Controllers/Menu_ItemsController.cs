using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Online_Food.Areas.Menu_Items.Models;
using Online_Food.Areas.OFD_Users.Models;
using Online_Food.DAL;
using System.Data.Common;
using System.Data;
using Online_Food.Areas.Category.Models;
using Online_Food.Areas.OFD_Restaurants.Models;
using System.Data.SqlClient;

namespace Online_Food.Areas.Menu_Items.Controllers
{

    [Area("Menu_Items")]
    [Route("Menu_Items/[controller]/[action]")]
    public class Menu_ItemsController:Controller

    {
        private readonly IConfiguration configuration;

        public Menu_ItemsController(IConfiguration _configuration)
        {
            this.configuration = _configuration;
        }

        #region Restaurant_DropDownList

        public void Restaurant_DropDownList()
        {
            Menu_Items_Main_DAL order = new Menu_Items_Main_DAL();
            DataTable dt = order.Restaurant_DropDownList();
            List<OFD_RestaurantsDropdownModel> restaurants = new List<OFD_RestaurantsDropdownModel>();
            foreach (DataRow data in dt.Rows)
            {
                OFD_RestaurantsDropdownModel restaurantsmodel = new OFD_RestaurantsDropdownModel();
                restaurantsmodel.RestaurantID = Convert.ToInt32(data["RestaurantID"]);
                restaurantsmodel.Name = data["Name"].ToString();
                restaurants.Add(restaurantsmodel);
            }
            ViewBag.Restaurant_DropDownList = restaurants;
        }
        #endregion

        #region Category_DropDownList

        public void Category_DropDownList()
        {
            Menu_Items_Main_DAL order = new Menu_Items_Main_DAL();
            DataTable dt = order.Category_DropDownList();
            List<CategoryDropdownModel> category = new List<CategoryDropdownModel>();
            foreach (DataRow data in dt.Rows)
            {
                CategoryDropdownModel categorymodel = new CategoryDropdownModel();
                categorymodel.CategoryID = Convert.ToInt32(data["CategoryID"]);
                categorymodel.CategoryName = data["CategoryName"].ToString();
                category.Add(categorymodel);
            }
            ViewBag.Category_DropDownList = category;
        }
        #endregion


        //#region SaveForAddEdit
        //public IActionResult SaveForAddEdit(Menu_ItemsModel model)
        //{
        //    bool ans = false;

        //    // Check if there is an image file attached to the model
        //    if (model.ImageFile != null && model.ImageFile.Length > 0)
        //    {
        //        // If an image file is present, save it to the wwwroot/Menu folder
        //        var path = Path.Combine(Environment.CurrentDirectory, "wwwroot", "Menu", model.MenuItemName + "." + model.ImageFile.ContentType.Split('/')[1]);
        //        using (FileStream stream = new FileStream(path, FileMode.Create))
        //        {
        //            model.ImageFile.CopyTo(stream);
        //        }
        //        // Update the model's MenuImg property with the saved image file name
        //        model.MenuImg = model.MenuItemName + "." + model.ImageFile.ContentType.Split('/')[1];
        //    }
        //    else
        //    {
        //        // If no image file is present, redirect to Menu_ItemsList
        //        return RedirectToAction("Menu_ItemsList");
        //    }

        //    // Create an instance of Menu_Items_Base_DAL
        //    Menu_Items_Base_DAL dal = new Menu_Items_Base_DAL();

        //    // Check if MenuItemID is set to determine if it's an update or insert operation
        //    if (model.MenuItemID != 0)
        //    {
        //        // If MenuItemID is not zero, it's an update operation
        //        ans = dal.Menu_Items_Update(model);
        //        TempData["message"] = "Record Updated Successfully";
        //    }
        //    else
        //    {
        //        // If MenuItemID is zero, it's an insert operation
        //        ans = dal.Menu_Items_Insert(model);
        //        TempData["message"] = "Record Inserted Successfully";
        //    }

        //    // Redirect to Menu_ItemsList based on the result of the update or insert operation
        //    return RedirectToAction("Menu_ItemsList");
        //}

        //#endregion

        #region Menu_ItemsSearchModel

        public IActionResult Menu_ItemSearch(Menu_ItemsSearchModel Menu_ItemsSearch)
        {
            try
            {
                // Ensure the parameter is not null or empty
                string menuItemName = Menu_ItemsSearch.MenuItemName ?? string.Empty;

                // Use the appropriate DbType and ParameterDirection
                Database db = new SqlDatabase(configuration.GetConnectionString("MyConnectionString"));
                DbCommand dbCmd = db.GetStoredProcCommand("PR_MenuItems_Search2");

                db.AddInParameter(dbCmd, "@MenuItemName", DbType.String, menuItemName);

                using (IDataReader dr = db.ExecuteReader(dbCmd))
                {
                    DataTable dt = new DataTable();
                    dt.Load(dr);

                    List<Menu_ItemsModel> modelList = new List<Menu_ItemsModel>();

                    foreach (DataRow row in dt.Rows)
                    {
                        Menu_ItemsModel menuItem = new Menu_ItemsModel();
                        menuItem.MenuItemID = Convert.ToInt32(row["MenuItemID"]);
                        menuItem.MenuImg = row["MenuImg"].ToString();

                        menuItem.RestaurantID = Convert.ToInt32(row["RestaurantID"]);
                        menuItem.Name = row["Name"].ToString();         
                        menuItem.MenuItemName = row["MenuItemName"].ToString();
                        menuItem.Description = row["Description"].ToString();
                        menuItem.Price = Convert.ToDecimal(row["Price"]);
                        menuItem.CategoryID = Convert.ToInt32(row["CategoryID"]);
                        menuItem.CategoryName = row["CategoryName"].ToString();
                        menuItem.Created = Convert.ToDateTime(row["Created"]);
                        menuItem.Modified = Convert.ToDateTime(row["Modified"]);

                        modelList.Add(menuItem);
                    }

                    // Pass the modelList to the "Menu_ItemsList" view
                    return View("Menu_ItemsList", modelList);
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                return View("Menu_ItemsList", new List<Menu_ItemsModel>());
            }
        }
        #endregion



        #region SelectAll

        public IActionResult Menu_ItemsList()
        {

            Menu_Items_Base_DAL dal = new Menu_Items_Base_DAL();
            return View(dal.Menu_ItemsList());

        }


        #endregion
        #region DeleteMenu
        public IActionResult DeleteMenu(int MenuItemID)
        {

            Menu_Items_Base_DAL dal = new Menu_Items_Base_DAL();
            TempData["message"] = dal.DeleteMenu(MenuItemID); ;
            return RedirectToAction("Menu_ItemsList");
        }
        #endregion

        #region AddEditMenu

        public IActionResult Menu_ItemsAddEdit(int MenuItemID)
        {
            Restaurant_DropDownList();
            Category_DropDownList();
            if (MenuItemID == null)
            {
                return View();
            }
            else
            {
                Menu_Items_Base_DAL dal = new Menu_Items_Base_DAL();
                Menu_ItemsModel model = dal.Menu_Items_SelectByPK1(MenuItemID);
                return View(model);
            }

        }
        #endregion

        #region SaveForAddEdit
        public IActionResult SaveForAddEdit(Menu_ItemsModel model)
        {
            bool ans = false;
            Console.WriteLine(model.MenuItemID);
            Menu_Items_Base_DAL dal = new Menu_Items_Base_DAL();
            if (model.MenuItemID != 0)
            {
                ans = dal.Menu_Items_Update(model);
                TempData["message"] = "Record Updated Successfully";
            }
            else
            {
                ans = dal.Menu_Items_Insert(model);
                TempData["message"] = "Record Inserted Successfully";
            }
            if (ans)
            {
                return RedirectToAction("Menu_ItemsList");
            }
            else
            {
                return RedirectToAction("Menu_ItemsList");
            }
        }
        #endregion

        #region Menu_ItemsDetails
        public IActionResult Menu_ItemsDetails(int MenuItemID)
        {
            Menu_Items_Base_DAL bal = new Menu_Items_Base_DAL();
            Menu_ItemsModel model = bal.Menu_Items_SelectByPK1( MenuItemID);
            return View(model);

        }
        #endregion

        #region #Cancel
        public IActionResult Cancel()
        {
            return RedirectToAction("Menu_ItemsList");
        }
        #endregion

    }
}
