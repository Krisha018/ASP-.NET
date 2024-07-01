using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Student_Registration.Models;
using Student_Registration.Areas.LOC_Contry.Models;
using Student_Registration.Areas.LOC_Country.Models;
using ClosedXML.Excel;
using System.Xml.Linq;
using System.Configuration;
using DocumentFormat.OpenXml.Math;
using Microsoft.CodeAnalysis.Differencing;
using System.Linq;

namespace Student_Registration.Areas.LOC_Contry.Controllers
{
    [Area("LOC_Country")]
    [Route("LOC_Country/[controller]/[action]")]
    public class LOC_CountryController : Controller
    {
        private readonly IConfiguration configuration;
       
        public LOC_CountryController(IConfiguration _configuration)
        {
            this.configuration = _configuration;
        }


        
        public IActionResult LOC_CountryList(LOC_SearchCountryModel searchCountryModel)
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_LOC_Country_Search";
            command.Parameters.AddWithValue("@CountryName", searchCountryModel.CountryName);

            command.Parameters.AddWithValue("@CountryCode", searchCountryModel.CountryCode);
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            connection.Close();
            return View(table);

        }

        public IActionResult LOC_CountryListByID(int CountryID)
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Country_SelectByPK";
            command.Parameters.AddWithValue("CountryID", CountryID);
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            connection.Close();
            return View(table);
        }

        public IActionResult LOC_CountrySave(LOC_CountryModel lOC_CountryModel, int CountryID = 0)
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            if (CountryID == 0)
            {
                command.CommandText = "PR_Country_Insert";
            }
            else
            {
                command.CommandText = "PR_Country_UpdateByPK";
                command.Parameters.AddWithValue("@CountryID", CountryID);
            }
            command.Parameters.AddWithValue("@CountryName", lOC_CountryModel.CountryName);
            command.Parameters.AddWithValue("@CountryCode", lOC_CountryModel.CountryCode);
            command.ExecuteNonQuery();
            connection.Close();
            return RedirectToAction("LOC_CountryList");
        }

        public IActionResult LOC_CountryDelete(int CountryID)
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Country_DeleteByPK";
            command.Parameters.AddWithValue("@CountryID", CountryID);
            command.ExecuteNonQuery();
            connection.Close();
            return RedirectToAction("LOC_CountryList");
        }

        public IActionResult LOC_CountryAdd(int CountryID = 0)
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Country_SelectByPK";
            command.Parameters.AddWithValue("@CountryID", CountryID);
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            LOC_CountryModel lOC_CountryModel = new LOC_CountryModel();
            foreach (DataRow dataRow in table.Rows)
            {
                lOC_CountryModel.CountryID = Convert.ToInt32(dataRow["CountryID"]);
                lOC_CountryModel.CountryName = dataRow["CountryName"].ToString();
                lOC_CountryModel.CountryCode = dataRow["CountryCode"].ToString();
            }
            return View("LOC_CountryAddEdit", lOC_CountryModel);
        }

        public IActionResult Search(string CountryName, string CountryCode)
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "LOC_Country_SelectByCountryNameCountryCode";
            command.Parameters.AddWithValue("@CountryName", CountryName);
            command.Parameters.AddWithValue("@CountryCode", CountryCode);
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            connection.Close();
            return View(table);
        }





        #region ExportXML
        public IActionResult ExportCountryToExcel()
        {
            // Fetch player data from the database
            DataTable dt = FetchPlayerDataFromDatabase();

            // Export data to Excel
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("LOC_Country");
                var currentRow = 1;

                // Add headers
                foreach (DataColumn column in dt.Columns)
                {
                    worksheet.Cell(currentRow, dt.Columns.IndexOf(column) + 1).Value = column.ColumnName;
                }

                // Add data
                foreach (DataRow row in dt.Rows)
                {
                    currentRow++;
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        worksheet.Cell(currentRow, i + 1).Value = row[i].ToString();

                    }
                }

                // Save the workbook to a memory stream
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "LOC_Country.xlsx");
                }
            }
        }

        private DataTable FetchPlayerDataFromDatabase()
        {
            // Your database logic to fetch player data
            string connectionstr = configuration.GetConnectionString("connectionString");
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(connectionstr);
            conn.Open();
            SqlCommand objCmd = conn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_Country_SelectAll";
            SqlDataReader objDR = objCmd.ExecuteReader();
            dt.Load(objDR);
            conn.Close();

            return dt;
        }

        #endregion

        [HttpPost]

        #region Multiple Delete
        public IActionResult Delete(int[] id)
        {

            foreach (var item in id)
            {
                try
                {
                    LOC_CountryDelete(item);
                    Console.WriteLine("Deleted " + item);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

            return View("LOC_CountryList");
        }

        #endregion


    }
}
