using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Online_Food.Areas.Category.Models;
using System.Data.Common;
using System.Data;


namespace Online_Food.DAL
{
    public class Category_Base_DAL:DAL_Helper
    {
        #region Category_SelectAll

        public List<CategoryModel> CategoryList()
        {
            List<CategoryModel> list = new List<CategoryModel>();
            SqlDatabase db = new SqlDatabase(ConnString);
            DbCommand cmd = db.GetStoredProcCommand("Category_SelectAll");
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    CategoryModel model = new CategoryModel();


                    model.CategoryID = Convert.ToInt32(reader["CategoryID"]);
                     model.CategoryName = reader["CategoryName"].ToString();
                     model.CategoryType = reader["CategoryType"].ToString();
                    model.Created = Convert.ToDateTime(reader["Created"]);
                    model.Modified = Convert.ToDateTime(reader["Modified"]);
                    list.Add(model);
                }
            }
            return list;
        }
        #endregion




        #region Category_Delete
        public string DeleteCategory(int CategoryID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("Category_Delete");
                sqlDB.AddInParameter(dbCMD, "CategoryID", SqlDbType.Int, CategoryID);
                sqlDB.ExecuteNonQuery(dbCMD);
                return "Record Deleted Successfully";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        #endregion

        #region Category_SelectByPK
        public CategoryModel Category_SelectByPK(int CategoryID)
        {
            SqlDatabase db = new SqlDatabase(ConnString);
            DbCommand cmd = db.GetStoredProcCommand("Category_SelectByPK");
            db.AddInParameter(cmd, "@CategoryID", SqlDbType.Int, CategoryID);
            CategoryModel model = new CategoryModel();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    model.CategoryID = Convert.ToInt32(reader["CategoryID"]);
                    model.CategoryName = reader["CategoryName"].ToString();
                    model.CategoryType = reader["CategoryType"].ToString();
                    model.Created = Convert.ToDateTime(reader["Created"]);
                    model.Modified = Convert.ToDateTime(reader["Modified"]);

                }
            }
            return model;
        }
        #endregion


        #region CategoryAdd
        public bool Category_Insert(CategoryModel model)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand cmd = db.GetStoredProcCommand("Category_Insert");
              
                db.AddInParameter(cmd, "@CategoryName", SqlDbType.VarChar, model.CategoryName);
                db.AddInParameter(cmd, "@CategoryType", SqlDbType.VarChar, model.CategoryType);


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
        #region Category_Update
        public bool Category_Update(CategoryModel model)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand cmd = db.GetStoredProcCommand("Category_Update");
                db.AddInParameter(cmd, "@CategoryID", SqlDbType.Int, model.CategoryID);
                db.AddInParameter(cmd, "@CategoryName", SqlDbType.VarChar, model.CategoryName);
                db.AddInParameter(cmd, "@CategoryType", SqlDbType.VarChar, model.CategoryType);

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
