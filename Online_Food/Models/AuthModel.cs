using System.ComponentModel.DataAnnotations;

namespace Online_Food.Models
{
    public class LoginModel
    {
        public int UserID { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        public string IsAdmin { get; set; }
    }

    public class AuthModel
    {
        public int UserID { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        public string IsAdmin { get; set; }
    }
}
