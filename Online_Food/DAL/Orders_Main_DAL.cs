using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Online_Food.Areas.Menu_Items.Models;

namespace Online_Food.DAL
{
    public class Orders_Main_DAL:Orders_Base_DAL
    {
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
