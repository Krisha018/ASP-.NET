using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace Online_Food.DAL
{
    public class User_About_Base_DAL:DAL_Helper
    {
        #region OFD_UsersCount
        public int OFD_UsersCount()
        {

            int rowCount = 0;
            SqlDatabase db = new SqlDatabase(ConnString);
            DbCommand cmd = db.GetStoredProcCommand("OFD_Users_SelectAll");

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    rowCount++;
                }
            }

            // Use the rowCount variable as needed
            return rowCount;
        }
        #endregion

        #region OFD_RestaurantsCount
        public int OFD_RestaurantsCount()
        {

            int rowCount = 0;
            SqlDatabase db = new SqlDatabase(ConnString);
            DbCommand cmd = db.GetStoredProcCommand("OFD_Restaurants_SelectAll");

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    rowCount++;
                }
            }

            // Use the rowCount variable as needed
            return rowCount;
        }
        #endregion

        #region Menu_ItemsCount
        public int Menu_ItemsCount()
        {

            int rowCount = 0;
            SqlDatabase db = new SqlDatabase(ConnString);
            DbCommand cmd = db.GetStoredProcCommand("Menu_Items_SelectAll");

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    rowCount++;
                }
            }

            // Use the rowCount variable as needed
            return rowCount;
        }
        #endregion
    }
}
