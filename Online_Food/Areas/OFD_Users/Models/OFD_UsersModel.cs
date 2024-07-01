namespace Online_Food.Areas.OFD_Users.Models
{
    public class OFD_UsersModel
    {
        public int  UserID { get; set; }
       
        public string FirstName { get; set; }

        public string LastName { get; set; }


        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }
    }

    public class OFD_UsersSearchModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }

    public class OFD_UserDropdownModel
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }

        
    }



}
