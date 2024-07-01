using APIDemo.DAL;
using APIDemo.Models;
using APIDemo1.BAL;
using Microsoft.AspNetCore.Mvc;

namespace APIDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class PersonController : Controller
    {
        #region Get All Person
        [HttpGet]
        public IActionResult Get()
        {
            Person_BALBase person_BALBase = new Person_BALBase();
            List<PersonModel> persons = person_BALBase.API_Person_SelectAll();
            // Make the Response in key Value Pair
            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();
            if (persons.Count > 0 && persons != null)
            {
                response.Add("status", true);
                response.Add("message", "Data Found");
                response.Add("data", persons);
                return Ok(response);
            }
            else
            {
                response.Add("status", true);
                response.Add("message", "Data Found");
                response.Add("data", null);
                return NotFound(response);
            }
        }
        #endregion
        #region Get Person By ID
        [HttpGet("{PersonID}")]
        public IActionResult Get(int PersonID)
        {
            Person_BALBase user_BALBase = new Person_BALBase();
            PersonModel person = user_BALBase.API_Person_SelectByID(PersonID);
            //Make the Response in key Value Pair
            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();
            if (person.PersonID != 0)
            {
                response.Add("status", true);
                response.Add("message", "Data Found");
                response.Add("data", person);
                return Ok(response);
            }
            else
            {
                response.Add("status", false);
                response.Add("message", "Data Not Found");
                response.Add("data", null);
                return NotFound(response);
            }
        }
        #endregion

        #region Get Person Insert
        [HttpPost]
        public IActionResult Post([FromForm] PersonModel personModel) 
        {
            Person_DALBase person_DALBase = new Person_DALBase();
            bool IsSuccess = person_DALBase.API_Person_Insert(personModel);
            //Make the Response in key Value Pair
            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();
            if (IsSuccess)
            {
                response.Add("status", true);
                response.Add("message", "Data Inserted Successfully");
               return Ok(response);
            }
            else
            {
                response.Add("status", false);
                response.Add("message", "Some Error has been occured");
                return Ok(response);
            }
        }
        #endregion

        #region Get Person Update
        [HttpPut]
        public IActionResult Put(int PersonID,[FromForm] PersonModel PersonModel)
        {
            Person_DALBase person_DALBase = new Person_DALBase();
           

            bool isSet = person_DALBase.API_Person_Update(PersonID,PersonModel);
            //Make the Response in key Value Pair
            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();
            if (isSet)
            {
                response.Add("status", true);
                response.Add("message", "Data Updated Successfully");
                return Ok(response);
            }
            else
            {
                response.Add("status", false);
                response.Add("message", "Some Error has been occured");
                response.Add("data",null);
                return NotFound(response);
            }
        }
        #endregion


        #region Get Person Delete
        [HttpDelete]
        public IActionResult Delete(int PersonID    )
        {
            Person_DALBase person_DALBase = new Person_DALBase();


            bool isSet = person_DALBase.API_Person_Delete(PersonID);
            //Make the Response in key Value Pair
            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();
            if (isSet)
            {
                response.Add("status", true);
                response.Add("message", "Data Deleted Successfully");
                return Ok(response);
            }
            else
            {
                response.Add("status", false);
                response.Add("message", "Some Error has been occured");
                response.Add("data", null);
                return NotFound(response);
            }
        }
        #endregion
    }
}
