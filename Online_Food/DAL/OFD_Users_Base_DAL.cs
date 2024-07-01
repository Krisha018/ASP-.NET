using DocumentFormat.OpenXml.EMMA;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Online_Food.Areas.OFD_Restaurants.Models;
using Online_Food.Areas.OFD_Users.Models;
using System.Data;
using System.Data.Common;

namespace Online_Food.DAL
{
    public class OFD_Users_Base_DAL : DAL_Helper
    {
          public List<OFD_UsersModel>OFD_UsersList()
              {
                  try
                  {
    SqlDatabase sqlDatabase = new SqlDatabase(ConnString);
    DbCommand cmd = sqlDatabase.GetStoredProcCommand("OFD_Users_SelectAll");
    List<OFD_UsersModel>oFD_UsersModels = new List<OFD_UsersModel>();
    using (IDataReader dr = sqlDatabase.ExecuteReader(cmd))
    {
            while (dr.Read())
            {
            OFD_UsersModel oFD_UsersModel = new OFD_UsersModel();
            oFD_UsersModel.UserID = Convert.ToInt32(dr["UserID"].ToString);
            oFD_UsersModel.FirstName = dr["FirstName"].ToString();
            oFD_UsersModel.LastName = dr["LastName"].ToString();
            oFD_UsersModel.Email = dr["Email"].ToString();
            oFD_UsersModel.Password = dr["Password"].ToString();
            oFD_UsersModel.PhoneNumber = dr["PhoneNumber"].ToString();
            oFD_UsersModel.Address = dr["Address"].ToString();
                        oFD_UsersModel.Created = Convert.ToDateTime(dr["Created"]);
                        oFD_UsersModel.Modified = Convert.ToDateTime(dr["Modified"]);
                        oFD_UsersModels.Add(oFD_UsersModel);
            }
    }
            return oFD_UsersModels;

  }
            catch(Exception e)
            {
            return null;

            }

        }


        #region OFD_Users_Delete
        public string DeleteUsers(int UserID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("OFD_Users_Delete");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, UserID);
                sqlDB.ExecuteNonQuery(dbCMD);
                return "Record Deleted Successfully";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        #endregion

        # region OFD_Users_SelectByPK
        public OFD_UsersModel OFD_Users_SelectByPK(int UserID)
        {
            SqlDatabase db = new SqlDatabase(ConnString);
            DbCommand cmd = db.GetStoredProcCommand("OFD_Users_SelectByPK");
            db.AddInParameter(cmd, "@UserID", SqlDbType.Int, UserID);
            OFD_UsersModel model = new OFD_UsersModel();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    model.UserID = Convert.ToInt32(reader["UserID"]);
                    model.FirstName = reader["FirstName"].ToString();
                    model.LastName = reader["LastName"].ToString();
                    model.Email = reader["Email"].ToString();
                    model.Password = reader["Password"].ToString();

                    model.PhoneNumber = reader["PhoneNumber"].ToString();
                    model.Address = reader["Address"].ToString();

                    model.Created = Convert.ToDateTime(reader["Created"]);
                    model.Modified = Convert.ToDateTime(reader["Modified"]);
                }
            }
            return model;
        }
        #endregion

        #region OFD_UsersAdd
        public bool OFD_Users_Insert(OFD_UsersModel model)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand cmd = db.GetStoredProcCommand("OFD_Users_Insert");

                db.AddInParameter(cmd, "@FirstName", SqlDbType.VarChar, model.FirstName);

                db.AddInParameter(cmd, "@LastName", SqlDbType.VarChar, model.LastName);
                db.AddInParameter(cmd, "@Email", SqlDbType.VarChar, model.Email);

                db.AddInParameter(cmd, "@Password", SqlDbType.VarChar, model.Password);

                db.AddInParameter(cmd, "@PhoneNumber", SqlDbType.VarChar, model.PhoneNumber);
                db.AddInParameter(cmd, "@Address", SqlDbType.VarChar, model.Address);


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

        #region OFD_Users_Update
        public bool OFD_Users_Update(OFD_UsersModel model)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand cmd = db.GetStoredProcCommand("OFD_Users_Update");
                db.AddInParameter(cmd, "@UserID", SqlDbType.Int, model.UserID);
                db.AddInParameter(cmd, "@FirstName", SqlDbType.VarChar, model.FirstName);

                db.AddInParameter(cmd, "@LastName", SqlDbType.VarChar, model.LastName);
                db.AddInParameter(cmd, "@Email", SqlDbType.VarChar, model.Email);

                db.AddInParameter(cmd, "@Password", SqlDbType.VarChar, model.Password);

                db.AddInParameter(cmd, "@PhoneNumber", SqlDbType.VarChar, model.PhoneNumber);
                db.AddInParameter(cmd, "@Address", SqlDbType.VarChar, model.Address);
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