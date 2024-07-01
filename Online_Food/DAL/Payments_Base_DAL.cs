using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Online_Food.Areas.Payments.Models;
using System.Data.Common;
using System.Data;

namespace Online_Food.DAL
{
    public class Payments_Base_DAL:DAL_Helper
    {
        #region Payments_SelectAll

        public List<PaymentsModel> PaymentsList()
        {
            List<PaymentsModel> list = new List<PaymentsModel>();
            SqlDatabase db = new SqlDatabase(ConnString);
            DbCommand cmd = db.GetStoredProcCommand("Payments_SelectAll");
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    PaymentsModel model = new PaymentsModel();


                    model.PaymentID = Convert.ToInt32(reader["PaymentID"]);
                    model.OrderID = Convert.ToInt32(reader["OrderID"]);
                    model.TotalAmount= reader["TotalAmount"].ToString();

                    model.Amount = reader["Amount"].ToString();
                    model.PaymentDate = Convert.ToDateTime(reader["PaymentDate"]);
                   
                    model.PaymentStatus = reader["PaymentStatus"].ToString(); ;
                    model.Created = Convert.ToDateTime(reader["Created"]);
                    model.Modified = Convert.ToDateTime(reader["Modified"]);
                    list.Add(model);
                }
            }
            return list;
        }
        #endregion




        #region Payments_Delete
        public string DeletePayment(int PaymentID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("Payments_Delete");
                sqlDB.AddInParameter(dbCMD, "PaymentID", SqlDbType.Int, PaymentID);
                sqlDB.ExecuteNonQuery(dbCMD);
                return "Record Deleted Successfully";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        #endregion

        #region Payments_SelectByPK
        public PaymentsModel Payments_SelectByPK(int PaymentID)
        {
            SqlDatabase db = new SqlDatabase(ConnString);
            DbCommand cmd = db.GetStoredProcCommand("Payments_SelectByPK");
            db.AddInParameter(cmd, "@PaymentID", SqlDbType.Int, PaymentID);
            PaymentsModel model = new PaymentsModel();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {

                    model.PaymentID = Convert.ToInt32(reader["PaymentID"]);
                    model.OrderID = Convert.ToInt32(reader["OrderID"]);
                    model.TotalAmount = reader["TotalAmount"].ToString();

                    model.Amount = reader["Amount"].ToString();
                    model.PaymentDate = Convert.ToDateTime(reader["PaymentDate"]);

                    model.PaymentStatus = reader["PaymentStatus"].ToString(); ;
                    model.Created = Convert.ToDateTime(reader["Created"]);
                    model.Modified = Convert.ToDateTime(reader["Modified"]);

                }
            }
            return model;
        }
        #endregion


        #region PaymentsAdd
        public bool Payments_Insert(PaymentsModel model)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand cmd = db.GetStoredProcCommand("Payments_Insert");
                db.AddInParameter(cmd, "@OrderID", SqlDbType.Int, model.OrderID);
                db.AddInParameter(cmd, "@Amount", SqlDbType.VarChar, model.Amount);
                db.AddInParameter(cmd, "@PaymentDate", SqlDbType.DateTime, model.PaymentDate);
                db.AddInParameter(cmd, "@PaymentStatus", SqlDbType.VarChar, model.PaymentStatus);
               


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
        #region Payments_Update
        public bool Payments_Update(PaymentsModel model)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand cmd = db.GetStoredProcCommand("Payments_Update");
                db.AddInParameter(cmd, "@PaymentID", SqlDbType.Int, model.PaymentID);
                db.AddInParameter(cmd, "@OrderID", SqlDbType.Int, model.OrderID);
                db.AddInParameter(cmd, "@Amount", SqlDbType.VarChar, model.Amount);
                db.AddInParameter(cmd, "@PaymentDate", SqlDbType.DateTime, model.PaymentDate);
                db.AddInParameter(cmd, "@PaymentStatus", SqlDbType.VarChar, model.PaymentStatus);
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
