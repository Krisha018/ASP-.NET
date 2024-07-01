using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace Student_Registration.Areas.LOC_Country.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExcelToXmlConverterController : Controller
    {
        [HttpGet("exportToXml")]
        public IActionResult ExportToXml()
        {
            try
            {
                // Specify the path to your Excel file
                string excelFilePath = "path/to/your/excel/file.xlsx";

                // Convert Excel data to XML
                var xmlData = ExcelToXmlConverter.ConvertToXml(excelFilePath);

                // Save XML data to a memory stream
                using (var memoryStream = new MemoryStream())
                {
                    xmlData.Save(memoryStream);
                    memoryStream.Position = 0;

                    // Return XML file as response
                    return File(memoryStream, "application/xml", "exportedData.xml");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error exporting data: {ex.Message}");
            }
        }


        public class ExcelToXmlConverter
        {
            public static XDocument ConvertToXml(string excelFilePath)
            {
                var workbook = new XLWorkbook(excelFilePath);
                var worksheet = workbook.Worksheets.First();

                var xmlData = new XDocument(
                    new XElement("Workbook",
                        new XElement("Worksheet",
                            new XElement("Rows",
                                worksheet.RowsUsed().Select(row =>
                                    new XElement("Row",
                                        row.Cells().Select(cell =>
                                            new XElement("Cell",
                                                new XElement("Value", cell.Value.ToString())
                                            )
                                        )
                                    )
                                )
                            )
                        )
                    )
                );

                return xmlData;
            }
        }
    }
}
