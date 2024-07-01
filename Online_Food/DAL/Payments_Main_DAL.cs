using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace Online_Food.DAL
{
    public class Payments_Main_DAL:Payments_Base_DAL
    {

        #region FoodorderDropDownList


        public DataTable FoodorderDropDownList()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_FoodOrder_DropdownList");
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
    }
}
