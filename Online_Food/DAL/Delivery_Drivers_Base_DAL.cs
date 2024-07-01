using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Online_Food.Areas.Delivery_Drivers.Models;
using System.Data.Common;
using System.Data;

namespace Online_Food.DAL
{
    public class Delivery_Drivers_Base_DAL:DAL_Helper
    {
        #region OFD_Users_SelectAll

        public List<Delivery_DriversModel> Delivery_DriversList()
        {
            List<Delivery_DriversModel> list = new List<Delivery_DriversModel>();
            SqlDatabase db = new SqlDatabase(ConnString);
            DbCommand cmd = db.GetStoredProcCommand("Delivery_Drivers_SelectAll");
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    Delivery_DriversModel model = new Delivery_DriversModel();

                    model.DriverID = Convert.ToInt32(reader["DriverID"]);
                    model.FirstName = reader["FirstName"].ToString();
                    model.LastName = reader["LastName"].ToString();
                    model.PhoneNumber = reader["PhoneNumber"].ToString();
                    model.DriverLicenceNo = reader["DriverLicenceNo"].ToString();
                    model.DriverLicencePhoto = reader["DriverLicencePhoto"].ToString();
                    model.Created = Convert.ToDateTime(reader["Created"]);
                    model.Modified = Convert.ToDateTime(reader["Modified"]);
                    list.Add(model);
                }
            }
            return list;
        }

        #endregion

        #region Delivery_Drivers_Delete
        public string DeleteDrivers(int DriverID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("Delivery_Drivers_Delete");
                sqlDB.AddInParameter(dbCMD, "DriverID", SqlDbType.Int, DriverID);
                sqlDB.ExecuteNonQuery(dbCMD);
                return "Record Deleted Successfully";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        #endregion

        #region Delivery_Drivers_SelectByPK
        public Delivery_DriversModel Delivery_Drivers_SelectByPK(int DriverID)
        {
            SqlDatabase db = new SqlDatabase(ConnString);
            DbCommand cmd = db.GetStoredProcCommand("Delivery_Drivers_SelectByPK");
            db.AddInParameter(cmd, "@DriverID", SqlDbType.Int, DriverID);
            Delivery_DriversModel model = new Delivery_DriversModel();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    model.DriverID = Convert.ToInt32(reader["DriverID"]);
                    model.FirstName = reader["FirstName"].ToString();
                    model.LastName = reader["LastName"].ToString();
                    model.PhoneNumber = reader["PhoneNumber"].ToString();
                    model.DriverLicenceNo = reader["DriverLicenceNo"].ToString();
                    model.DriverLicencePhoto = reader["DriverLicencePhoto"].ToString();
                    model.Created = Convert.ToDateTime(reader["Created"]);
                    model.Modified = Convert.ToDateTime(reader["Modified"]);
                }
            }
            return model;
        }
        #endregion

        #region Delivery_Drivers_Insert
        public bool Delivery_Drivers_Insert(Delivery_DriversModel model)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand cmd = db.GetStoredProcCommand("Delivery_Drivers_Insert");
              

                db.AddInParameter(cmd, "@FirstName", SqlDbType.VarChar, model.FirstName);
                db.AddInParameter(cmd, "@LastName", SqlDbType.VarChar, model.LastName);
                db.AddInParameter(cmd, "@PhoneNumber", SqlDbType.VarChar, model.PhoneNumber);
                db.AddInParameter(cmd, "@DriverLicenceNo", SqlDbType.VarChar, model.DriverLicenceNo);
                db.AddInParameter(cmd, "@DriverLicencePhoto", SqlDbType.VarChar, model.DriverLicencePhoto);

              
               

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

        #region Delivery_Drivers_Update
        public bool Delivery_Drivers_Update(Delivery_DriversModel model)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand cmd = db.GetStoredProcCommand("Delivery_Drivers_Update");
                db.AddInParameter(cmd, "@DriverID", SqlDbType.Int, model.DriverID);
                db.AddInParameter(cmd, "@FirstName", SqlDbType.VarChar, model.FirstName);
                db.AddInParameter(cmd, "@LastName", SqlDbType.VarChar, model.LastName);
                db.AddInParameter(cmd, "@PhoneNumber", SqlDbType.VarChar, model.PhoneNumber);
                db.AddInParameter(cmd, "@DriverLicenceNo", SqlDbType.VarChar, model.DriverLicenceNo);
                db.AddInParameter(cmd, "@DriverLicencePhoto", SqlDbType.VarChar, model.DriverLicencePhoto);
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
