using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Student_Registration.Models;
using Student_Registration.Areas.MST_Student.Models;


namespace Student_Registration.Areas.MST_Student.Controllers
{
    [Area("MST_Student")]
    [Route("MST_Student/[controller]/[action]")]
    public class MST_StudentController : Controller
    {
        private readonly IConfiguration configuration;

        public MST_StudentController(IConfiguration _configuration)
        {
            this.configuration = _configuration;
        }

       

        public IActionResult MST_StudentList(MST_Search_StudentModel searchStudentModel)
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_MST_Student_Search";
            command.Parameters.AddWithValue("@StudentName", searchStudentModel.StudentName);
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            connection.Close();
            return View(table);

        }



        public IActionResult MST_StudentListByID(int StudentID)
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Student_SelectByPK";
            command.Parameters.AddWithValue("StudentID", StudentID);
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            connection.Close();
            return View(table);
        }

       

        public IActionResult MST_StudentSave(MST_StudentModel StudentModel)
        {
            try
            {
                String connectionStr = this.configuration.GetConnectionString("ConnectionString");
                SqlConnection conn = new SqlConnection(connectionStr);
                conn.Open();
                SqlCommand objCmd = conn.CreateCommand();
                objCmd.CommandType = CommandType.StoredProcedure;
                if (StudentModel.StudentID!= null)
                {

                    objCmd.CommandText = "PR_Student_UpdateByPK";
                    objCmd.Parameters.AddWithValue("@StudentID", StudentModel.StudentID);
                }
                else
                {
                    objCmd.CommandText = "PR_Student_Insert";
                   
                   
                }
                objCmd.Parameters.AddWithValue("@BranchID", StudentModel.BranchID);
                objCmd.Parameters.AddWithValue("@CityID", StudentModel.CityID);
                objCmd .Parameters.AddWithValue("@MobileNoStudent", StudentModel.MobileNoStudent);
                objCmd.Parameters.AddWithValue("@Age", StudentModel.Age);
                objCmd.Parameters.AddWithValue("@StudentName", StudentModel.StudentName);
                objCmd.Parameters.AddWithValue("@IsActive", StudentModel.IsActive);
                objCmd.Parameters.AddWithValue("@Email", StudentModel.Email);
                objCmd.Parameters.AddWithValue("@MobileNoFather", StudentModel.MobileNoFather);
                objCmd.Parameters.AddWithValue("@Address", StudentModel.Address);
                objCmd.Parameters.AddWithValue("@BirthDate", StudentModel.BirthDate);
                objCmd.Parameters.AddWithValue("@Gender", StudentModel.Gender);
                objCmd.Parameters.AddWithValue("@Password", StudentModel.Password);


                objCmd.ExecuteNonQuery();

                conn.Close();
                return RedirectToAction("MST_StudentList");
            }
            catch (Exception ex)
            {
                return RedirectToAction("MST_StudentList");
            }
        }

        public IActionResult MST_StudentDelete(int StudentID)
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Student_DeleteByPK";
            command.Parameters.AddWithValue("@StudentID", StudentID);
            command.ExecuteNonQuery();
            connection.Close();
            return RedirectToAction("MST_StudentList");
        }

        public IActionResult MST_StudentAdd(int StudentID = 0)
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Student_SelectByPK";
            command.Parameters.AddWithValue("@StudentID", StudentID);
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            MST_StudentModel mST_StudentModel = new MST_StudentModel();
            foreach (DataRow dataRow in table.Rows)
            {
                mST_StudentModel.StudentID = Convert.ToInt32(dataRow["StudentID"]);
                mST_StudentModel.StudentName = dataRow["StudentName"].ToString();
                mST_StudentModel.MobileNoStudent = dataRow["MobileNoStudent"].ToString();
                mST_StudentModel.Email = dataRow["Email"].ToString();
                mST_StudentModel.MobileNoFather = dataRow["MobileNoFather"].ToString();
                mST_StudentModel.Address = dataRow["Address"].ToString();
                mST_StudentModel.BirthDate = Convert.ToDateTime(dataRow["BirthDate"]);
                mST_StudentModel.Age = dataRow["Age"].ToString();
                mST_StudentModel.IsActive = dataRow["IsActive"].ToString();
                mST_StudentModel.Gender = dataRow["Gender"].ToString();
                mST_StudentModel.Password = dataRow["Password"].ToString();
                mST_StudentModel.BranchID = Convert.ToInt32(dataRow["BranchID"]);
                mST_StudentModel.CityID = Convert.ToInt32(dataRow["CityID"]);
            }
            return View("MST_StudentAddEdit", mST_StudentModel);
        }





    }
}
