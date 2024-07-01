using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Online_Food.Areas.OFD_Users.Models;
using Online_Food.DAL;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;



namespace Online_Food.Areas.OFD_Users.Controllers
{
    [Area("OFD_Users")]
    [Route("OFD_Users/[controller]/[action]")]
    public class OFD_UsersController: Controller
    {
        private readonly IConfiguration configuration;

        public OFD_UsersController(IConfiguration _configuration)
        {
            this.configuration = _configuration;
        }






        #region OFD_UsersSearchModel
        public IActionResult OFD_UserSearch(OFD_UsersSearchModel OFD_UsersSearch)
        {
            try
            {
                String ConnString = this.configuration.GetConnectionString("MyConnectionString");
                Database db = new SqlDatabase(ConnString);
                DbCommand dbCmd = db.GetStoredProcCommand("PR_OFD_Users_Search");
                db.AddInParameter(dbCmd, "@FirstName", (DbType)SqlDbType.VarChar, OFD_UsersSearch.FirstName);
                db.AddInParameter(dbCmd, "@LastName", (DbType)SqlDbType.VarChar, OFD_UsersSearch.LastName);



                using (IDataReader dr = db.ExecuteReader(dbCmd))
                {
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    List<OFD_UsersModel> modelList = new List<OFD_UsersModel>();

                    foreach (DataRow row in dt.Rows)
                    {
                        OFD_UsersModel users = new OFD_UsersModel();
                        users.UserID = Convert.ToInt32(row["UserID"]);
                        users.FirstName = row["FirstName"].ToString();
                        users.LastName = row["LastName"].ToString();
                        users.Email = row["Email"].ToString();
                        users.Password = row["Password"].ToString();
                        users.PhoneNumber = row["PhoneNumber"].ToString();
                        users.Address = row["Address"].ToString();
                        users.Created = Convert.ToDateTime(row["Created"]);
                        users.Modified = Convert.ToDateTime(row["Modified"]);
                        // Set other properties here

                        modelList.Add(users);
                    }

                    return View("OFD_UsersList", modelList);
                }
            }
            catch (Exception ex)
            {

                return null;

            }
        }
        #endregion

        #region SelectAll

        public IActionResult OFD_UsersList()
        {

            OFD_Users_Base_DAL dal = new OFD_Users_Base_DAL();
            return View(dal.OFD_UsersList());

        }
        #endregion
        #region DeleteUsers
        public IActionResult DeleteUsers(int UserID)
        {

            OFD_Users_Base_DAL dal = new OFD_Users_Base_DAL();
            TempData["message"] = dal.DeleteUsers(UserID); ;
            return RedirectToAction("OFD_UsersList");
        }
        #endregion
        #region AddEditUsers

        public IActionResult OFD_UsersAddEdit(int UserID)
        {
            if (UserID == null)
            {
                return View();
            }
            else
            {
                OFD_Users_Base_DAL dal = new OFD_Users_Base_DAL();
                OFD_UsersModel model = dal.OFD_Users_SelectByPK(UserID);
                return View(model);
            }

        }
        #endregion
        

        #region SaveForAddEdit
        public IActionResult SaveForAddEdit(OFD_UsersModel model)
        {
            bool ans = false;
            Console.WriteLine(model.UserID);
            OFD_Users_Base_DAL dal = new OFD_Users_Base_DAL();
            if (model.UserID != 0)
            {
                ans = dal.OFD_Users_Update(model);
                TempData["message"] = "Record Updated Successfully";
            }
            else
            {
                ans = dal.OFD_Users_Insert(model);
                TempData["message"] = "Record Inserted Successfully";
            }
            if (ans)
            {
                return RedirectToAction("OFD_UsersList");
            }
            else
            {
                return RedirectToAction("OFD_UsersList");
            }
        }
        #endregion

        #region OFD_UserDetails
        public IActionResult OFD_UserDetails(int UserID)
        {
            OFD_Users_Base_DAL bal = new OFD_Users_Base_DAL();
            OFD_UsersModel model = bal.OFD_Users_SelectByPK(UserID);
            return View(model);

        }
        #endregion
        #region #Cancel
        public IActionResult Cancel()
        {
            return RedirectToAction("OFD_UsersList");
        }
        #endregion


        #region ExportXML
        public IActionResult ExportCountryToExcel()
        {
            // Fetch player data from the database
            DataTable dt = FetchPlayerDataFromDatabase();

            // Export data to Excel
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("OFD_Users");
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
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "OFD_Users.xlsx");
                }
            }
        }

        private DataTable FetchPlayerDataFromDatabase()
        {
            // Your database logic to fetch player data
            string connectionstr = configuration.GetConnectionString("MyConnectionString");
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(connectionstr);
            conn.Open();
            SqlCommand objCmd = conn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "OFD_Users_SelectAll";
            SqlDataReader objDR = objCmd.ExecuteReader();
            dt.Load(objDR);
            conn.Close();

            return dt;
        }

        #endregion

        //[HttpPost]

        //#region Multiple Delete
        //public IActionResult Delete(int[] id)
        //{

        //    foreach (var item in id)
        //    {
        //        try
        //        {
        //            OFD_Users_Delete(item);
        //            Console.WriteLine("Deleted " + item);
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }

        //    }

        //    return View("OFD_UsersList");
        //}

       
        //#endregion


    }
}
