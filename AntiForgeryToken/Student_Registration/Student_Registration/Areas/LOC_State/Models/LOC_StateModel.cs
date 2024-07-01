using System.ComponentModel.DataAnnotations;

namespace Student_Registration.Areas.LOC_State.Models
{
    public class LOC_StateModel
    {
        public int? StateID { get; set; }
        [Required(ErrorMessage = "Please Enter CountryID")]
        public int? CountryID { get; set; }
        [Required(ErrorMessage = "Please Enter StateName")]
        public string StateName { get; set; }

        [Required(ErrorMessage = "Please Enter StateCode")]
        public string StateCode { get; set; }
      
    }

    public class LOC_SearchStateModel
    {
        public string StateName { get; set; }

        public string StateCode { get; set; }
        public string CountryName { get;  set; }
    }

    public class LOC_StateDropDownModel
    {

        public int? StateID { get; set; }
        public string StateName { get; set; }
    }


}
