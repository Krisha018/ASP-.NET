using APIDemo.BAL;
using APIDemo.Model;
using System.Data;

namespace APIDemo.DAL
{
    public class Person_DALBase : DAL_Helper
    {
        public List<PersonModel> API_Person_SelectAll()
        {
            try
            {
                SqlDatabase sqldb = new SqlDatabase(ConnStr);
                DbCommand cmd = sqldb.GetStoredProcCommand("API_Person_SelectAll");
                List<PersonModel> list = new List<PersonModel>();
                using (IDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        PersonModel pm = new PersonModel();
                        pm.PersonID = Convert.ToInt32(dr["PersonID"].ToString());
                        pm.Name = dr["Name"].ToString();
                        pm.Email = dr["Email"].ToString();
                        pm.Contact = dr["Contact"].ToString();
                        list.Add(pm);
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
