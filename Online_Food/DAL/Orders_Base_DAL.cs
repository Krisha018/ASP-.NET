using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Online_Food.Areas.Orders.Models;
using System.Data.Common;
using System.Data;



namespace Online_Food.DAL
{
    public class Orders_Base_DAL:DAL_Helper
    {

       

        #region OrderItems_SelectAll

        public List<OrdersModel> OrdersList()
        {
            List<OrdersModel> list = new List<OrdersModel>();
            SqlDatabase db = new SqlDatabase(ConnString);
            DbCommand cmd = db.GetStoredProcCommand("OrderItems_SelectAll");
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    OrdersModel model = new OrdersModel();

                   
                    model.OrderItemID = Convert.ToInt32(reader["OrderItemID"]);
                    model.TotalAmount = Convert.ToString(reader["TotalAmount"]);
                    model.OrderID = Convert.ToInt32(reader["OrderID"]);
                    model.Subtotal = Convert.ToDecimal(reader["Subtotal"]);
                    model.Qty = Convert.ToInt32(reader["Qty"]);
                    model.MenuItemID = Convert.ToInt32(reader["MenuItemID"]);

                    model.MenuItemName = Convert.ToString(reader["MenuItemName"]);
                    model.Created = Convert.ToDateTime(reader["Created"]);
                    model.Modified = Convert.ToDateTime(reader["Modified"]);
                    list.Add(model);
                }
            }
            return list;
        }
        #endregion




        #region OrderItems_Delete
        public string DeleteOrder(int OrderItemID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("OrderItems_Delete");
                sqlDB.AddInParameter(dbCMD, "OrderItemID", SqlDbType.Int, OrderItemID);
                sqlDB.ExecuteNonQuery(dbCMD);
                return "Record Deleted Successfully";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        #endregion

        #region OrderItems_SelectByPK
        public OrdersModel OrderItems_SelectByPK(int OrderItemID)
        {
            SqlDatabase db = new SqlDatabase(ConnString);
            DbCommand cmd = db.GetStoredProcCommand("OrderItems_SelectByPK");
            db.AddInParameter(cmd, "@OrderItemID", SqlDbType.Int, OrderItemID);
            OrdersModel model = new OrdersModel();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    model.OrderItemID = Convert.ToInt32(reader["OrderItemID"]);
                    //model.TotalAmount = Convert.ToString(reader["TotalAmount"]);
                    model.OrderID = Convert.ToInt32(reader["OrderID"]);
                    model.Subtotal = Convert.ToDecimal(reader["Subtotal"]);
                    model.Qty = Convert.ToInt32(reader["Qty"]);
                    model.MenuItemID = Convert.ToInt32(reader["MenuItemID"]);
                    model.MenuItemName = Convert.ToString(reader["MenuItemName"]);

                    model.Created = Convert.ToDateTime(reader["Created"]);
                    model.Modified = Convert.ToDateTime(reader["Modified"]);

                }
            }
            return model;
        }
        #endregion


        #region OrdersAdd
        public bool OrderItems_Insert(OrdersModel model)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand cmd = db.GetStoredProcCommand("OrderItems_Insert");
                db.AddInParameter(cmd, "@OrderID", SqlDbType.Int, model.OrderID);
                //db.AddInParameter(cmd, "@TotalAmount", SqlDbType.VarChar, model.TotalAmount);
                db.AddInParameter(cmd, "@Subtotal", SqlDbType.Decimal, model.Subtotal);
                db.AddInParameter(cmd, "@Qty", SqlDbType.Int, model.Qty);
                db.AddInParameter(cmd, "@MenuItemID", SqlDbType.Int, model.MenuItemID);

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
        #region OrderItems_Update
        public bool OrderItems_Update(OrdersModel model)
        {

            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand cmd = db.GetStoredProcCommand("OrderItems_Update");
                db.AddInParameter(cmd, "@OrderItemID", SqlDbType.Int, model.OrderItemID);
                db.AddInParameter(cmd, "@OrderID", SqlDbType.Int, model.OrderID);
                //db.AddInParameter(cmd, "@TotalAmount", SqlDbType.VarChar, model.TotalAmount);

                db.AddInParameter(cmd, "@Subtotal", SqlDbType.Decimal, model.Subtotal);
                db.AddInParameter(cmd, "@Qty", SqlDbType.Int, model.Qty);
                db.AddInParameter(cmd, "@MenuItemID", SqlDbType.Int, model.MenuItemID);
                
               
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
