using System.ComponentModel.DataAnnotations;

namespace Student_Registration.Areas.LOC_City.Models
{
    public class LOC_CityModel
    {
        public int? CityID { get; set; }

        [Required]
        public string? CityName { get; set; }
        [Required]
        public string? CityCode { get; set; }

        [Required(ErrorMessage = "Please Enter valid  StateId")]
        public int? StateID { get; set; }

        [Required (ErrorMessage="Please Enter valid CountryId")]
        public int? CountryID { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime Modified { get; set; }
    }

    public class LOC_SearchCityModel
    {
        public string CityName { get; set; }
        public string CityCode { get; set; }
        public string CountryName { get; set; }
        public string StateName { get; set; }
    }

}
