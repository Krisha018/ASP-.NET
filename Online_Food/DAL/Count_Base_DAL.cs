using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;

namespace Online_Food.DAL
{
    public class Count_Base_DAL:DAL_Helper
    {
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


        public int FoodOrderCount()
        {

            int rowCount = 0;
            SqlDatabase db = new SqlDatabase(ConnString);
            DbCommand cmd = db.GetStoredProcCommand("FoodOrder_SelectAll");

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

        public int OrdersCount()
        {

            int rowCount = 0;
            SqlDatabase db = new SqlDatabase(ConnString);
            DbCommand cmd = db.GetStoredProcCommand("OrderItems_SelectAll");

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

        public int PaymentsCount()
        {

            int rowCount = 0;
            SqlDatabase db = new SqlDatabase(ConnString);
            DbCommand cmd = db.GetStoredProcCommand("Payments_SelectAll");

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

        public int CategoryCount()
        {

            int rowCount = 0;
            SqlDatabase db = new SqlDatabase(ConnString);
            DbCommand cmd = db.GetStoredProcCommand("Category_SelectAll");

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

        public int Delivery_DriversCount()
        {

            int rowCount = 0;
            SqlDatabase db = new SqlDatabase(ConnString);
            DbCommand cmd = db.GetStoredProcCommand("Delivery_Drivers_SelectAll");

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

    }
}
