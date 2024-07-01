using APIDemo.DAL;
using APIDemo.Models;



namespace APIDemo1.BAL
{

    public class Person_BALBase
    {
        #region API_Person_SelectAll
        public List<PersonModel> API_Person_SelectAll()
        {

            Person_DALBase user_DALBase = new Person_DALBase();
            List<PersonModel> userModels = user_DALBase.API_Person_SelectAll();
            return userModels;
        }
        #endregion

        #region API_Person_SelectByID
        public PersonModel API_Person_SelectByID(int UserID)
        {

            Person_DALBase user_DALBase = new Person_DALBase();
            PersonModel userModel = user_DALBase.API_Person_SelectByID(UserID);
            return userModel;


        }
        #endregion

        #region API_Person_Insert
        public bool API_Person_Insert(PersonModel PersonModel)
        {
            try
            {
                Person_DALBase person_DALBase = new Person_DALBase();
                if (person_DALBase.API_Person_Insert(PersonModel))
                    return true;
                else
                    return false;
            }
            catch (Exception ex) 
            {
                return false;   
            }    

        }
        #endregion

        //#region API_Person_Update
        //public bool API_Person_Update( int PersonID ,PersonModel PersonModel)
        //{
        //    try
        //    {
        //        Person_DALBase person_DALBase = new Person_DALBase();
        //        if (person_DALBase.API_Person_Update(PersonModel))
        //            return true;
        //        else
        //            return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }

        //}
        //#endregion

        #region API_Person_Delete
        public bool API_Person_Delete( int PersonID)
        {
            try
            {
                Person_DALBase person_DALBase = new Person_DALBase();
                if (person_DALBase.API_Person_Delete(PersonID))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
    #endregion
}