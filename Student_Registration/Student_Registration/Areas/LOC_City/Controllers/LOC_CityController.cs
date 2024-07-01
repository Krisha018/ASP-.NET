using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Student_Registration.Models;
using Student_Registration.Areas.LOC_City.Models;
using System.Data.Common;
using Student_Registration.Areas.LOC_Contry.Models;
using Student_Registration.Areas.LOC_State.Models;

namespace Student_Registration.Areas.LOC_City.Controllers
{
    [Area("LOC_City")]
    [Route("LOC_City/[controller]/[action]")]
    public class LOC_CityController : Controller
    {
        private readonly IConfiguration configuration;

        public LOC_CityController(IConfiguration _configuration)
        {
            this.configuration = _configuration;
        }


     

        public IActionResult LOC_CityList(LOC_SearchCityModel searchCityModel)
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);

            DataTable dt = new DataTable();

            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_LOC_City_Search";


            command.Parameters.AddWithValue("@StateName", searchCityModel.StateName);
            command.Parameters.AddWithValue("@CountryName", searchCityModel.CountryName);
            command.Parameters.AddWithValue("@CityCode", searchCityModel.CityCode);
            command.Parameters.AddWithValue("@CityName", searchCityModel.CityName);
            SqlDataReader objSDR = command.ExecuteReader();
            dt.Load(objSDR);
            connection.Close();
            return View("LOC_CityList", dt);
        }


        public IActionResult LOC_CityListByID(int CityID)
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_City_SelectByPK";
            command.Parameters.AddWithValue("CityID", CityID);
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            connection.Close();
            return View(table);
        }

        public IActionResult LOC_CitySave(LOC_CityModel lOC_CityModel)
        {
            try
            {
                String connectionStr = this.configuration.GetConnectionString("ConnectionString");
                SqlConnection conn = new SqlConnection(connectionStr);
                conn.Open();
                SqlCommand objCmd = conn.CreateCommand();
                objCmd.CommandType = CommandType.StoredProcedure;
                if (lOC_CityModel.CityID != null)
                {
                    objCmd.CommandText = "PR_City_UpdateByPK";
                    objCmd.Parameters.AddWithValue("@CityID", lOC_CityModel.CityID);

                }
                else
                {
                    objCmd.CommandText = "PR_City_Insert";
                }
                objCmd.Parameters.AddWithValue("@CityName", lOC_CityModel.CityName);
                objCmd.Parameters.AddWithValue("@CityCode", lOC_CityModel.CityCode);
                objCmd.Parameters.AddWithValue("@StateID", lOC_CityModel.StateID);
                objCmd.Parameters.AddWithValue("@CountryID", lOC_CityModel.CountryID);


                objCmd.ExecuteNonQuery();

                conn.Close();
                return RedirectToAction("LOC_CityList");
            }
            catch (Exception ex)
            {
                return RedirectToAction("LOC_CityList");
            }
        }




        public IActionResult LOC_CityDelete(int CityID)
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_City_DeleteByPK";
            command.Parameters.AddWithValue("@CityID", CityID);
            command.ExecuteNonQuery();
            connection.Close();
            return RedirectToAction("LOC_CityList");
        }

       
        public IActionResult LOC_CityAdd( int CityID = 0)
        {

            string connectionstr = this.configuration.GetConnectionString("ConnectionString");

            //country drop down
            SqlConnection sqlConnection1 = new SqlConnection(connectionstr);
            sqlConnection1.Open();
            SqlCommand ObjCmd1 = sqlConnection1.CreateCommand();
            ObjCmd1.CommandType = CommandType.StoredProcedure;
            ObjCmd1.CommandText = "LOC_City_Dropdown";
            SqlDataReader sqlDataReader1 = ObjCmd1.ExecuteReader();
            List<LOC_CountryDropDownModel> list = new List<LOC_CountryDropDownModel>();
            DataTable dt1 = new DataTable();
            dt1.Load(sqlDataReader1);
            foreach (DataRow dr1 in dt1.Rows)
            {
                LOC_CountryDropDownModel vlst = new LOC_CountryDropDownModel();
                vlst.CountryID = int.Parse(dr1["CountryID"].ToString());
                vlst.CountryName = dr1["CountryName"].ToString();
                list.Add(vlst);
            }
            ViewBag.CountryList = list;

           // State drop down
            SqlConnection sqlConnection2 = new SqlConnection(connectionstr);
            sqlConnection2.Open();
            SqlCommand ObjCmd2 = sqlConnection2.CreateCommand();
            ObjCmd2.CommandType = CommandType.StoredProcedure;
            ObjCmd2.CommandText = "LOC_City_State_Dropdown";
            SqlDataReader sqlDataReader2 = ObjCmd2.ExecuteReader();
            List<LOC_StateDropDownModel> list1 = new List<LOC_StateDropDownModel>();
            DataTable dt2 = new DataTable();
            dt2.Load(sqlDataReader2);
            foreach (DataRow dr2 in dt2.Rows)
            {
                LOC_StateDropDownModel lOC_StateDropDownModel = new LOC_StateDropDownModel();
                lOC_StateDropDownModel.StateID = int.Parse(dr2["StateID"].ToString());
                lOC_StateDropDownModel.StateName = dr2["StateName"].ToString();
                list1.Add(lOC_StateDropDownModel);
            }
            ViewBag.StateList = list1;



            // Stete form fill

            if (CityID != 0)
            {
                string connectionString = this.configuration.GetConnectionString("ConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PR_City_SelectByPK";
                command.Parameters.AddWithValue("@CityID", CityID);
                SqlDataReader reader = command.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(reader);
                LOC_CityModel lOC_CityModel = new LOC_CityModel();
                foreach (DataRow dataRow in table.Rows)
                {
                    lOC_CityModel.StateID = Convert.ToInt32(dataRow["StateID"]);
                    lOC_CityModel.CountryID = Convert.ToInt32(dataRow["CountryID"]);
                    lOC_CityModel.CityName = dataRow["CityName"].ToString();
                    lOC_CityModel.CityCode = dataRow["CityCode"].ToString();
                    lOC_CityModel.CityID = Convert.ToInt32(dataRow["CityID"]);

                }
                return View("LOC_CityAddEdit", lOC_CityModel);
            }
            return View("LOC_CityAddEdit");

        }
      


    }
}
