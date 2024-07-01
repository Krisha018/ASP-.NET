using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Student_Registration.Models;
using Student_Registration.Areas.MST_Branch.Models;
using Microsoft.CodeAnalysis.Operations;
using Student_Registration.Areas.LOC_State.Models;
using Student_Registration.Areas.LOC_Country.Models;

namespace Student_Registration.Areas.MST_Branch.Controllers
{
    [Area("MST_Branch")]
    [Route("MST_Branch/[controller]/[action]")]
    public class MST_BranchController : Controller
    {
        private readonly IConfiguration configuration;

        public MST_BranchController(IConfiguration _configuration)
        {
            this.configuration = _configuration;
        }

        public IActionResult MST_BranchList(MST_Search_BranchModel searchBranchModel)
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_MST_Branch_Search";
            command.Parameters.AddWithValue("@BranchName", searchBranchModel.BranchName);

            command.Parameters.AddWithValue("@BranchCode", searchBranchModel.BranchCode);
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            connection.Close();
            return View(table);

        }

      

        public IActionResult MST_BranchListByID(int BranchID)
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Branch_SelectByPK";
            command.Parameters.AddWithValue("BranchID", BranchID);
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            connection.Close();
            return View(table);
        }

        public IActionResult MST_BranchSave(MST_BranchModel MST_BranchModel)
        {
            try
            {
                String connectionStr = this.configuration.GetConnectionString("ConnectionString");
                SqlConnection conn = new SqlConnection(connectionStr);
                conn.Open();
                SqlCommand objCmd = conn.CreateCommand();
                objCmd.CommandType = CommandType.StoredProcedure;
                if (MST_BranchModel.BranchID != null)
                {
                    objCmd.CommandText = "PR_Branch_UpdateByPK";
                    objCmd.Parameters.AddWithValue("@BranchID", MST_BranchModel.BranchID);

                }
                else
                {
                    objCmd.CommandText = "PR_Branch_Insert";
                }
                objCmd.Parameters.AddWithValue("@BranchName", MST_BranchModel.BranchName);
                objCmd.Parameters.AddWithValue("@BranchCode", MST_BranchModel.BranchCode);

               


                objCmd.ExecuteNonQuery();

                conn.Close();
                return RedirectToAction("MST_Branchlist");
            }
            catch (Exception ex)
            {
                return RedirectToAction("MST_Branchlist");
            }
        }

        public IActionResult MST_BranchDelete(int BranchID)
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Branch_DeleteByPK";
            command.Parameters.AddWithValue("@BranchID", BranchID);
            command.ExecuteNonQuery();
            connection.Close();
            return RedirectToAction("MST_BranchList");
        }

        public IActionResult MST_BranchAdd(int BranchID = 0)
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Branch_SelectByPK";
            command.Parameters.AddWithValue("@BranchID", BranchID);
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            MST_BranchModel mst_BranchModel = new MST_BranchModel();
            foreach (DataRow dataRow in table.Rows)
            {
                mst_BranchModel.BranchID = Convert.ToInt32(dataRow["BranchID"]);
                mst_BranchModel.BranchName = dataRow["BranchName"].ToString();
                mst_BranchModel.BranchCode = dataRow["BranchCode"].ToString();
         
            }
            return View("MST_BranchAddEdit", mst_BranchModel);
        }






    }
}
