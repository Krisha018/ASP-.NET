using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Online_Food.Areas.Cart.Models;
using System.Data;
using System.Data.Common;

namespace Online_Food.DAL
{
    public class Cart_DAL:DAL_Helper
    {
        #region SelectAllCartItems

        public List<CartModel> CartList()
        {
            List<CartModel> list = new List<CartModel>();
            SqlDatabase db = new SqlDatabase(ConnString);
            DbCommand cmd = db.GetStoredProcCommand("SelectAllCartItems");
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    CartModel model = new CartModel();

                    model.Qty = Convert.ToInt32(reader["Qty"]);
                    model.UserID = Convert.ToInt32(reader["UserID"]);
                    model.CartID = Convert.ToInt32(reader["CartID"]);
                    model.MenuItemID = Convert.ToInt32(reader["MenuItemID"]);

                    list.Add(model);
                }
            }
            return list;
        }

        #endregion

        #region DeleteCartItem
        public string DeleteCart(int CartID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("DeleteCartItem");
                sqlDB.AddInParameter(dbCMD, "CartID", SqlDbType.Int, CartID);
                sqlDB.ExecuteNonQuery(dbCMD);
                return "Record Deleted Successfully";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        #endregion

        #region SelectCartItemByID
        public CartModel SelectCartItemByID(int CartID)
        {
            SqlDatabase db = new SqlDatabase(ConnString);
            DbCommand cmd = db.GetStoredProcCommand("SelectCartItemByID");
            db.AddInParameter(cmd, "@CartID", SqlDbType.Int, CartID);
            CartModel model = new CartModel();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    model.Qty = Convert.ToInt32(reader["Qty"]);
                    model.UserID = Convert.ToInt32(reader["UserID"]);
                 
                    model.MenuItemID = Convert.ToInt32(reader["MenuItemID"]);

                }
            }
            return model;
        }
        #endregion

        #region InsertCartItem
        public bool InsertCartItem(CartModel model)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand cmd = db.GetStoredProcCommand("InsertCartItem");

                db.AddInParameter(cmd, "@Qty", SqlDbType.Int, model.Qty);
                db.AddInParameter(cmd, "@UserID", SqlDbType.Int, model.UserID);
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

        #region UpdateCartItem
        public bool UpdateCartItem(CartModel model)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand cmd = db.GetStoredProcCommand("UpdateCartItem");
                db.AddInParameter(cmd, "@Qty", SqlDbType.Int, model.Qty);
                db.AddInParameter(cmd, "@UserID", SqlDbType.Int, model.UserID);
                db.AddInParameter(cmd, "@CartID", SqlDbType.Int, model.CartID);

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
