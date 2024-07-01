using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Online_Food.Areas.OFD_Restaurants.Models;
using System.Data.Common;
using System.Data;



namespace Online_Food.DAL
{
    public class OFD_Restaurants_Base_DAL : DAL_Helper
    {
        #region OFD_Restaurants_SelectAll

        public List<OFD_RestaurantsModel> OFD_RestaurantsList()
        {
            List<OFD_RestaurantsModel> list = new List<OFD_RestaurantsModel>();
            SqlDatabase db = new SqlDatabase(ConnString);
            DbCommand cmd = db.GetStoredProcCommand("OFD_Restaurants_SelectAll");
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    OFD_RestaurantsModel model = new OFD_RestaurantsModel();

                    model.RestaurantID = Convert.ToInt32(reader["RestaurantID"]);
                    model.Name = reader["Name"].ToString();
                    model.Description = reader["Description"].ToString();
                    model.Address = reader["Address"].ToString();
                    model.PhoneNumber = reader["PhoneNumber"].ToString();
                    model.Rating = Convert.ToInt32(reader["Rating"]);
                    model.Created = Convert.ToDateTime(reader["Created"]);
                    model.Modified = Convert.ToDateTime(reader["Modified"]);
                    list.Add(model);
                }
            }
            return list;
        }
        #endregion




        #region OFD_Restaurants_Delete
        public string DeleteUsers(int RestaurantID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("OFD_Restaurants_Delete");
                sqlDB.AddInParameter(dbCMD, "RestaurantID", SqlDbType.Int, RestaurantID);
                sqlDB.ExecuteNonQuery(dbCMD);
                return "Record Deleted Successfully";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        #endregion

        # region OFD_Restaurants_SelectByPK
        public OFD_RestaurantsModel OFD_Restaurants_SelectByPK(int RestaurantID)
        {
            SqlDatabase db = new SqlDatabase(ConnString);
            DbCommand cmd = db.GetStoredProcCommand("OFD_Restaurants_SelectByPK");
            db.AddInParameter(cmd, "@RestaurantID", SqlDbType.Int, RestaurantID);
            OFD_RestaurantsModel model = new OFD_RestaurantsModel();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    model.RestaurantID = Convert.ToInt32(reader["RestaurantID"]);
                    model.Name = reader["Name"].ToString();
                    model.Description = reader["Description"].ToString();
                    model.Address = reader["Address"].ToString();
                    model.PhoneNumber = reader["PhoneNumber"].ToString();
                    model.Rating = Convert.ToInt32(reader["Rating"]);
                    model.Created = Convert.ToDateTime(reader["Created"]);
                    model.Modified = Convert.ToDateTime(reader["Modified"]);
                }
            }
            return model;
        }
        #endregion

        #region OFD_RestaurantsAdd
        public bool OFD_Restaurants_Insert(OFD_RestaurantsModel model)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand cmd = db.GetStoredProcCommand("OFD_Restaurants_Insert");
              
                db.AddInParameter(cmd, "@Name", SqlDbType.VarChar, model.Name);
                db.AddInParameter(cmd, "@Description", SqlDbType.VarChar, model.Description);
                db.AddInParameter(cmd, "@Address", SqlDbType.VarChar, model.Address);
                  db.AddInParameter(cmd, "@PhoneNumber", SqlDbType.VarChar, model.PhoneNumber);
                db.AddInParameter(cmd, "@Rating", SqlDbType.Int, model.Rating);

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

        #region OFD_Restaurants_Update
        public bool OFD_Restaurants_Update(OFD_RestaurantsModel model)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand cmd = db.GetStoredProcCommand("OFD_Restaurants_Update");
                db.AddInParameter(cmd, "@RestaurantID", SqlDbType.Int, model.RestaurantID);
                db.AddInParameter(cmd, "@Name", SqlDbType.VarChar, model.Name);
                db.AddInParameter(cmd, "@Description", SqlDbType.VarChar, model.Description);
                db.AddInParameter(cmd, "@Address", SqlDbType.VarChar, model.Address);
                db.AddInParameter(cmd, "@PhoneNumber", SqlDbType.VarChar, model.PhoneNumber);
                db.AddInParameter(cmd, "@Rating", SqlDbType.Int, model.Rating);
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
