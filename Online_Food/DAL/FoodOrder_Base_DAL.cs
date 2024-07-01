using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Online_Food.Areas.FoodOrder.Models;
using System.Data.Common;
using System.Data;


namespace Online_Food.DAL
{
    public class FoodOrder_Base_DAL:DAL_Helper
    {
        #region FoodOrder_SelectAll

        public List<FoodOrderModel> FoodOrderList()
        {
            List<FoodOrderModel> list = new List<FoodOrderModel>();
            SqlDatabase db = new SqlDatabase(ConnString);
            DbCommand cmd = db.GetStoredProcCommand("FoodOrder_SelectAll");
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    FoodOrderModel model = new FoodOrderModel();

                    model.OrderID = Convert.ToInt32(reader["OrderID"]);
                    model.UserID = Convert.ToInt32(reader["UserID"]);
                    model.FoodImg = Convert.ToString(reader["FoodImg"]);
                    model.FirstName = Convert.ToString(reader["FirstName"]);

                    model.OrderDate = Convert.ToDateTime(reader["OrderDate"]);
                    model.TotalAmount = Convert.ToString(reader["TotalAmount"]);
                    model.DeliveryAddress = reader["DeliveryAddress"].ToString();
                   model.Created = Convert.ToDateTime(reader["Created"]);
                    model.Modified = Convert.ToDateTime(reader["Modified"]);
                    list.Add(model);
                }
            }
            return list;
        }
        #endregion




        #region FoodOrder_Delete
        public string DeleteFood(int OrderID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("FoodOrder_Delete");
                sqlDB.AddInParameter(dbCMD, "OrderID", SqlDbType.Int, OrderID);
                sqlDB.ExecuteNonQuery(dbCMD);
                return "Record Deleted Successfully";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        #endregion

        #region FoodOrder_SelectByPK
        public FoodOrderModel FoodOrder_SelectByPK(int OrderID)
        {
            SqlDatabase db = new SqlDatabase(ConnString);
            DbCommand cmd = db.GetStoredProcCommand("FoodOrder_SelectByPK1");
            db.AddInParameter(cmd, "@OrderID", SqlDbType.Int, OrderID);
            FoodOrderModel model = new FoodOrderModel();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    model.OrderID = Convert.ToInt32(reader["OrderID"]);
                    model.UserID = Convert.ToInt32(reader["UserID"]);
                    model.FoodImg = Convert.ToString(reader["FoodImg"]);
                    model.FirstName = Convert.ToString(reader["FirstName"]);

                    model.OrderDate = Convert.ToDateTime(reader["OrderDate"]);
                    model.TotalAmount = Convert.ToString(reader["TotalAmount"]);
                    model.DeliveryAddress = reader["DeliveryAddress"].ToString();
                    model.Created = Convert.ToDateTime(reader["Created"]);
                    model.Modified = Convert.ToDateTime(reader["Modified"]);

                }
            }
            return model;
        }
        #endregion

        #region FoodOrder_Insert
        public bool FoodOrder_Insert(FoodOrderModel model)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand cmd = db.GetStoredProcCommand("FoodOrder_Insert1");
                db.AddInParameter(cmd, "@UserID", SqlDbType.Int, model.UserID);
                db.AddInParameter(cmd, "@FoodImg", SqlDbType.VarChar, model.FoodImg);
                db.AddInParameter(cmd, "@OrderDate", SqlDbType.VarChar, model.OrderDate);
                db.AddInParameter(cmd, "@TotalAmount", SqlDbType.VarChar, model.TotalAmount);
                db.AddInParameter(cmd, "@DeliveryAddress", SqlDbType.VarChar, model.DeliveryAddress);
             


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

        #region FoodOrder_Update
        public bool FoodOrder_Update(FoodOrderModel model)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand cmd = db.GetStoredProcCommand("FoodOrder_Update1");
                db.AddInParameter(cmd, "@OrderID", SqlDbType.Int, model.OrderID);
                db.AddInParameter(cmd, "@UserID", SqlDbType.Int, model.UserID);
                db.AddInParameter(cmd, "@FoodImg", SqlDbType.VarChar, model.FoodImg);
                db.AddInParameter(cmd, "@OrderDate", SqlDbType.VarChar, model.OrderDate);
                db.AddInParameter(cmd, "@TotalAmount", SqlDbType.VarChar, model.TotalAmount);
                db.AddInParameter(cmd, "@DeliveryAddress", SqlDbType.VarChar, model.DeliveryAddress);

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
