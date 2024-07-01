using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

using System.Data.Common;
using System.Data;
using Online_Food.Areas.Menu_Items.Models;


namespace Online_Food.DAL
{
    public class Menu_Items_Base_DAL:DAL_Helper
    {

       



        #region Set_Menu_Items_DropDownList
        public DataTable Set_Menu_Items_DropDownList()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_Menu_Items_DropdownList");
                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
        #region Menu_Items_SelectAll

        public List<Menu_ItemsModel> Menu_ItemsList()
        {
            List<Menu_ItemsModel> list = new List<Menu_ItemsModel>();
            SqlDatabase db = new SqlDatabase(ConnString);
            DbCommand cmd = db.GetStoredProcCommand("Menu_Items_SelectAll");
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    Menu_ItemsModel model = new Menu_ItemsModel();

                    model.MenuItemID = Convert.ToInt32(reader["MenuItemID"]);
                    model.MenuImg = reader["MenuImg"].ToString();


                    model.RestaurantID = Convert.ToInt32(reader["RestaurantID"]);
                    model.Name = Convert.ToString(reader["Name"]);

                    model.MenuItemName = reader["MenuItemName"].ToString();
                    model.Description = reader["Description"].ToString();
                     model.Price = Convert.ToDecimal(reader["Price"]);
                    model.CategoryID = Convert.ToInt32(reader["CategoryID"]);
                    model.CategoryName = Convert.ToString(reader["CategoryName"]);

                    model.Created = Convert.ToDateTime(reader["Created"]);
                    model.Modified = Convert.ToDateTime(reader["Modified"]);
                    list.Add(model);
                }
            }
            return list;
        }
        #endregion




        #region Menu_Items_Delete
        public string DeleteMenu(int MenuItemID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("Menu_Items_Delete");
                sqlDB.AddInParameter(dbCMD, "MenuItemID", SqlDbType.Int, MenuItemID);
                sqlDB.ExecuteNonQuery(dbCMD);
                return "Record Deleted Successfully";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        #endregion

        #region MenuItems_SelectByPK
        public Menu_ItemsModel Menu_Items_SelectByPK1(int MenuItemID)
        {
            SqlDatabase db = new SqlDatabase(ConnString);
            DbCommand cmd = db.GetStoredProcCommand("MenuItems_SelectByPK");
            db.AddInParameter(cmd, "@MenuItemID", SqlDbType.Int, MenuItemID);
            Menu_ItemsModel model = new Menu_ItemsModel();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    model.MenuItemID = Convert.ToInt32(reader["MenuItemID"]);
                    model.MenuImg = Convert.ToString(reader["MenuImg"]);

                    model.RestaurantID = Convert.ToInt32(reader["RestaurantID"]);
                    model.Name = Convert.ToString(reader["Name"]);
                    model.MenuItemName = reader["MenuItemName"].ToString();
                    model.Description = reader["Description"].ToString();
                    model.Price = Convert.ToDecimal(reader["Price"]);
                    model.CategoryID = Convert.ToInt32(reader["CategoryID"]);

                    model.CategoryName = Convert.ToString(reader["CategoryName"]);
                    model.Created = Convert.ToDateTime(reader["Created"]);
                    model.Modified = Convert.ToDateTime(reader["Modified"]);
                 
                }
            }
            return model;
        }
        #endregion

        #region Menu_ItemsAdd
        public bool Menu_Items_Insert(Menu_ItemsModel model)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand cmd = db.GetStoredProcCommand("MenuItems_Insert4");
                db.AddInParameter(cmd, "@MenuImg", SqlDbType.VarChar, model.MenuImg);
                db.AddInParameter(cmd, "@RestaurantID", SqlDbType.Int, model.RestaurantID);
                //db.AddInParameter(cmd, "@Name", SqlDbType.VarChar, model.Name);

                db.AddInParameter(cmd, "@MenuItemName", SqlDbType.VarChar, model.MenuItemName);
                db.AddInParameter(cmd, "@Description", SqlDbType.VarChar, model.Description);
                db.AddInParameter(cmd, "@Price", SqlDbType.Decimal, model.Price);

                db.AddInParameter(cmd, "@CategoryID", SqlDbType.Int, model.CategoryID);
                //db.addinparameter(cmd, "@categoryname", sqldbtype.varchar, model.categoryname);


                int noOfRows = db.ExecuteNonQuery(cmd);
                if (noOfRows > 0) { return true; }
                else { return false; }

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region Menu_Items_Update
        public bool Menu_Items_Update(Menu_ItemsModel model)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand cmd = db.GetStoredProcCommand("PR_Menu_Update1");
                db.AddInParameter(cmd, "@MenuItemID", SqlDbType.Int, model.MenuItemID);
                db.AddInParameter(cmd, "@MenuImg", SqlDbType.VarChar, model.MenuImg);

                db.AddInParameter(cmd, "@RestaurantID", SqlDbType.Int, model.RestaurantID);
                //db.AddInParameter(cmd, "@Name", SqlDbType.VarChar, model.Name);

                db.AddInParameter(cmd, "@MenuItemName", SqlDbType.VarChar, model.MenuItemName);
                db.AddInParameter(cmd, "@Description", SqlDbType.VarChar, model.Description);
                db.AddInParameter(cmd, "@Price", SqlDbType.Decimal, model.Price);
                db.AddInParameter(cmd, "@CategoryID", SqlDbType.Int, model.CategoryID);
                //db.AddInParameter(cmd, "@CategoryName", SqlDbType.VarChar, model.CategoryName);

                int noOfRows = db.ExecuteNonQuery(cmd);
                if (noOfRows > 0) { return true; }
                else { return false; }

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

    }
}
