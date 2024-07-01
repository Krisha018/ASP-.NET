using System.ComponentModel.DataAnnotations;
namespace Student_Registration.Areas.MST_Student.Models
{
    public class MST_StudentModel
    {
        public int? StudentID { get; set; }

        public string StudentName { get; set; }

        public string MobileNoStudent { get; set; }

        public string Email { get; set; }

        public string MobileNoFather { get; set; }

        public string Address { get; set; }

        public DateTime BirthDate { get; set; }

        public string Age { get; set; }

        public string IsActive { get; set; }

        public string Gender { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        public string? Password { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

        [Required(ErrorMessage = "Please Enter BranchID")]
        public int? BranchID { get; set; }

        [Required(ErrorMessage = "Please Enter CityID")]
        public int? CityID { get; set; }

    }

    public class MST_Search_StudentModel
    {
        public string StudentName { get; set; }

       
    }
}
