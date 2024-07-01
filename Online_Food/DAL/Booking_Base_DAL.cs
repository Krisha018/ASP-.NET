using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Online_Food.Areas.Booking.Models;
using System.Data;
using System.Data.Common;

namespace Online_Food.DAL
{
    public class Booking_Base_DAL:DAL_Helper
    {

        #region OFD_User_DropDownList


        public DataTable OFD_User_DropDownList()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_OFD_User_DropdownList");
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

        #region Menu_DropDownList


        public DataTable Menu_DropDownList()
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


        #region Booking_SelectAll

        public List<BookingModel> BookingList()
        {
            List<BookingModel> list = new List<BookingModel>();
            SqlDatabase db = new SqlDatabase(ConnString);
            DbCommand cmd = db.GetStoredProcCommand("Booking_SelectAll");
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    BookingModel model = new BookingModel();

                    model.BookingID = Convert.ToInt32(reader["BookingID"]);
                    model.UserID = Convert.ToInt32(reader["UserID"]);
                    model.FirstName = reader["FirstName"].ToString();
                    model.Address = reader["Address"].ToString();
                    model.OrderDate = Convert.ToDateTime(reader["OrderDate"]);

                    model.MenuItemID = Convert.ToInt32(reader["MenuItemID"]);
                    model.MenuItemName = Convert.ToString(reader["MenuItemName"]);
                    model.Email = reader["Email"].ToString();
                  
                    model.PhoneNumber = reader["PhoneNumber"].ToString();
                    model.Created = Convert.ToDateTime(reader["Created"]);
                    model.Modified = Convert.ToDateTime(reader["Modified"]);
                    list.Add(model);
                }
            }
            return list;
        }

        #endregion

        #region Booking_Delete
        public string DeleteUsers(int BookingID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("Booking_Delete");
                sqlDB.AddInParameter(dbCMD, "BookingID", SqlDbType.Int, BookingID);
                sqlDB.ExecuteNonQuery(dbCMD);
                return "Record Deleted Successfully";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        #endregion

        #region Booking_SelectByPK
        public BookingModel Booking_SelectByPK(int BookingID)
        {
            SqlDatabase db = new SqlDatabase(ConnString);
            DbCommand cmd = db.GetStoredProcCommand("Booking_SelectByPK");
            db.AddInParameter(cmd, "@BookingID", SqlDbType.Int, BookingID);
            BookingModel model = new BookingModel();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    model.BookingID = Convert.ToInt32(reader["BookingID"]);
                    model.UserID = Convert.ToInt32(reader["UserID"]);
                    model.FirstName = reader["FirstName"].ToString();
                    model.Address = reader["Address"].ToString();
                    model.OrderDate = Convert.ToDateTime(reader["OrderDate"]);

                    model.MenuItemID = Convert.ToInt32(reader["MenuItemID"]);
                    model.MenuItemName = Convert.ToString(reader["MenuItemName"]);
                    model.Email = reader["Email"].ToString();

                    model.PhoneNumber = reader["PhoneNumber"].ToString();
                    model.Created = Convert.ToDateTime(reader["Created"]);
                    model.Modified = Convert.ToDateTime(reader["Modified"]);
                }
            }
            return model;
        }
        #endregion

        #region BookingAdd
        public bool Booking_Insert(BookingModel model)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand cmd = db.GetStoredProcCommand("Booking_Insert");
                db.AddInParameter(cmd, "@UserID", SqlDbType.Int, model.UserID);
                db.AddInParameter(cmd, "@Address", SqlDbType.VarChar, model.Address);
                db.AddInParameter(cmd, "@OrderDate", SqlDbType.DateTime, model.OrderDate);
                db.AddInParameter(cmd, "@MenuItemID", SqlDbType.Int, model.MenuItemID);

               
                db.AddInParameter(cmd, "@Email", SqlDbType.VarChar, model.Email);

                db.AddInParameter(cmd, "@PhoneNumber", SqlDbType.VarChar, model.PhoneNumber);
              

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

        #region Booking_Update
        public bool Booking_Update(BookingModel model)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand cmd = db.GetStoredProcCommand("Booking_Update");
                db.AddInParameter(cmd, "@BookingID", SqlDbType.Int, model.BookingID);

                db.AddInParameter(cmd, "@UserID", SqlDbType.Int, model.UserID);
                db.AddInParameter(cmd, "@Address", SqlDbType.VarChar, model.Address);
                db.AddInParameter(cmd, "@OrderDate", SqlDbType.DateTime, model.OrderDate);
                db.AddInParameter(cmd, "@MenuItemID", SqlDbType.Int, model.MenuItemID);


                db.AddInParameter(cmd, "@Email", SqlDbType.VarChar, model.Email);

                db.AddInParameter(cmd, "@PhoneNumber", SqlDbType.VarChar, model.PhoneNumber);
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
