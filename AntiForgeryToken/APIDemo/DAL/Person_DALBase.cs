using APIDemo.Models;
using System.Data;
using System.Data.Common;


namespace APIDemo.DAL
{
    public class Person_DALBase : DAL_Helper
    {
        #region API_Person_SelectAll
        public List<PersonModel> API_Person_SelectAll()
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("API_Person_SelectAll");
                List<PersonModel> personModels = new List<PersonModel>();
                using (IDataReader dr = sqlDatabase.ExecuteReader(dbCommand))
                {
                    while (dr.Read())
                    {
                        PersonModel personModel = new PersonModel();
                        personModel.PersonID = Convert.ToInt32(dr["PersonID"].ToString());
                        personModel.Name = dr["Name"].ToString();
                        personModel.Email = dr["Email"].ToString();
                        personModel.Contact = dr["Contact"].ToString();
                        personModels.Add(personModel);
                    }
                }
                return personModels;

            }
            catch (Exception ex)
            {
                return null;
            }

        }
        #endregion

        #region API_Person_SelectByID
        public PersonModel API_Person_SelectByID(int PersonID)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("API_Person_SelectByID");
                sqlDatabase.AddInParameter(dbCommand, "@PersonID", SqlDbType.Int, PersonID);
                PersonModel personModel = new PersonModel();
                using (IDataReader dr = sqlDatabase.ExecuteReader(dbCommand))
                {
                    while (dr.Read())
                    {
                        personModel.PersonID = Convert.ToInt32(dr["PersonID"].ToString());
                        personModel.Name = dr["Name"].ToString();
                        personModel.Email = dr["Email"].ToString();
                        personModel.Contact = dr["Contact"].ToString();
                    }
                }
                return personModel;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        #endregion

        #region API_Person_Insert
        public bool API_Person_Insert(PersonModel PersonModel)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("API_Person_Insert");
                sqlDatabase.AddInParameter(dbCommand, "@Name", SqlDbType.VarChar, PersonModel.Name);
                sqlDatabase.AddInParameter(dbCommand, "@Email", SqlDbType.VarChar, PersonModel.Email);
                sqlDatabase.AddInParameter(dbCommand, "@Contact", SqlDbType.VarChar, PersonModel.Contact);
                sqlDatabase.ExecuteNonQuery(dbCommand);
                return true;    

            }
            catch
            {
                return false;
            }


            
            }
        #endregion


        #region API_Person_Update

        public bool API_Person_Update(int PersonID, PersonModel PersonModel)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("API_Person_Update");
                sqlDatabase.AddInParameter(dbCommand, "@PersonID", SqlDbType.Int, PersonModel.PersonID);
                sqlDatabase.AddInParameter(dbCommand, "@Name", SqlDbType.VarChar, PersonModel.Name);
                sqlDatabase.AddInParameter(dbCommand, "@Email", SqlDbType.VarChar, PersonModel.Email);
                sqlDatabase.AddInParameter(dbCommand, "@Contact", SqlDbType.VarChar, PersonModel.Contact);
                sqlDatabase.ExecuteNonQuery(dbCommand);
                return true;

            }
            catch
            {
                return false;
            }

            #endregion





        }

        #region API_Person_Delete

        public bool API_Person_Delete(int PersonID)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("API_Person_Delete");
                sqlDatabase.AddInParameter(dbCommand, "@PersonID", SqlDbType.Int,PersonID);
           
                sqlDatabase.ExecuteNonQuery(dbCommand);
                return true;

            }
            catch
            {
                return false;
            }

            #endregion





        }
    }
}
