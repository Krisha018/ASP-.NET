using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Online_Food.Areas.Category.Models;
using Online_Food.Areas.Menu.Models;
using Online_Food.Areas.Menu_Items.Models;
using Online_Food.Models;
using System.Data;
using System.Data.Common;

namespace Online_Food.DAL
{
    public class UserMenu_Base_DAL:DAL_Helper
    {
        #region Menu_Items_SelectAll

        public List<MenuModel> Menu_ItemsList()
        {
            List<MenuModel> list = new List<MenuModel>();

            // Assuming SqlDatabase and DbCommand are from Microsoft.Practices.EnterpriseLibrary.Data
            SqlDatabase db = new SqlDatabase(ConnString);
            DbCommand cmd = db.GetStoredProcCommand("Menu_Items_SelectAll");

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    MenuModel model = new MenuModel();

                    model.MenuItemID = Convert.ToInt32(reader["MenuItemID"]);
                    model.MenuImg = reader["MenuImg"].ToString();
                    model.RestaurantID = Convert.ToInt32(reader["RestaurantID"]);
                    model.Name = reader["Name"].ToString();
                    model.MenuItemName = reader["MenuItemName"].ToString();
                    model.Description = reader["Description"].ToString();
                    model.Price = Convert.ToDecimal(reader["Price"]);
                    model.CategoryID = Convert.ToInt32(reader["CategoryID"]);
                    model.CategoryName = reader["CategoryName"].ToString();
                    model.Created = Convert.ToDateTime(reader["Created"]);
                    model.Modified = Convert.ToDateTime(reader["Modified"]);

                    list.Add(model);
                }
            }

            return list;
        }

        #endregion

        //#region Category_SelectAll

        //public List<CategoryModel> CategoryList()
        //{
        //    List<CategoryModel> list = new List<CategoryModel>();
        //    SqlDatabase db = new SqlDatabase(ConnString);
        //    DbCommand cmd = db.GetStoredProcCommand("Category_SelectAll");
        //    using (IDataReader reader = db.ExecuteReader(cmd))
        //    {
        //        while (reader.Read())
        //        {
        //            CategoryModel model = new CategoryModel();


        //            model.CategoryID = Convert.ToInt32(reader["CategoryID"]);
        //            model.CategoryName = reader["CategoryName"].ToString();
        //            model.CategoryType = reader["CategoryType"].ToString();
        //            model.Created = Convert.ToDateTime(reader["Created"]);
        //            model.Modified = Convert.ToDateTime(reader["Modified"]);
        //            list.Add(model);
        //        }
        //    }
        //    return list;
        //}
        //#endregion

    }
}
