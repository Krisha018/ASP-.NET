using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Online_Food.Areas.Cart.Models;
using System.Data;
using System.Data.Common;

namespace Online_Food.DAL
{
    public class User_cart_DAL:DAL_Helper
    {
        #region SelectAllCartItems

        public DataTable SelectAllCartItems()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("SelectAllCartItems");


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

        #region DeleteCartItem

        public bool? DeleteCartItem(int? CartID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("DeleteCartItem");
                sqlDB.AddInParameter(dbCMD, "CartID", SqlDbType.Int, CartID);

                int vReturnValue = sqlDB.ExecuteNonQuery(dbCMD);
                return (vReturnValue == -1 ? false : true);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        public void Save(CartModel cartModel)
        {
            DbCommand objCmd;
            SqlDatabase sqlDB = new SqlDatabase(ConnString);
            if (cartModel.CartID == null)
            {
                objCmd = sqlDB.GetStoredProcCommand("InsertCartItem");

            }
            else
            {
                objCmd = sqlDB.GetStoredProcCommand("UpdateCartItem");
                sqlDB.AddInParameter(objCmd, "@CartID", SqlDbType.Int, cartModel.CartID);
            }
            sqlDB.AddInParameter(objCmd, "Qty", SqlDbType.VarChar, cartModel.Qty);
            sqlDB.AddInParameter(objCmd, "UserID", SqlDbType.Int, cartModel.UserID);
            sqlDB.AddInParameter(objCmd, "MenuItemID", SqlDbType.Int, cartModel.MenuItemID);



            sqlDB.ExecuteNonQuery(objCmd);

        }
    }
}
