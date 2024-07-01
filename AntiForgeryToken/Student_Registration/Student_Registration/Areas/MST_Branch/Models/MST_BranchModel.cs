using System.ComponentModel.DataAnnotations;
namespace Student_Registration.Areas.MST_Branch.Models
{
    public class MST_BranchModel
    {
        public int? BranchID { get; set; }
        
        [Required]
        public string? BranchName { get; set; }
        [Required]
        public string? BranchCode { get; set; }
        public string Created { get; set; }
        public string Modified { get; set; }

    }

    public class MST_Search_BranchModel
    {
        public string BranchName { get; set; }

        public string BranchCode { get; set; }
    }
}
