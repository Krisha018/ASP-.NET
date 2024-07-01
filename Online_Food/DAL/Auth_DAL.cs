using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Online_Food.Models;
using System.Data;
using System.Data.Common;

namespace Online_Food.DAL
{
    public class Auth_DAL:DAL_Helper
    {
        #region
        public AuthModel PR_UserLogin1(string email)
        {
            AuthModel user = new AuthModel();
            SqlDatabase db = new SqlDatabase(ConnString);
            DbCommand cmd = db.GetStoredProcCommand("PR_UserLogin1");
            db.AddInParameter(cmd, "@Email", SqlDbType.VarChar, email);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    user.UserID = (int)reader["UserID"];
                    user.Email = (string)reader["Email"];
                    user.UserName = (string)reader["UserName"];
                    user.Password = (string)reader["Password"];
                    user.IsAdmin = (string)reader["IsAdmin"];

                }
            }

            return user;
        }
        #endregion
    }
}
