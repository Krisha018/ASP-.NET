using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Online_Food.Areas.Login.Models;
using System.Data;
using System.Data.Common;

namespace Online_Food.DAL
{
    public class Login_Base_DAL : DAL_Helper
    {
        public LoginModel Login(LoginModel loginModel)
        {
            SqlDatabase db = new SqlDatabase(ConnString);
            DbCommand cmd = db.GetStoredProcCommand("PR_UserLogin1");
            db.AddInParameter(cmd, "@Email", SqlDbType.VarChar, loginModel.Email);

            LoginModel model = new LoginModel();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    model.UserID = Convert.ToInt32(reader["UserID"]);

                    model.UserName = reader["UserName"].ToString();
                    model.Password = reader["Password"].ToString();
                    model.Email = reader["Email"].ToString();



                }
            }
            return model;

        }
    }
}
